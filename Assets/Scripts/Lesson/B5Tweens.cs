using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B5Tweens : MonoBehaviour
{
    [SerializeField] private GameObject keyword;
    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject wrong;
    [SerializeField] private GameObject x;
    [SerializeField] private GameObject check;

    [SerializeField] private float wrongtarget;
    [SerializeField] private float correcttarget;

    [SerializeField] private float delay;

    private void Awake() {
        keyword.GetComponent<RectTransform>().localScale = Vector3.zero;
        correct.GetComponent<RectTransform>().localScale = Vector3.zero;
        wrong.GetComponent<RectTransform>().localScale = Vector3.zero;

        x.GetComponent<RectTransform>().localScale = Vector3.zero;
        check.GetComponent<RectTransform>().localScale = Vector3.zero;


        LeanTween.moveX(correct.GetComponent<RectTransform>(), 0, 0f);
        LeanTween.moveX(wrong.GetComponent<RectTransform>(), 0, 0f);
    }

    private void OnEnable() {
        LeanTween.scale(keyword, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(wrong, Vector3.one, 1f).setDelay(delay + 2f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(x, Vector3.one, 0.0001f).setDelay(delay + 3f);

        LeanTween.moveX(wrong.GetComponent<RectTransform>(), wrongtarget, 1f).setDelay(delay + 4f);

        LeanTween.scale(correct, Vector3.one, 1f).setDelay(delay + 5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(check, Vector3.one, 0.0001f).setDelay(delay + 6f);

        LeanTween.moveX(correct.GetComponent<RectTransform>(), correcttarget, 1f).setDelay(delay + 7f);
    }

    private void OnDisable() {
        LeanTween.reset();
        keyword.GetComponent<RectTransform>().localScale = Vector3.zero;
        correct.GetComponent<RectTransform>().localScale = Vector3.zero;
        wrong.GetComponent<RectTransform>().localScale = Vector3.zero;

        x.GetComponent<RectTransform>().localScale = Vector3.zero;
        check.GetComponent<RectTransform>().localScale = Vector3.zero;

        LeanTween.moveX(correct.GetComponent<RectTransform>(), 0, 0f);
        LeanTween.moveX(wrong.GetComponent<RectTransform>(), 0, 0f);
    }
}
