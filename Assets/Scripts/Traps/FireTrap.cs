using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    [SerializeField] private bool alwaysOn;
    //set damge of the fire trap
    [SerializeField] private float damage;
    //header
    [Header ("Firetrap Timers")]
    //how many secs before the trap being active
    [SerializeField] private float activationDelay;
    //how many secs the trap active
    [SerializeField] private float activeTime;
    //animator component initialization
    private Animator anim;
    //sprite component initialization
    private SpriteRenderer spriteRend;

    [Header ("Activation Sound")]
    [SerializeField] private AudioClip activateSound;

    //for checking when trap is triggered
    private bool triggered;//when trap is triggered
    //for checking if trap is active
    private bool active;

    private Health playerHealth;

    void Awake() {
        //get Animator component of firetrap when the screen open
        anim = GetComponent<Animator>();
        //get Animator component of sprite when the screen open
        spriteRend = GetComponent<SpriteRenderer>();

        if(alwaysOn){
           TrapON();
        }
    }

    private void TrapON(){
        anim.SetBool("activated", true);
        triggered = true;
        active = true;
    }

    public void TrapOFF(){
        anim.SetBool("activated", false);
        triggered = false;
        active = false;
    }

    void Update() {
        if(playerHealth != null && active){
            playerHealth.TakeDamage(damage);
        }

        
    }

    //collide checker
    void OnTriggerEnter2D(Collider2D collision) {
        
        //check if object is collided with player
        if(collision.tag == "Player"){

            playerHealth = collision.GetComponent<Health>();
            //check if the trap is already triggered or not
            if(!triggered){
                //if not triggered, then be triggered and do the active trap method
                StartCoroutine(ActivateFiretrap());
            }
            //check if the trap is active when collided with player
            if(active){
                //if it is active, then take damage to the player
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Player"){
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        //set triggered to true so that it will not be able to triggered again while active
        triggered = true;
        // turn red when triggered
        spriteRend.color = Color.red;// turn red when triggered

        //delay before it is being activated
        yield return new WaitForSeconds(activationDelay);
        
        SoundManager.instance.PlaySound(activateSound);
        //return white before it is being activated
        spriteRend.color = Color.white;
        //set active to true
        active = true;
        //activate the animation
        anim.SetBool("activated", true);

        //delay before it is being deactivated
        yield return new WaitForSeconds(activeTime);
        //set active to false
        active = false;
        //set triggered false so that it can be triggered again
        triggered = false;
        //deactivate the animation
        anim.SetBool("activated", false);

    }
}
