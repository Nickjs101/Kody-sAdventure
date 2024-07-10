using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2Tweens : MonoBehaviour
{
    [SerializeField] private GameObject Web1;
    [SerializeField] private GameObject Web2;
    [SerializeField] private GameObject Left;
    [SerializeField] private GameObject Right;
    [SerializeField] private GameObject Laptop;
    [SerializeField] private GameObject Setting1;
    [SerializeField] private GameObject Setting2;

    [SerializeField] private float TargetPosleft;
    [SerializeField] private float TargetPosright;

    [SerializeField] private Vector3 OrigPos1;
    [SerializeField] private Vector3 OrigPos2;
    void Awake()
    {
        Laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web1.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web2.GetComponent<RectTransform>().localScale = Vector3.zero;


        LeanTween.alpha(Left.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(Right.GetComponent<RectTransform>(), 0f, 0f);
        OrigPos1 = Left.GetComponent<RectTransform>().position;
        OrigPos2 = Right.GetComponent<RectTransform>().position;
    }

    private void OnEnable() {
        LeanTween.scale(Web2, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Web1, Vector3.one, 1f).setDelay(1f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(Laptop, Vector3.one, 1f).setDelay(2.5f).setEase(LeanTweenType.easeOutBack);

        LeanTween.moveX(Left.GetComponent<RectTransform>(), TargetPosleft, 1f).setDelay(3.5f);
        LeanTween.alpha(Left.GetComponent<RectTransform>(), 1f, 1f).setDelay(3.5f);

        LeanTween.moveX(Right.GetComponent<RectTransform>(), TargetPosright, 1f).setDelay(4.5f);
        LeanTween.alpha(Right.GetComponent<RectTransform>(), 1f, 1f).setDelay(4.5f);

        LeanTween.rotateAround(Setting1, Vector3.forward, -360, 5f).setDelay(5.5f).setLoopClamp();
        LeanTween.rotateAround(Setting2, Vector3.forward, 360, 5f).setDelay(5.5f).setLoopClamp();


    }

    private void OnDisable()
    {
        LeanTween.reset();

        Laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web1.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web2.GetComponent<RectTransform>().localScale = Vector3.zero;

        LeanTween.alpha(Left.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(Right.GetComponent<RectTransform>(), 0f, 0f);

        Left.GetComponent<RectTransform>().position = OrigPos1;
        Right.GetComponent<RectTransform>().position = OrigPos2;
    }
}
