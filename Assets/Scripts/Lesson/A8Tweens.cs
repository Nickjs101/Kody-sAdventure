using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A8Tweens : MonoBehaviour
{
    [SerializeField] private GameObject VAR;
    [SerializeField] private GameObject VAL;
    
    private void Awake() {
        VAR.GetComponent<RectTransform>().localScale = Vector3.zero;        
        VAL.GetComponent<RectTransform>().localScale = Vector3.zero;        
    }

    private void OnEnable() {
        LeanTween.scale(VAR, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(VAL, Vector3.one, 1f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable() {
        LeanTween.reset();

        VAR.GetComponent<RectTransform>().localScale = Vector3.zero;        
        VAL.GetComponent<RectTransform>().localScale = Vector3.zero;     
    }
}
