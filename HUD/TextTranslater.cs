using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTranslater : MonoBehaviour
{
    [SerializeField] string _en;
    [SerializeField] string _ru;
    // Start is called before the first frame update
    void Start()
    {
        if (Language.Instance.CurrentLanguage == "en")
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = _en;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
