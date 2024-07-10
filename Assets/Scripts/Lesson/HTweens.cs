using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HTweens : MonoBehaviour
{   
    [SerializeField] private float delay2;
    [SerializeField] private float delay3;
    [SerializeField] private float delay4;
    [SerializeField] private float delay5;
    [SerializeField] private GameObject DialogueText;
    [SerializeField] private GameObject FreeToLearn;
    [SerializeField] private GameObject EasyToLearn;
    [SerializeField] private GameObject Creates;
    [SerializeField] private GameObject Tools;
    [SerializeField] private GameObject Salary;

    [SerializeField] private int newFontS;
    [SerializeField] private Vector2 newWxH;
    [SerializeField] private Vector3 newPosition;

    private Vector2 OrigWxH;
    private Vector3 OriginalPos;
    private int OrigFontS;

    private void Awake() {
        OriginalPos = DialogueText.GetComponent<RectTransform>().anchoredPosition;
        OrigWxH = DialogueText.GetComponent<RectTransform>().sizeDelta;
        OrigFontS = DialogueText.GetComponent<Text>().fontSize;

        FreeToLearn.GetComponent<RectTransform>().localScale = Vector3.zero;
        EasyToLearn.GetComponent<RectTransform>().localScale = Vector3.zero;
        Creates.GetComponent<RectTransform>().localScale = Vector3.zero;
        Tools.GetComponent<RectTransform>().localScale = Vector3.zero;
        Salary.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void OnEnable() {
        DialogueText.GetComponent<RectTransform>().anchoredPosition = newPosition;
        DialogueText.GetComponent<RectTransform>().sizeDelta = newWxH;
        DialogueText.GetComponent<Text>().fontSize = newFontS;

        LeanTween.scale(FreeToLearn, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(EasyToLearn, Vector3.one, 1f).setDelay(delay2).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Creates, Vector3.one, 1f).setDelay(delay3).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Tools, Vector3.one, 1f).setDelay(delay4).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Salary, Vector3.one, 1f).setDelay(delay5).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable(){
        LeanTween.reset();

        DialogueText.GetComponent<RectTransform>().anchoredPosition = OriginalPos;
        DialogueText.GetComponent<RectTransform>().sizeDelta = OrigWxH;
        DialogueText.GetComponent<Text>().fontSize = OrigFontS;

        FreeToLearn.GetComponent<RectTransform>().localScale = Vector3.zero;
        EasyToLearn.GetComponent<RectTransform>().localScale = Vector3.zero;
        Creates.GetComponent<RectTransform>().localScale = Vector3.zero;
        Tools.GetComponent<RectTransform>().localScale = Vector3.zero;
        Salary.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
