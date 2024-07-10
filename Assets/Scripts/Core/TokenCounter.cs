using UnityEngine;
using UnityEngine.UI;
using System;

public class TokenCounter : MonoBehaviour
{
    [SerializeField] private Text TokenText;

    private int Tokens;

    private void Awake() {
        PlayerPrefs.SetInt("CurrentTokens", 0);
        Tokens = Convert.ToInt32(TokenText.text);
    }
    private void Update() {
        if(PlayerPrefs.GetInt("CurrentTokens") != Tokens){
           TokenText.text = Convert.ToString(PlayerPrefs.GetInt("CurrentTokens"));
        }
    }

    public void GetTokens() {
        PlayerPrefs.SetInt("CurrentTokens", PlayerPrefs.GetInt("Tokens") + PlayerPrefs.GetInt("CurrentTokens"));
    }
}
