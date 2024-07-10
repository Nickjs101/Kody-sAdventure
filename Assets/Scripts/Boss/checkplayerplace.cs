using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkplayerplace : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            GetComponentInParent<BossBehaviour>().inPosition = true;
           
        }
    }
}
