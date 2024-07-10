using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTweens : MonoBehaviour
{
    [SerializeField] private GameObject programmer;
    [SerializeField] private GameObject Salary;
    [SerializeField] private Sprite img1;
    [SerializeField] private Sprite img2;

    private Image progImg;

    private bool animate;

    private void Awake() {
        programmer.GetComponent<RectTransform>().localScale = Vector3.zero;
        Salary.GetComponent<RectTransform>().localScale = Vector3.zero;
        progImg = programmer.GetComponent<Image>();
    }

    private void OnEnable() {
        LeanTween.scale(programmer, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(Salary, Vector3.one, 1f).setDelay(1.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.rotateAround(Salary, Vector3.forward, 360, 1.5f).setDelay(2.25f);

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
