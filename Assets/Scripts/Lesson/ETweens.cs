using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ETweens : MonoBehaviour
{
    [SerializeField] private GameObject programmer;
    [SerializeField] private Sprite img1;
    [SerializeField] private Sprite img2;

    private Image progImg;

    private bool animate;

    private void Awake() {
        programmer.GetComponent<RectTransform>().localScale = Vector3.zero;
        progImg = programmer.GetComponent<Image>();
    }

    private void OnEnable() {
        LeanTween.scale(programmer, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutBack);
        StartCoroutine(AnimationProg());
    }

    private void OnDisable() {
        animate = false;
        LeanTween.reset();
        programmer.GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    IEnumerator AnimationProg()
    {   
        yield return new WaitForSeconds(1.5f);
        animate = true;
        while(animate) {
            if(progImg.sprite == img1){
                yield return new WaitForSeconds(.5f);
                progImg.sprite = img2;
            }else{
                yield return new WaitForSeconds(.5f);
                progImg.sprite = img1;
            }
        }
        
    }
}
