using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F2Tweens : MonoBehaviour
{
    [SerializeField] private GameObject data1;
    [SerializeField] private GameObject data2;

    [SerializeField] private float TargetPos1;
    [SerializeField] private float TargetPos2;

    [SerializeField] private Vector3 OrigPos1;
    [SerializeField] private Vector3 OrigPos2;

    private void Awake() {
        LeanTween.alpha(data1.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(data2.GetComponent<RectTransform>(), 0f, 0f);

        OrigPos1 = data1.GetComponent<RectTransform>().position;
        OrigPos2 = data2.GetComponent<RectTransform>().position;
    }

    private void OnEnable() {
        LeanTween.alpha(data1.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.alpha(data2.GetComponent<RectTransform>(), 1f, 2f).setDelay(4);

        LeanTween.moveX(data1.GetComponent<RectTransform>(), TargetPos1, 2f);
        LeanTween.moveX(data2.GetComponent<RectTransform>(), TargetPos2, 2f).setDelay(4);

    }

    private void OnDisable() {
        LeanTween.reset();

        LeanTween.alpha(data1.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(data2.GetComponent<RectTransform>(), 0f, 0f);

        data1.GetComponent<RectTransform>().position = OrigPos1;
        data2.GetComponent<RectTransform>().position = OrigPos2;
    }
}
