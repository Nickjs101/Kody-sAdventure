using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List3 : MonoBehaviour
{
    [SerializeField] private GameObject[] Items;
    [SerializeField] private float delay;
    [SerializeField] private float ScaleSize;

    private void OnEnable() {
        for(int i = 0; i < Items.Length; i++) {
        LeanTween.scale(Items[i], new Vector3(ScaleSize, ScaleSize, ScaleSize), 1f).setDelay(delay + i);
        LeanTween.scale(Items[i], Vector3.one, 1f).setDelay(delay + 1 + i);
        }
    }

    private void OnDisable() {
        
    }
}
