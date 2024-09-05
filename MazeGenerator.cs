
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject mazeBlock;
    public GameObject mazeFinalBlock;
    private GameObject mazeFinalBlockSpawned;
    private List<MazeBlock> blocks = new List<MazeBlock>();
    private List<MazeBlock> blocksActivated = new List<MazeBlock>();
    private MazeBlock lastBlock;
    
    public float blockSize;
    private int currentBlock;
    private int iterations = 0;
    private Camera _cam;
    public GameObject key;
    public int keysCount = 1;
    public int obs1Count = 1;
    public int randomObsCount = 1;
    public int lazerObsCount = 1;
    public List<int> usedBlockIndexes = new List<int>();
    public List<int> usefulBlockIndexes = new List<int>();
    public List<int> freeBlocks = new List<int>();
    public List<int> forLazerBlocks = new List<int>();
    public List<int> keyBlocks = new List<int>();
    public List<int> lazerBlocks = new List<int>();
    private float timeCurrent = 0;
    private float multilazerDelay = 12.5f;
    public GameObject multiLazer;
    public GameObject nubik;
    public GameObject key1;
    public GameObject key2;
    public GameObject key3;
    public GameObject key4;
    public GameObject key1Fill;
    public GameObject key2Fill;
    public GameObject key3Fill;
    public GameObject key4Fill;
    public GameObject roades;
    private void Start()
    {
        height = GameManager.MazeSize;
        width = GameManager.MazeSize;
        obs1Count = height * width / 8;
        randomObsCount = height * width / 16;
        nubik.GetComponent<Nubik>().speed = 5 + (GameManager.MazeSize - 4) / 2;
        if (GameManager.MazeSize < 6)
        {
            keysCount = 1;
            lazerObsCount = 1;
        }
        else {
            if (GameManager.MazeSize < 8)
            {
                keysCount = 2;
                lazerObsCount = 2;
            }
            else {
                if (GameManager.MazeSize < 10)
                {
                    keysCount = 3;
                    lazerObsCount = 3;
                }
                else {
                    keysCount = 4;
                    lazerObsCount = 4;
                }
            }
        }
        for (int i = 1; i <= height; i++) {
            for (int j = 1; j <= width; j++)
            {
                lastBlock = Instantiate(mazeBlock, new Vector3(j*blockSize, 0, -i*blockSize), new Quaternion()).GetComponent<MazeBlock>();
                lastBlock.indexX = j;
                lastBlock.indexZ = i;
                lastBlock.mazeHeight = height;
                blocks.Add(lastBlock);
                lastBlock.index = blocks.IndexOf(lastBlock);
            }
        }
        
        currentBlock = Random.Range(0, blocks.Count);
        blocks[currentBlock].activated = true;
        blocksActivated.Add(blocks[currentBlock]);
        Generate();
        _cam = Camera.main;
        _cam.transform.position = new Vector3((width * blockSize/2) * 1.3f, 1 + (height * blockSize / 2) * 2.4f, -1 -(height * blockSize / 2) * 2.4f);
        if (GameManager.levelMode == 3)
        {
            GameManager.CamSizeStart = 7 + height * 1.65f;
        }
        else {
            GameManager.CamSizeStart = 4 + height * 1.6f;
        }
        blocks[0].leftOpen = true;
        blocks[blocks.Count - 1].endBlock = true;
        mazeFinalBlockSpawned = Instantiate(mazeFinalBlock, new Vector3((width + 1) * blockSize, 0, -height * blockSize), new Quaternion());
        SpawnKeys();
        GameManager.keysCurrent = 0;
        GameManager.keysNeed = keysCount;
        for (int i = 0; i < usedBlockIndexes.Count; i++) {
            FindUsefulWays(usedBlockIndexes[i], 3, new List<int>(), true);
            //Debug.Log(usedBlockIndexes[i]);
        }
        FindUsefulWays(blocks.Count-1, 3, new List<int>());
        UpdateUsefulBLocks();
        SpawnLazerObs();
        SpawnObs1();
        SpawnRandomObs();
        /*for (int i = 0; i<usefulBlockIndexes.Count; i++) {
            Debug.Log(usefulBlockIndexes[i]);
        }*/
        freeBlocks.Clear();
        for (int i = 0; i < blocks.Count - 1; i++)
        {
            freeBlocks.Add(i);
        }

        for (int i = 0; i < freeBlocks.Count; i++)
        {
            for (int j = 0; j < usedBlockIndexes.Count; j++)
            {
                if (freeBlocks[i] == usedBlockIndexes[j])
                {
                    freeBlocks.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
        switch (GameManager.levelMode) {
            case 1:
                nubik.SetActive(false);
                roades.SetActive(false);
                break;
            case 2:
                nubik.SetActive(false);
                roades.SetActive(false);
                break;
            case 3:
                nubik.SetActive(true);
                roades.SetActive(true);
                break;
        }
        timeCurrent = multilazerDelay - 5f;
    }

    private void Update()
    {
        if (GameManager.state == GameState.Game) {
            KeysHUD_Controller();
            if (GameManager.keysCurrent >= GameManager.keysNeed)
            {
                mazeFinalBlockSpawned.GetComponent<MazeBlock>().leftOpen = true;
            }
            else {
                mazeFinalBlockSpawned.GetComponent<MazeBlock>().leftOpen = false;
            }
            blocks[blocks.Count - 1].rightOpen = true;
            if (GameManager.levelMode == 2 || GameManager.levelMode == 3) {
                timeCurrent += 1 * Time.deltaTime;
                if (timeCurrent >= multilazerDelay)
                {
                    timeCurrent = 0;
                    if (freeBlocks.Count >= 1) {
                        int randomIndex = Random.Range(0, freeBlocks.Count);
                        Instantiate(multiLazer, blocks[freeBlocks[randomIndex]].transform.position, new Quaternion());
                    }
                    
                }
            }
            
        }
    }

    public void KeysHUD_Controller() {
        
        key1.SetActive(GameManager.keysNeed >= 1);
        key2.SetActive(GameManager.keysNeed >= 2);
        key3.SetActive(GameManager.keysNeed >= 3);
        key4.SetActive(GameManager.keysNeed >= 4);

        key1Fill.SetActive(GameManager.keysCurrent >= 1);
        key2Fill.SetActive(GameManager.keysCurrent >= 2);
        key3Fill.SetActive(GameManager.keysCurrent >= 3);
        key4Fill.SetActive(GameManager.keysCurrent >= 4);
    }

    public bool FindUsefulWays(int blockIndex, int lastSide, List<int> usefulBlocks, bool firstKeyBlock = false) {
        List<int> newUsefulBlocks = new List<int>();
        for (int i = 0; i < usefulBlocks.Count; i++)
        {
            newUsefulBlocks.Add(usefulBlocks[i]);
        }
        newUsefulBlocks.Add(blockIndex);
        bool canRight = false;
        bool canLeft = false;
        bool canUp = false;
        bool canDown = false;
        if (blockIndex != 0) {
            if (blocks[blockIndex].leftOpen == true && (lastSide != 1 || firstKeyBlock == true))
            {
                canLeft = FindUsefulWays(blockIndex - 1, 3, newUsefulBlocks);
            }
            if (blocks[blockIndex].rightOpen == true && (lastSide != 3 || firstKeyBlock == true))
            {
                canRight = FindUsefulWays(blockIndex + 1, 1, newUsefulBlocks);
            }
            if (blocks[blockIndex].upOpen == true && (lastSide != 2 || firstKeyBlock == true))
            {
                canUp = FindUsefulWays(blockIndex - width, 4, newUsefulBlocks);
            }
            if (blocks[blockIndex].downOpen == true && (lastSide != 4 || firstKeyBlock == true))
            {
                canDown = FindUsefulWays(blockIndex + width, 2, newUsefulBlocks);
            }
        }
        
        if (blockIndex == 0)
        {
            for (int i = 0; i < newUsefulBlocks.Count; i++) {
                bool existFlag = false;
                for (int j = 0; j < usefulBlockIndexes.Count; j++) {
                    if (newUsefulBlocks[i] == usefulBlockIndexes[j] || newUsefulBlocks[i] == height * width - 1) {
                        existFlag = true;
                        break;
                    }
                }
                if (existFlag == false) {
                    usefulBlockIndexes.Add(newUsefulBlocks[i]);
                }
                
            }
            
            return true;
            
        }
        else {
            return false;
            
        }
    }

    public void UpdateUsefulBLocks()
    {
        //usefulBlockIndexes = new HashSet<int>(usefulBlockIndexes).ToList();
        for (int i = 0; i < usefulBlockIndexes.Count; i++)
        {
            for (int j = 0; j < usedBlockIndexes.Count; j++)
            {
                if (usefulBlockIndexes[i] == usedBlockIndexes[j])
                {
                    usefulBlockIndexes.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
    }

    public void SpawnObs1() {
        for (int i = 0; i < obs1Count; i++) {
            int obsIndex = Random.Range(0, usefulBlockIndexes.Count);
            if (obsIndex >= 0 && obsIndex < usefulBlockIndexes.Count) {
                blocks[usefulBlockIndexes[obsIndex]].obs1 = true;
                usedBlockIndexes.Add(usefulBlockIndexes[obsIndex]);
                if (blocks[usefulBlockIndexes[obsIndex]].leftOpen == true)
                {
                    usedBlockIndexes.Add(usefulBlockIndexes[obsIndex] - 1);
                }
                if (blocks[usefulBlockIndexes[obsIndex]].rightOpen == true)
                {
                    usedBlockIndexes.Add(usefulBlockIndexes[obsIndex] + 1);
                }
                if (blocks[usefulBlockIndexes[obsIndex]].upOpen == true)
                {
                    usedBlockIndexes.Add(usefulBlockIndexes[obsIndex] - width);
                }
                if (blocks[usefulBlockIndexes[obsIndex]].downOpen == true)
                {
                    usedBlockIndexes.Add(usefulBlockIndexes[obsIndex] + width);
                }
                UpdateUsefulBLocks();
            }
            
        }
        
    }

    public void SpawnRandomObs() {
        freeBlocks.Clear();
        for (int i = 0; i < blocks.Count-1; i++) {
            freeBlocks.Add(i);
        }
        for (int h = 0; h < randomObsCount; h++)
        {
            for (int i = 0; i < freeBlocks.Count; i++)
            {
                for (int j = 0; j < usedBlockIndexes.Count; j++)
                {
                    if (freeBlocks[i] == usedBlockIndexes[j])
                    {
                        freeBlocks.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }
            if (freeBlocks.Count >= 1) {
                int obsIndex = Random.Range(0, freeBlocks.Count);
                blocks[freeBlocks[obsIndex]].obs1 = true;
                usedBlockIndexes.Add(freeBlocks[obsIndex]);
                if (blocks[freeBlocks[obsIndex]].leftOpen == true)
                {
                    usedBlockIndexes.Add(freeBlocks[obsIndex] - 1);
                }
                if (blocks[freeBlocks[obsIndex]].rightOpen == true)
                {
                    usedBlockIndexes.Add(freeBlocks[obsIndex] + 1);
                }
                if (blocks[freeBlocks[obsIndex]].upOpen == true)
                {
                    usedBlockIndexes.Add(freeBlocks[obsIndex] - width);
                }
                if (blocks[freeBlocks[obsIndex]].downOpen == true)
                {
                    usedBlockIndexes.Add(freeBlocks[obsIndex] + width);
                }
                UpdateUsefulBLocks();
            }
            
        }
    }

    public void SpawnLazerObs() {
        forLazerBlocks.Clear();
        for (int i = 1; i < usefulBlockIndexes.Count; i++)
        {
            forLazerBlocks.Add(usefulBlockIndexes[i]);
        }
        for (int h = 0; h < lazerObsCount; h++)
        {
            for (int i = 0; i < forLazerBlocks.Count; i++)
            {
                if ((blocks[forLazerBlocks[i]].rightOpen == blocks[forLazerBlocks[i]].leftOpen && blocks[forLazerBlocks[i]].upOpen == blocks[forLazerBlocks[i]].downOpen)) {
                    forLazerBlocks.RemoveAt(i);
                    i--;
                }
            }
            if (forLazerBlocks.Count < 1) {
                return;
            }
            int obsIndex = Random.Range(0, forLazerBlocks.Count);
            
            blocks[forLazerBlocks[obsIndex]].obs2 = true;
            usedBlockIndexes.Add(forLazerBlocks[obsIndex]);
            for (int k = 0; k < lazerBlocks.Count; k++)
            {
                if (lazerBlocks[k] == forLazerBlocks[obsIndex])
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                }
            }
                
           
            lazerBlocks.Add(forLazerBlocks[obsIndex]);
            
            if (blocks[forLazerBlocks[obsIndex]].leftOpen == true)
            {
                usedBlockIndexes.Add(forLazerBlocks[obsIndex] - 1);
            }
            if (blocks[forLazerBlocks[obsIndex]].rightOpen == true)
            {
                usedBlockIndexes.Add(forLazerBlocks[obsIndex] + 1);
            }
            if (blocks[forLazerBlocks[obsIndex]].upOpen == true)
            {
                usedBlockIndexes.Add(forLazerBlocks[obsIndex] - width);
            }
            if (blocks[forLazerBlocks[obsIndex]].downOpen == true)
            {
                usedBlockIndexes.Add(forLazerBlocks[obsIndex] + width);
            }
            int lazerLenght = 1;
            int maxLazerLenght = 0;
            int currentLazerLenght = 0;
            if (blocks[forLazerBlocks[obsIndex]].upOpen == false && blocks[forLazerBlocks[obsIndex]].downOpen == true)
            {
                while (true) {
                    if (blocks.Count > forLazerBlocks[obsIndex] + width * lazerLenght && blocks[forLazerBlocks[obsIndex] + width * lazerLenght].upOpen == true)
                    {
                        if (blocks[forLazerBlocks[obsIndex] + width * lazerLenght].rightOpen == true || blocks[forLazerBlocks[obsIndex] + width * lazerLenght].leftOpen == true)
                        {
                            if (currentLazerLenght > maxLazerLenght)
                            {
                                maxLazerLenght = currentLazerLenght;
                            }
                            currentLazerLenght = 0;
                        }
                        else
                        {
                            bool usefulFlag = false;
                            bool keyFlag = false;
                            
                            for (int k = 0; k < usefulBlockIndexes.Count; k++)
                            {
                                if (usefulBlockIndexes[k] == forLazerBlocks[obsIndex] + width * lazerLenght)
                                {
                                    usefulFlag = true;
                                    break;
                                }
                            }
                            for (int k = 0; k < keyBlocks.Count; k++)
                            {
                                if (keyBlocks[k] == forLazerBlocks[obsIndex] + width * lazerLenght)
                                {
                                    keyFlag = true;
                                    usefulFlag = true;
                                    break;
                                }
                            }
                            if (usefulFlag == true)
                            {
                                if (keyFlag == true)
                                {
                                    currentLazerLenght = currentLazerLenght * 2;
                                }
                                currentLazerLenght += 1;
                            }
                            
                        }
                        bool usedFlag = false;
                        for (int k = 0; k < lazerBlocks.Count; k++)
                        {
                            if (lazerBlocks[k] == forLazerBlocks[obsIndex] + width * lazerLenght)
                            {
                                usedFlag = true;
                                break;
                            }
                        }
                        if (usedFlag == true)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                        lazerBlocks.Add(forLazerBlocks[obsIndex] + width * lazerLenght);
                        lazerLenght += 1;
                        
                        usedBlockIndexes.Add(forLazerBlocks[obsIndex] + width * lazerLenght);
                        

                    }
                    else {
                        if (currentLazerLenght > maxLazerLenght)
                        {
                            maxLazerLenght = currentLazerLenght;
                        }
                        currentLazerLenght = 0;
                        break;
                    }
                }
            }
            else {
                if ((blocks[forLazerBlocks[obsIndex]].downOpen == false && blocks[forLazerBlocks[obsIndex]].upOpen == true) || (forLazerBlocks[obsIndex] == width * height - 1))
                {
                    while (true)
                    {
                        if (forLazerBlocks[obsIndex] - width * lazerLenght >= 0 && blocks.Count > forLazerBlocks[obsIndex] - width * lazerLenght && blocks[forLazerBlocks[obsIndex] - width * lazerLenght].downOpen == true)
                        {
                            if (blocks[forLazerBlocks[obsIndex] - width * lazerLenght].rightOpen == true || blocks[forLazerBlocks[obsIndex] - width * lazerLenght].leftOpen == true)
                            {
                                if (currentLazerLenght > maxLazerLenght)
                                {
                                    maxLazerLenght = currentLazerLenght;
                                }
                                currentLazerLenght = 0;
                            }
                            else
                            {
                                bool usefulFlag = false;
                                bool keyFlag = false;
                                
                                for (int k = 0; k < usefulBlockIndexes.Count; k++)
                                {
                                    if (usefulBlockIndexes[k] == forLazerBlocks[obsIndex] - width * lazerLenght)
                                    {
                                        usefulFlag = true;
                                        break;
                                    }
                                }
                                for (int k = 0; k < keyBlocks.Count; k++)
                                {
                                    if (keyBlocks[k] == forLazerBlocks[obsIndex] - width * lazerLenght)
                                    {
                                        usefulFlag = true;
                                        keyFlag = true;
                                        break;
                                    }
                                }
                                if (usefulFlag == true)
                                {
                                    if (keyFlag == true) {
                                        currentLazerLenght = currentLazerLenght * 2;
                                    }
                                    currentLazerLenght += 1;
                                }
                                
                            }
                            bool usedFlag = false;
                            for (int k = 0; k < lazerBlocks.Count; k++)
                            {
                                if (lazerBlocks[k] == forLazerBlocks[obsIndex] - width * lazerLenght)
                                {
                                    usedFlag = true;
                                    break;
                                }
                            }
                            if (usedFlag == true)
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            }
                            lazerBlocks.Add(forLazerBlocks[obsIndex] - width * lazerLenght);
                            lazerLenght += 1;
                            usedBlockIndexes.Add(forLazerBlocks[obsIndex] - width * lazerLenght);
                            
                        }
                        else
                        {
                            if (currentLazerLenght > maxLazerLenght)
                            {
                                maxLazerLenght = currentLazerLenght;
                            }
                            currentLazerLenght = 0;
                            break;
                        }
                    }
                }
                else
                {
                    if (blocks[forLazerBlocks[obsIndex]].leftOpen == false && blocks[forLazerBlocks[obsIndex]].rightOpen == true)
                    {
                        while (true)
                        {
                            if (forLazerBlocks[obsIndex] + 1 * lazerLenght >= 0 && blocks.Count > forLazerBlocks[obsIndex] + 1 * lazerLenght && blocks[forLazerBlocks[obsIndex] + 1 * lazerLenght].leftOpen == true)
                            {
                                if (blocks[forLazerBlocks[obsIndex] + 1 * lazerLenght].upOpen == true || blocks[forLazerBlocks[obsIndex] + 1 * lazerLenght].downOpen == true)
                                {
                                    if (currentLazerLenght > maxLazerLenght)
                                    {
                                        maxLazerLenght = currentLazerLenght;
                                    }
                                    currentLazerLenght = 0;
                                }
                                else
                                {
                                    bool usefulFlag = false;
                                    bool keyFlag = false;
                                    
                                    for (int k = 0; k < usefulBlockIndexes.Count; k++)
                                    {
                                        if (usefulBlockIndexes[k] == forLazerBlocks[obsIndex] + 1 * lazerLenght)
                                        {
                                            usefulFlag = true;
                                            break;
                                        }
                                    }
                                    for (int k = 0; k < keyBlocks.Count; k++)
                                    {
                                        if (keyBlocks[k] == forLazerBlocks[obsIndex] + 1 * lazerLenght)
                                        {
                                            usefulFlag = true;
                                            keyFlag = true;
                                            break;
                                        }
                                    }
                                    if (usefulFlag == true)
                                    {
                                        if (keyFlag == true)
                                        {
                                            currentLazerLenght = currentLazerLenght * 2;
                                        }
                                        currentLazerLenght += 1;
                                    }
                                    
                                }
                                bool usedFlag = false;
                                for (int k = 0; k < lazerBlocks.Count; k++)
                                {
                                    if (lazerBlocks[k] == forLazerBlocks[obsIndex] + 1 * lazerLenght)
                                    {
                                        usedFlag = true;
                                        break;
                                    }
                                }
                                if (usedFlag == true)
                                {
                                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                                }
                                lazerBlocks.Add(forLazerBlocks[obsIndex] + 1 * lazerLenght);
                                lazerLenght += 1;
                                usedBlockIndexes.Add(forLazerBlocks[obsIndex] + 1 * lazerLenght);
                                
                            }
                            else
                            {
                                if (currentLazerLenght > maxLazerLenght)
                                {
                                    maxLazerLenght = currentLazerLenght;
                                }
                                currentLazerLenght = 0;
                                break;
                            }
                        }
                    }
                    else
                    {
                        
                        if (blocks[forLazerBlocks[obsIndex]].rightOpen == false && blocks[forLazerBlocks[obsIndex]].leftOpen == true)
                        {
                            while (true)
                            {
                                if (forLazerBlocks[obsIndex] - 1 * lazerLenght >= 0 && blocks.Count > forLazerBlocks[obsIndex] - 1 * lazerLenght && blocks[forLazerBlocks[obsIndex] - 1 * lazerLenght].rightOpen == true)
                                {
                                    if (blocks[forLazerBlocks[obsIndex] - 1 * lazerLenght].upOpen == true || blocks[forLazerBlocks[obsIndex] - 1 * lazerLenght].downOpen == true)
                                    {
                                        if (currentLazerLenght > maxLazerLenght)
                                        {
                                            maxLazerLenght = currentLazerLenght;
                                        }
                                        currentLazerLenght = 0;
                                    }
                                    else
                                    {
                                        bool usefulFlag = false;
                                        bool keyFlag = false;
                                        
                                        for (int k = 0; k < usefulBlockIndexes.Count; k++)
                                        {
                                            if (usefulBlockIndexes[k] == forLazerBlocks[obsIndex] - 1 * lazerLenght)
                                            {
                                                usefulFlag = true;
                                                break;
                                            }
                                        }
                                        for (int k = 0; k < keyBlocks.Count; k++)
                                        {
                                            if (keyBlocks[k] == forLazerBlocks[obsIndex] - 1 * lazerLenght)
                                            {
                                                keyFlag = true;
                                                usefulFlag = true;
                                                break;
                                            }
                                        }
                                        if (usefulFlag == true)
                                        {
                                            if (keyFlag == true)
                                            {
                                                currentLazerLenght = currentLazerLenght * 2;
                                            }
                                            currentLazerLenght += 1;
                                        }
                                        
                                    }
                                    bool usedFlag = false;
                                    for (int k = 0; k < lazerBlocks.Count; k++)
                                    {
                                        if (lazerBlocks[k] == forLazerBlocks[obsIndex] - 1 * lazerLenght)
                                        {
                                            usedFlag = true;
                                            break;
                                        }
                                    }
                                    if (usedFlag == true)
                                    {
                                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                                    }
                                    lazerBlocks.Add(forLazerBlocks[obsIndex] - 1 * lazerLenght);
                                    lazerLenght += 1;
                                    usedBlockIndexes.Add(forLazerBlocks[obsIndex] - 1 * lazerLenght);
                                    

                                }
                                else
                                {
                                    if (currentLazerLenght > maxLazerLenght)
                                    {
                                        maxLazerLenght = currentLazerLenght;
                                    }
                                    currentLazerLenght = 0;
                                    break;
                                }
                            }
                            
                        }
                    }
                }
            }
            UpdateUsefulBLocks();
            blocks[forLazerBlocks[obsIndex]].lazerLenght = lazerLenght;
            blocks[forLazerBlocks[obsIndex]].lazerMaxLenght = maxLazerLenght;
        }
    }

    public void SpawnKeys() {
        int blockX;
        int blockY;
        int blockKeyIndex;
        switch (keysCount) {
            case 1:
                int dice = Random.Range(0,2);
                if (dice == 1)
                {
                    blockX = width / 2 + Random.Range(1, width / 2+1);
                    blockY = Random.Range(1, height / 2);
                    blockKeyIndex = width * (blockY - 1) + blockX - 1;
                    usedBlockIndexes.Add(blockKeyIndex);
                    keyBlocks.Add(blockKeyIndex);
                    Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                }
                else {
                    blockX = Random.Range(1, width / 2 + 1);
                    blockY = height/2 + Random.Range(1, height / 2+1);
                    blockKeyIndex = width * (blockY - 1) + blockX - 1;
                    usedBlockIndexes.Add(blockKeyIndex);
                    keyBlocks.Add(blockKeyIndex);
                    Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                }
                break;
            case 2:
                blockX = width / 2 + Random.Range(1, width / 2 + 1);
                blockY = Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = Random.Range(1, width / 2 + 1);
                blockY = height / 2 + Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                break;
            case 3:
                blockX = width / 2 + Random.Range(1, width / 2 + 1);
                blockY = Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = Random.Range(1, width / 2 + 1);
                blockY = height / 2 + Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = width / 2 + Random.Range(1, width / 2 + 1);
                blockY = height / 2 + Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                break;
            case 4:
                blockX = width / 2 + Random.Range(1, width / 2 + 1);
                blockY = Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = Random.Range(1, width / 2 + 1);
                blockY = height / 2 + Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = width / 2 + Random.Range(1, width / 2 + 1);
                blockY = height / 2 + Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                blockX = Random.Range(1, width / 2 + 1);
                blockY = Random.Range(1, height / 2 + 1);
                blockKeyIndex = width * (blockY - 1) + blockX - 1;
                usedBlockIndexes.Add(blockKeyIndex);
                keyBlocks.Add(blockKeyIndex);
                Instantiate(key, blocks[blockKeyIndex].transform.position, new Quaternion());
                break;
        }
    }

    public void Generate() {
        UpdateSidesBLocks();
        while (blocksActivated.Count > 0 && iterations < 10000) {
            UpdateActivatedBlocks();
            ActivateRandomBlock();
            iterations++;
        }

    }

    public void UpdateActivatedBlocks() {
        UpdateSidesBLocks();
        blocksActivated.Clear();
        for (int i = 0; i < blocks.Count; i ++) {
            if (blocks[i].activated == true && (blocks[i].right == true || blocks[i].left == true || blocks[i].up == true || blocks[i].down == true)) {
                blocksActivated.Add(blocks[i]);
            }
        }
    }

    public void ActivateRandomBlock() {

        if (blocksActivated.Count >= 1) {
            int newBlockIndex = Random.Range(0, blocksActivated.Count);
            currentBlock = blocks.IndexOf(blocksActivated[newBlockIndex]);
            int newSide = blocks[currentBlock].ChooseRandomSide();
            switch (newSide)
            {
                case 1:
                    blocks[currentBlock].leftOpen = true;
                    blocks[currentBlock - 1].rightOpen = true;
                    blocks[currentBlock - 1].activated = true;
                    break;
                case 2:
                    blocks[currentBlock].upOpen = true;
                    blocks[currentBlock - width].downOpen = true;
                    blocks[currentBlock - width].activated = true;
                    break;
                case 3:
                    blocks[currentBlock].rightOpen = true;
                    blocks[currentBlock + 1].leftOpen = true;
                    blocks[currentBlock + 1].activated = true;
                    break;
                case 4:
                    blocks[currentBlock].downOpen = true;
                    blocks[currentBlock + width].upOpen = true;
                    blocks[currentBlock + width].activated = true;
                    break;
            }
        }
        
    }

    public void UpdateSidesBLocks() {
        for (int i = 0; i < blocks.Count; i++)
        {
            if (i % width != 0 && blocks[i - 1].activated == false)
            {
                blocks[i].left = true;
            }
            else {
                blocks[i].left = false;
            }
            if (i % width != width - 1 && blocks[i + 1].activated == false)
            {
                blocks[i].right = true;
            }
            else {
                blocks[i].right = false;
            }
            if (i / width >= 1 && blocks[i - width].activated == false)
            {
                blocks[i].up = true;
            }
            else
            {
                blocks[i].up = false;
            }
            if ((i) / width < height - 1 && blocks[i + width].activated == false)
            {
                blocks[i].down = true;
            }
            else
            {
                blocks[i].down = false;
            }

        }
        
    }
}