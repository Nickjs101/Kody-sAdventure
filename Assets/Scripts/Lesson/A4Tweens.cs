using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A4Tweens : MonoBehaviour
{
    [SerializeField] private InputField input1;
    [SerializeField] private InputField input2;
    [SerializeField] private InputField input3;
    [SerializeField] private Text OutputText;

    private void Awake() {
        input1.text = "";
        input2.text = "";
        input3.text = "";
    }

    public void Run() {
        OutputText.text = input1.text + "\n" + input2.text + "\n" + input3.text;
    }
}
