using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C8Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Cloud;
    [SerializeField] private GameObject John;
    [SerializeField] private GameObject Rose;
    [SerializeField] private GameObject Name;

    [SerializeField] private float delay;

    private void Awake() {
        Cloud.GetComponent<RectTransform>().localScale = Vector3.zero;
        John.GetComponent<RectTransform>().localScale = Vector3.zero;
        Rose.GetComponent<RectTransform>().localScale = Vector3.zero;
        Name.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void OnEnable() {
        LeanTween.scale(John, Vector3.one, 1f).setDelay(delay);
        LeanTween.scale(Rose, Vector3.one, 1f).setDelay(delay + 1);
        LeanTween.scale(Name, Vector3.one, 1f).setDelay(delay + 3).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Cloud, Vector3.one, 1f).setDelay(delay + 4).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable() {
        LeanTween.reset();

        Cloud.GetComponent<RectTransform>().localScale = Vector3.zero;
        John.GetComponent<RectTransform>().localScale = Vector3.zero;
        Rose.GetComponent<RectTransform>().localScale = Vector3.zero;
        Name.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
