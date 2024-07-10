using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B7Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Editor;

    [SerializeField] private float TargetPos;

    [SerializeField] private Vector3 OrigPos;


    private void Awake() {
        LeanTween.alpha(Editor.GetComponent<RectTransform>(), 0f, 0f);

        OrigPos = Editor.GetComponent<RectTransform>().position;
    }

    private void OnEnable() {
        LeanTween.alpha(Editor.GetComponent<RectTransform>(), 1f, 1f);
        LeanTween.moveY(Editor.GetComponent<RectTransform>(), TargetPos, 2f);
    }

    private void OnDisable() {
        LeanTween.reset();

        LeanTween.alpha(Editor.GetComponent<RectTransform>(), 0f, 0f);
        Editor.GetComponent<RectTransform>().position = OrigPos;
    }
}
