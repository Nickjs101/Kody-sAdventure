using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SlideScript : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Button Backbtn;
    [SerializeField] private Button Nextbtn;
    [SerializeField] private Button Exit;

    int current_pos = 0; 

    float[] pos; 
 
    // Update is called once per frame
    void Awake()
    {
        Exit.interactable = false;

        pos = new float[transform.childCount];

        float distance = 1f / (pos.Length - 1f);

        for(int i = 0; i < pos.Length; i++) {
            pos[i] = distance * i;
        }
        if(scrollbar.value != current_pos)
            scrollbar.value = pos[current_pos];
    }

    private void Update() {
        if(scrollbar.value == 0){
            Backbtn.interactable = false;
        }else{
            Backbtn.interactable = true;
        }

        if(current_pos == transform.childCount - 1){
            Nextbtn.interactable = false;
            Exit.interactable = true;
        }else{
            Nextbtn.interactable = true;
        }
    }

    public void Back() {
        //scrollbar.value = pos[current_pos - 1];
        scrollbar.value = Mathf.Lerp(scrollbar.value, pos[current_pos - 1], 1f);
        current_pos -= 1;
    }

    public void Next() {
        //scrollbar.value = pos[current_pos + 1];
        scrollbar.value = Mathf.Lerp(scrollbar.value, pos[current_pos + 1], 1f);
        current_pos += 1;
    }
}
