using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B6Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Val;
    [SerializeField] private GameObject var;
    [SerializeField] private GameObject identify;
    [SerializeField] private GameObject KeyValue;

    [SerializeField] private float xPos;

    [SerializeField] private Vector3 targetPos;

    [SerializeField] private float delay;

    private Vector3 origPos;

    [SerializeField] private GameObject SecondObject;

    [SerializeField] private InputField inputs;
    [SerializeField] private Text OutputText;

    [SerializeField] private string name;




    private void Awake() {
        origPos = KeyValue.GetComponent<RectTransform>().position;

        Val.GetComponent<RectTransform>().localScale = Vector3.zero;
        var.GetComponent<RectTransform>().localScale = Vector3.zero;
        LeanTween.alpha(identify.GetComponent<RectTransform>(), 0f, 0f);

        LeanTween.alpha(SecondObject.GetComponent<RectTransform>(), 0f, 0f);

        inputs.text = name;

        OutputText.text = "";
    }

    private void OnEnable() {
        LeanTween.scale(Val, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);
        LeanTween.moveX(Val.GetComponent<RectTransform>(), xPos, 1f).setDelay(delay + 1f);

        LeanTween.scale(var, Vector3.one, 1f).setDelay(delay + 2f).setEase(LeanTweenType.easeOutBack);

        LeanTween.alpha(identify.GetComponent<RectTransform>(), 1f, 1f).setDelay(delay + 3f);
        
        LeanTween.alpha(identify.GetComponent<RectTransform>(), 0f, 1f).setDelay(delay + 5f);

        LeanTween.move(KeyValue.GetComponent<RectTransform>(), targetPos, 1f).setDelay(delay + 7f);

        LeanTween.alpha(SecondObject.GetComponent<RectTransform>(), 1f, 1f).setDelay(delay + 8f);

        

    }

    private void OnDisable() {
        LeanTween.reset();

        KeyValue.GetComponent<RectTransform>().position = origPos;

        Val.GetComponent<RectTransform>().localScale = Vector3.zero;
        var.GetComponent<RectTransform>().localScale = Vector3.zero;
        LeanTween.alpha(identify.GetComponent<RectTransform>(), 0f, 0f);

        LeanTween.alpha(SecondObject.GetComponent<RectTransform>(), 0f, 0f);

        inputs.text = name;
        OutputText.text = "";

    }

    public void Run() {
        OutputText.text = inputs.text;
    }
}
