using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List2 : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject[] Lines;
    [SerializeField] private float delay;

    private void Awake() {
        box.GetComponent<RectTransform>().localScale = Vector3.one;

        for(int i = 0; i < Lines.Length; i++) {
            Lines[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    private void OnEnable() {
        LeanTween.scale(box, new Vector3(3f, 1f, 0f), 1f).setDelay(delay);

        LeanTween.scale(Lines[0], Vector3.one, 1f).setDelay(delay + 1f);
        LeanTween.scale(Lines[1], Vector3.one, 1f).setDelay(delay + 2f);
    }

    private void OnDisable() {
        LeanTween.reset();

        box.GetComponent<RectTransform>().localScale = Vector3.one;

        for(int i = 0; i < Lines.Length; i++) {
            Lines[i].GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }
}
