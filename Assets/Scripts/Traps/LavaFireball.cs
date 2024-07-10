using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFireball : MonoBehaviour
{
    [SerializeField] private float Power;
    
    private void OnEnable() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Power);
    }

    private void Update() {

        if(GetComponent<Rigidbody2D>().velocity.y < 0){
            GetComponent<SpriteRenderer>().flipX = true;
        }else{
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
