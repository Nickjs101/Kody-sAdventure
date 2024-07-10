using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTweens : MonoBehaviour
{
    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private GameObject english;

    private void Awake() {
        Logo.GetComponent<RectTransform>().localScale = Vector3.zero;
        Arrow.GetComponent<RectTransform>().localScale = Vector3.zero;
        english.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    
    private void OnEnable() {
        LeanTween.scale(Logo, Vector3.one, 2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Arrow, Vector3.one, 2f).setDelay(2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(english, Vector3.one, 2f).setDelay(4f).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable() {
        LeanTween.reset();
        Logo.GetComponent<RectTransform>().localScale = Vector3.zero;
        Arrow.GetComponent<RectTransform>().localScale = Vector3.zero;
        english.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
