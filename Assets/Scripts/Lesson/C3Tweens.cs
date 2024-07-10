using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C3Tweens : MonoBehaviour
{
    [SerializeField] private InputField statement;
    [SerializeField] private InputField comment;
    [SerializeField] private Text OutputText;

    private void Awake() {
        statement.text = "";
        comment.text = "";
    }

    public void Run() {
        OutputText.text = statement.text;
    }
}
