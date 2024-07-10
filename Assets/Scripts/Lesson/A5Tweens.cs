using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A5Tweens : MonoBehaviour
{
    [SerializeField] private GameObject obj1;
    [SerializeField] private GameObject obj2;
    [SerializeField] private float midposX;
    [SerializeField] private float obj1TargetPosX;
    [SerializeField] private float obj2TargetPosX;
    [SerializeField] private float delay;

    private float OriginalPositionX;

    private float obj1OrigPos;
    private float obj2OrigPos;

    
    private void Awake() {


        OriginalPositionX = gameObject.GetComponent<RectTransform>().position.x;
        obj1OrigPos = obj1.GetComponent<RectTransform>().position.x;
        obj2OrigPos = obj2.GetComponent<RectTransform>().position.x;

    }

    private void OnEnable() {
        LeanTween.moveX(gameObject.GetComponent<RectTransform>(), midposX, 2f).setDelay(delay);

        LeanTween.moveX(obj1.GetComponent<RectTransform>(), obj1TargetPosX, 2f).setDelay(delay + 3f);
        
        LeanTween.moveX(obj2.GetComponent<RectTransform>(), obj2TargetPosX, 1f).setDelay(delay + 5f);
        LeanTween.scale(obj2.GetComponent<RectTransform>(), new Vector3(1.5f, 1.5f, 1.5f), .5f).setDelay(delay + 6.5f);
        LeanTween.scale(obj2.GetComponent<RectTransform>(), Vector3.one, .5f).setDelay(delay + 7f);
    }

    private void OnDisable() {
        gameObject.GetComponent<RectTransform>().position = new Vector3(OriginalPositionX, gameObject.GetComponent<RectTransform>().position.y, gameObject.GetComponent<RectTransform>().position.z);
        obj1.GetComponent<RectTransform>().position = new Vector3(obj1OrigPos, obj1.GetComponent<RectTransform>().position.y, obj1.GetComponent<RectTransform>().position.z);
        obj2.GetComponent<RectTransform>().position = new Vector3(obj2OrigPos, obj2.GetComponent<RectTransform>().position.y, obj2.GetComponent<RectTransform>().position.z);
    }

}
