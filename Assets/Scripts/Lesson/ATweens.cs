using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATweens : MonoBehaviour
{
    [SerializeField] private GameObject Logo;
    [SerializeField] private GameObject Web;
    [SerializeField] private GameObject Mobile;
    [SerializeField] private GameObject Ai;

    private void Awake() {
        Logo.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web.GetComponent<RectTransform>().localScale = Vector3.zero;
        Mobile.GetComponent<RectTransform>().localScale = Vector3.zero;
        Ai.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
    private void OnEnable() {
        LeanTween.scale(Logo, Vector3.one, 1.5f).setDelay(.25f).setEase(LeanTweenType.easeOutBack);
        LeanTween.rotateAround(Logo, Vector3.forward, 360, 1.5f).setDelay(.25f);

        LeanTween.scale(Web, Vector3.one, 1.5f).setDelay(1.75f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(Mobile, Vector3.one, 1.5f).setDelay(3.25f).setEase(LeanTweenType.easeOutBack);

        LeanTween.scale(Ai, Vector3.one, 1.5f).setDelay(4.75f).setEase(LeanTweenType.easeOutBack);
    }
    void OnDisable()
    {
        LeanTween.reset();
        
        Logo.GetComponent<RectTransform>().localScale = Vector3.zero;
        Web.GetComponent<RectTransform>().localScale = Vector3.zero;
        Mobile.GetComponent<RectTransform>().localScale = Vector3.zero;
        Ai.GetComponent<RectTransform>().localScale = Vector3.zero;
    }
}
