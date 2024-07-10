using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrao : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    //array of arrow object
    [SerializeField] private GameObject fireball;
    

    private void Awake() {
        Attack();
    }
    private void Attack(){
        
        fireball.transform.position = firePoint.position;
        
        fireball.SetActive(true);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Attack"){
            fireball.SetActive(false);
            Attack();
        }
    }
}
