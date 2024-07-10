using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B4Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    [SerializeField] private float delay;
    private void Awake() {
        LeanTween.scale(Obj, Vector3.zero, 0f);
    }

    private void OnEnable() {
        LeanTween.scale(Obj, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);
    }

    private void OnDisable() {
        LeanTween.reset();
        LeanTween.scale(Obj, Vector3.zero, 0f);
    }
}
