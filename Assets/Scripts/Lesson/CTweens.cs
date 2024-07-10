using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTweens : MonoBehaviour
{
    [SerializeField] private GameObject Web1;
    [SerializeField] private GameObject Web2;
    [SerializeField] private GameObject Laptop;
    [SerializeField] private GameObject program;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject arrowright;
    [SerializeField] private GameObject arrowup;
    [SerializeField] private GameObject arrowdownright;
    [SerializeField] private GameObject animationmovement;

    
    [SerializeField] private GameObject Setting1;
    [SerializeField] private GameObject Setting2;

    private Vector3 OrigPos;

    private void Awake() {
        Web1.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web2.GetComponent<RectTransform>().localScale = Vector3.zero;

        LeanTween.alpha(program.GetComponent<RectTransform>(), 0f, .0000001f);

        arrowright.GetComponent<RectTransform>().localScale = Vector3.zero;
        arrowup.GetComponent<RectTransform>().localScale = Vector3.zero;
        arrowdownright.GetComponent<RectTransform>().localScale = Vector3.zero;

        OrigPos = animationmovement.GetComponent<RectTransform>().position;

        LeanTween.alpha(cursor.GetComponent<RectTransform>(), 0f, .0001f);

        Laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    private void OnEnable() {
        LeanTween.scale(Web1, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutBack);
        
        //move cursor the web1
        LeanTween.alpha(cursor.GetComponent<RectTransform>(), 1f, 1f).setDelay(1.5f);
        LeanTween.move(cursor.GetComponent<RectTransform>(), new Vector3(-11f, 242f, 0f), 1f).setDelay(1.5f);

        //cursor click effect
        LeanTween.scale(cursor, new Vector3(.9f, .9f, .9f), .5f).setDelay(2.5f);
        LeanTween.scale(cursor, Vector3.one, .5f).setDelay(3f);

        //transition
        LeanTween.moveX(animationmovement.GetComponent<RectTransform>(), -230f, 2f).setDelay(3.5f);
        LeanTween.scale(arrowright, Vector3.one, 1f).setDelay(4.5f);
        LeanTween.alpha(program.GetComponent<RectTransform>(), 1f, 1f).setDelay(5.5f);

        //remove cursor
        LeanTween.alpha(cursor.GetComponent<RectTransform>(), 0f, 1f).setDelay(3.5f);

        LeanTween.scale(arrowdownright, Vector3.one, 1f).setDelay(6.5f);
        LeanTween.scale(Laptop, Vector3.one, 1f).setDelay(7.5f);
        LeanTween.rotateAround(Setting1, Vector3.forward, -360, 5f).setDelay(8.5f).setLoopClamp();
        LeanTween.rotateAround(Setting2, Vector3.forward, 360, 5f).setDelay(8.5f).setLoopClamp();
        
        LeanTween.scale(arrowup, Vector3.one, .5f).setDelay(9.5f);
        LeanTween.scale(Web1, Vector3.zero, 1f).setDelay(10f);
        LeanTween.scale(Web2, Vector3.one, 1f).setDelay(11f);

    }

    private void OnDisable() {
        LeanTween.reset();

        Web1.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web2.GetComponent<RectTransform>().localScale = Vector3.zero;

        LeanTween.alpha(program.GetComponent<RectTransform>(), 0f, .01f);

        arrowright.GetComponent<RectTransform>().localScale = Vector3.zero;
        arrowup.GetComponent<RectTransform>().localScale = Vector3.zero;
        arrowdownright.GetComponent<RectTransform>().localScale = Vector3.zero;

        animationmovement.GetComponent<RectTransform>().position = OrigPos;

        LeanTween.alpha(cursor.GetComponent<RectTransform>(), 0f, .01f);

        Laptop.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

}
