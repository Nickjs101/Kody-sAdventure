using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List1Tweens : MonoBehaviour
{
    [SerializeField] private GameObject[] tween1;
    [SerializeField] private GameObject[] tween2;

    [SerializeField] private float delayOnStart;
    [SerializeField] private float delayEachTween;

    private void Awake() {
        for(int i = 0; i < tween1.Length; i++) {
            LeanTween.scale(tween1[i], Vector3.zero, 0f);
        }     

        for(int i = 0; i < tween2.Length; i++) {
            LeanTween.scale(tween2[i], Vector3.zero, 0f);
        }
    }

    private void OnEnable() {
        StartCoroutine(tweening());
        StartCoroutine(tweening2());
    }

    private void OnDisable() { 
        LeanTween.reset();

        for(int i = 0; i < tween1.Length; i++) {
            LeanTween.scale(tween1[i], Vector3.zero, 0f);
        }     

        for(int i = 0; i < tween2.Length; i++) {
            LeanTween.scale(tween2[i], Vector3.zero, 0f);
        }
    }

    IEnumerator tweening(){
        yield return new WaitForSeconds(delayOnStart);

        for(int i = 0; i < tween1.Length; i++) {
            LeanTween.scale(tween1[i], Vector3.one, delayEachTween).setDelay(delayEachTween * i).setEase(LeanTweenType.easeOutBack);
        }

        
    }
    IEnumerator tweening2(){
        yield return new WaitForSeconds((delayEachTween * tween1.Length + 1f) + 2f);

        for(int i = 0; i < tween2.Length; i++) {
            LeanTween.scale(tween2[i], Vector3.one, delayEachTween).setDelay(delayEachTween * i).setEase(LeanTweenType.easeOutBack);
        }
    }
}
