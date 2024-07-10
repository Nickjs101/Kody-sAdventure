using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private enum BossType{
        Meelee,
        Range
    }
    [SerializeField] private BossType bossType;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float speed;
    

    [Header ("Attack Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask playerLayer;

    
    private Animator anim;

    private Vector3 OriginalPosition;

    private bool moveToPlayer = false;
    private bool moveToPlace = false;

    
    private Health playerHealth;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        OriginalPosition = transform.position;
    }

    void Update()
    {
        if(moveToPlayer){
            transform.position = Vector3.MoveTowards(transform.position, attackPosition.position, Time.deltaTime * speed);
            if(transform.position == attackPosition.position){
                moveToPlayer = false;
            }
            
        }

        if(moveToPlace){
            transform.position = Vector3.MoveTowards(transform.position, OriginalPosition, Time.deltaTime * speed);
            if(transform.position == OriginalPosition){
                moveToPlace = false;
            }
        }
    }

    public void Attack() {
        if(bossType == BossType.Meelee){
            anim.SetTrigger("attack");
            moveToPlayer = true;
        }
        else if(bossType == BossType.Range){
            anim.SetTrigger("attack");
        }
        
    }

    public void MoveBack() {
        moveToPlayer = false;
        moveToPlace = true;
    }



    public void DamagePlayer() {
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }


    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
                            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
