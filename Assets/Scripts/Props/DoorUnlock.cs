using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField] private Sprite doorOpen;

    [SerializeField] private GameObject coliderlock;

    [SerializeField] private GameObject message;

    [SerializeField] private AudioClip openSound;

    
    

    
    private SpriteRenderer renderer;
    private BoxCollider2D boxCollider;

    private int keys;
     private bool isOpen = false;


    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    IEnumerator Unlock()
    {   
        yield return new WaitForSeconds(.2f);
        SoundManager.instance.PlaySound(openSound);
        renderer.sprite = doorOpen;
        isOpen = true;
        coliderlock.SetActive(false);
        //playsound
    }

    public void Open() {
        
        if(isOpen == false){
            keys = PlayerPrefs.GetInt("Keys");
            if(keys >= 1){
                PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") - 1);
                StartCoroutine(Unlock());
            }
            else{
                LeanTween.scale(message, new Vector3(.2f, .2f, .2f), .12f);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag =="Player"){
            LeanTween.scale(message, Vector3.zero, .12f);
        }
    }

}
