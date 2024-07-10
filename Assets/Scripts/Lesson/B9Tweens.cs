using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B9Tweens : MonoBehaviour
{
    [SerializeField] private InputField nameField;

    [SerializeField] private Text outputField;


    private void Awake() {
        nameField.text = "";
    }

    public void Enter(){
        outputField.text = nameField.text;
    }
}
