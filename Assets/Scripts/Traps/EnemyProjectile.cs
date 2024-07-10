using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    //speed of arrow
    [SerializeField] private float speed;
    //seconds before the next arrow
    [SerializeField] private float resetTime;
    //how many seconds the arrow active
    private float lifetime;

    //this will activate the Arrow trap
    public void ActivateProjectile() {
        //reset the lifetime of arrows
        lifetime = 0;
        //set the arrows active firing
        gameObject.SetActive(true);
    }

    private void Update(){
        //calculate the arrow speed by multiply it to timeframe
        float movementSpeed = speed * Time.deltaTime;
        //move the arrow horizontal depending speed
        transform.Translate(movementSpeed, 0, 0);

        //increment lifetime when it is active
        lifetime += Time.deltaTime;
        //if the arrow lifetime reach the resettime it will be deactivated
        if(lifetime > resetTime){
            gameObject.SetActive(false);
        }
    }

    //do the code when the arrow collided with other object
    private void OnTriggerEnter2D(Collider2D collision) {
        //this will call the ontrigger method of EnemyDamage class which will check if the arrow is collided with player
        base.OnTriggerEnter2D(collision);
        //deactivated arrow when collided 
        gameObject.SetActive(false);
    }
}
