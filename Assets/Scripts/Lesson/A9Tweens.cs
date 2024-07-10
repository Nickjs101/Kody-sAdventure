using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A9Tweens : MonoBehaviour
{
    [SerializeField] private GameObject printtool;
    [SerializeField] private GameObject inputTool;

    [SerializeField] private float delay;

    private Vector3 printtoolPos;
    private Vector3 inputToolPos;

    private void Awake() {
        printtool.GetComponent<RectTransform>().localScale = Vector3.zero;
        inputTool.GetComponent<RectTransform>().localScale = Vector3.zero;

        printtoolPos = printtool.GetComponent<RectTransform>().position;
        inputToolPos = printtool.GetComponent<RectTransform>().position;
    }

    private void OnEnable() {
        LeanTween.scale(printtool, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(inputTool, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);

        LeanTween.moveX(inputTool.GetComponent<RectTransform>(), -32f, 1f).setDelay(delay + 2);
        LeanTween.moveX(printtool.GetComponent<RectTransform>(), -678f, 1f).setDelay(delay + 2.75f);
        LeanTween.scale(printtool, Vector3.zero, 0f).setDelay(delay + 3.75f);
    }

    private void OnDisable() {
        LeanTween.reset();
        
        printtool.GetComponent<RectTransform>().localScale = Vector3.zero;
        inputTool.GetComponent<RectTransform>().localScale = Vector3.zero;

        printtool.GetComponent<RectTransform>().position = printtoolPos;
        printtool.GetComponent<RectTransform>().position = inputToolPos;
    }
}
