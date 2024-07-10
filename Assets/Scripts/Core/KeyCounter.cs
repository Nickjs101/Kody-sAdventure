using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyCounter : MonoBehaviour
{
    [SerializeField] private Text keyText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Keys", 0);

    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = Convert.ToString(PlayerPrefs.GetInt("Keys"));
    }
}
