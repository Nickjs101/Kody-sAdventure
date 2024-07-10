using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPotion : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private GameObject ControlsUI;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            SoundManager.instance.PlaySound(collectSound);
            ControlsUI.GetComponent<Controls>().EnableAttack();
            Destroy(this.gameObject);
        }

        Debug.Log(collision.tag);
    }
}
