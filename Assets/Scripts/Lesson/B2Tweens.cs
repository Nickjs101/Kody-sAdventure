using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B2Tweens : MonoBehaviour
{
    [SerializeField] private GameObject P3;
    [SerializeField] private GameObject P2;
    [SerializeField] private GameObject PSF;

    private Vector3 P3Pos;
    private Vector3 P2Pos;

    private void Awake() {
        P3.GetComponent<RectTransform>().localScale = Vector3.zero;
        P2.GetComponent<RectTransform>().localScale = Vector3.zero;
        PSF.GetComponent<RectTransform>().localScale = Vector3.zero;

        P3Pos = P3.GetComponent<RectTransform>().position;
        P2Pos = P3.GetComponent<RectTransform>().position;

        
    }
     
    private void OnEnable()
    {
        LeanTween.scale(PSF, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(P3, Vector3.one, 1f).setDelay(4f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(P2, Vector3.one, 1f).setDelay(5f).setEase(LeanTweenType.easeOutBack);

        LeanTween.moveX(P3.GetComponent<RectTransform>(), 34f, 1f).setDelay(6f);
        LeanTween.moveX(P2.GetComponent<RectTransform>(), 678f, 1f).setDelay(6.75f);
        LeanTween.scale(P2, Vector3.zero, 0.000001f).setDelay(7.75f);
    }

    private void OnDisable()
    {
        LeanTween.reset();
        
        P3.GetComponent<RectTransform>().localScale = Vector3.zero;
        P2.GetComponent<RectTransform>().localScale = Vector3.zero;
        PSF.GetComponent<RectTransform>().localScale = Vector3.zero;

        P3.GetComponent<RectTransform>().position = P3Pos;
        P3.GetComponent<RectTransform>().position = P2Pos;
    }
}
