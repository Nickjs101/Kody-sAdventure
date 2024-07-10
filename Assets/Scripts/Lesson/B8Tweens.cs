using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B8Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Box;
    [SerializeField] private GameObject Bat;

    [SerializeField] private float delay;


    private Vector3 BatPos;
    private Vector3 BatTargetPos;
    private Vector3 boxPos;

    private void Awake() {
        Box.GetComponent<RectTransform>().localScale = Vector3.zero;
        Bat.GetComponent<RectTransform>().localScale = Vector3.zero;

        boxPos = Box.GetComponent<RectTransform>().anchoredPosition;
        BatPos = Bat.GetComponent<RectTransform>().anchoredPosition;
        BatTargetPos = new Vector3(boxPos.x, boxPos.y + 50f, boxPos.z);   
    }

    private void OnEnable() {
        LeanTween.scale(Box, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Bat, Vector3.one, 1f).setDelay(delay + 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.move(Bat.GetComponent<RectTransform>(), BatTargetPos, 1f).setDelay(delay + 2f);
    }

    private void OnDisable() {
        LeanTween.reset();

        Box.GetComponent<RectTransform>().localScale = Vector3.zero;
        Bat.GetComponent<RectTransform>().localScale = Vector3.zero;

        Bat.GetComponent<RectTransform>().anchoredPosition = BatPos;
    }
}
