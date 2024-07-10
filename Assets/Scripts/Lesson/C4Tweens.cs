using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Tweens : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private GameObject pythonBox;
    [SerializeField] private GameObject pythonifCover;
    [SerializeField] private GameObject otherBox;
    [SerializeField] private GameObject otherifCover;

    private void Awake() {
        LeanTween.scale(pythonBox, Vector3.zero, 0f);
        LeanTween.scale(otherBox, Vector3.zero, 0f);
    }

    private void OnEnable() {
        LeanTween.scale(pythonBox, Vector3.one, 1f).setDelay(delay);
        LeanTween.scale(otherBox, Vector3.one, 1f).setDelay(delay + 1f);

        LeanTween.scale(pythonifCover, new Vector3(1.5f, 1.5f, 1.5f), .5f).setDelay(delay + 2f);
        LeanTween.scale(pythonifCover, Vector3.one, .5f).setDelay(delay + 2.5f);

        LeanTween.scale(otherifCover, new Vector3(1.5f, 1.5f, 1.5f), .5f).setDelay(delay + 4f);
        LeanTween.scale(otherifCover, Vector3.one, .5f).setDelay(delay + 4.5f);

    }

    private void OnDisable() {
        LeanTween.reset();
        LeanTween.scale(pythonBox, Vector3.zero, 0f);
        LeanTween.scale(otherBox, Vector3.zero, 0f);
    }
}
