using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBox : MonoBehaviour
{
    [SerializeField] private bool useKey;
    [SerializeField] private GameObject Reward;
    [SerializeField] private Sprite chestOpen;
    [SerializeField] private GameObject message;

    private bool isOpen;
    private SpriteRenderer renderer;

    private void Start() {
        isOpen = false;
        renderer = GetComponent<SpriteRenderer>();
        Reward.transform.position = new Vector3(transform.position.x, transform.position.y + .912f, transform.position.z);
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(useKey) return;
    //     if(other.tag == "Player"){
    //         if(!isOpen){
    //             OpenChest();
    //         }
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other) {
        LeanTween.scale(message, Vector3.zero, .12f);
    }

    public void OpenChest()
    {
        if(useKey){
            if(PlayerPrefs.GetInt("Keys") >= 1){
                PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") - 1);
                isOpen = true;
                renderer.sprite = chestOpen;
                Reward.SetActive(true);;
            }else{
                LeanTween.scale(message, new Vector3(.2f, .2f, .2f), .12f);
            }
        }else{
            isOpen = true;
            renderer.sprite = chestOpen;
            Reward.SetActive(true);  
        }
        
    }
}
