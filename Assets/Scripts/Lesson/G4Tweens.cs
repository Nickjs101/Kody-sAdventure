using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G4Tweens : MonoBehaviour
{
    [SerializeField] private GameObject[] tween;
    [SerializeField] private GameObject[] pop;

    [SerializeField] private float delayOnStart;
    [SerializeField] private float delayEachTween;

    private void Awake() {
        for(int i = 0; i < tween.Length; i++) {
            LeanTween.scale(tween[i], Vector3.zero, 0f);
        }     

        for(int i = 0; i < pop.Length; i++) {
            pop[i].SetActive(false);
        }
    }

    private void OnEnable() {
        StartCoroutine(tweening());
        StartCoroutine(poping());
    }

    private void OnDisable() { 
        LeanTween.reset();

        for(int i = 0; i < tween.Length; i++) {
            LeanTween.scale(tween[i], Vector3.zero, 0f);
        }     

        for(int i = 0; i < pop.Length; i++) {
            pop[i].SetActive(false);
        }
    }

    IEnumerator tweening(){
        yield return new WaitForSeconds(delayOnStart);

        for(int i = 0; i < tween.Length; i++) {
            LeanTween.scale(tween[i], Vector3.one, delayEachTween).setDelay(delayEachTween * i).setEase(LeanTweenType.easeOutBack);
        }

        
    }
    IEnumerator poping(){
        yield return new WaitForSeconds((delayEachTween * tween.Length + 1f) + delayOnStart);

        for(int i = 0; i < pop.Length; i++) {
            pop[i].SetActive(true);
            yield return new WaitForSeconds(delayEachTween);
        }
    }
}
