using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private float delay = 0;

    private void Awake() {
        Obj.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void OnEnable() {
        LeanTween.scale(Obj, Vector3.one, 1f).setDelay(.25f + delay).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable() {
        LeanTween.reset();
        Obj.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
