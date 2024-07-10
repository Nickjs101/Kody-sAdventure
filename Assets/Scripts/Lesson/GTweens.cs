using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTweens : MonoBehaviour
{
    [SerializeField] private GameObject DialogueText;
    [SerializeField] private GameObject[] objects;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float delay;

    private float originaldelay;

    private Vector3 originalPos;

    private void Awake() {
        originaldelay = delay;
        if(DialogueText != null){
            originalPos = DialogueText.GetComponent<RectTransform>().position;
        }
        
        foreach (GameObject job in objects)
        {
            job.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }
    private void OnEnable() {
        if(newPosition != Vector3.zero && DialogueText != null){
            DialogueText.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }

        foreach (GameObject job in objects)
        {
            LeanTween.scale(job, Vector3.one, 1.5f).setDelay(delay).setEase(LeanTweenType.easeOutBack);
            delay += 1.5f;
        }
    }

    private void OnDisable()
    {
        LeanTween.reset();
        delay = originaldelay;
        
        if(DialogueText != null){
            DialogueText.GetComponent<RectTransform>().position = originalPos;
        }
        
        foreach (GameObject job in objects)
        {
            job.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }
}
