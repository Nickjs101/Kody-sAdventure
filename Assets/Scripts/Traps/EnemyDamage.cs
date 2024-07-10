using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;
    
    //collide checker
    protected void OnTriggerEnter2D(Collider2D collision) {
        //check if object is collided with player
        if(collision.tag == "Player"){
            //if collided then player take damage
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
