using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set;}
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;//how many seconds will flashes do
    [SerializeField] private int numberOfFlashes;//number of flashes when hurt
    private SpriteRenderer spriteRend;//sprite component holder

    [Header ("Components")]
    [SerializeField] private Behaviour[] components; 
    private bool isInvulnerable;

    [Header ("Components")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;


    //will do the code when the game starts
    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        
    }
    // private void Update() {
    //     Debug.Log(currentHealth);
    // }

    //will take damage when collide with enemy/traps
    public void TakeDamage(float _damage){

        if(isInvulnerable) return;

        //will do the health reduction
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        //will check if there is still life
        if (currentHealth > 0){
            SoundManager.instance.PlaySound(hurtSound);
            //animation hurt will trigger
            if(transform.gameObject.tag == "Player"){
                anim.SetTrigger("hurt");
            }
            
            //call the hurt function
            StartCoroutine(Invunerability());
        }
        else{
            if (!dead){
                
                
                foreach(Behaviour component in components) {
                    component.enabled = false;
                }
                if(GetComponent<PlayerMovement>() != null){
                    GetComponent<PlayerMovement>().enableMovement = false;
                }

                anim.SetBool("grounded", true);
                //trigger die animation
                anim.SetTrigger("die");

                //set dead to true so it cannot die again
                dead = true;

                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
    }

    //function that will add health when get heart
    public void addHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //will do the flashes when hurt
    private IEnumerator Invunerability(){
        isInvulnerable = true;

        //it ignore the damage when hurt for seconds
        
        Physics2D.IgnoreLayerCollision(10,8, true);

        //then do the flasher while ingoring damage
        for(int i = 0; i < numberOfFlashes; i++) {
            spriteRend.color = new Color(1,1,1,0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        //then remove ignore damage
        Physics2D.IgnoreLayerCollision(10,8, false);
        GetComponent<PlatformEffector2D>().useColliderMask = true;

        isInvulnerable = false;

    }

    

    /*
    public void Respawn() {

        dead = false;
        addHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach(Behaviour component in components) {
            component.enabled = true;
        }
    }*/


}
