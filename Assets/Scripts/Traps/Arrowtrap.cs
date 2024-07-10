using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtrap : MonoBehaviour
{   
    //set arrow trap attack cooldown
    [SerializeField] private float attackCooldown;
    //set the initialization of arrow when attacking
    [SerializeField] private Transform firePoint;
    //array of arrow object
    [SerializeField] private GameObject[] arrows;
    //set cooldown timer for arrows
    private float cooldownTimer;

    [Header ("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private void Attack(){
        SoundManager.instance.PlaySound(attackSound);
        //everytime it attack it reset cooldown to zero
        cooldownTimer = 0;

        //set firepoint as initial position of arrows
        arrows[FindArrow()].transform.position = firePoint.position;
        //each arrow will be activated depending on arrow projectile class
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();

    }
    //this will find the non active arrow in the pool and return the number of it 
    private int FindArrow(){
        for(int i = 0; i < arrows.Length; i++) {
            if(!arrows[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private void Update(){
        //increment cooldown timer when active
        cooldownTimer += Time.deltaTime;
        //as long as the cooldown timer greater or equal to setted attack cooldown keep  attacking
        if(cooldownTimer >= attackCooldown){
            Attack();
        }
    }
     
}
