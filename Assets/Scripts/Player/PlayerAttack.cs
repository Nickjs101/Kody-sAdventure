using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{   
    
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private GameObject[] Balls;
    [SerializeField] private AudioClip fireballSound;

    [Header ("Score points")] 
    [SerializeField] private int addEnemypoints;

    [SerializeField] private Button AttackBTN;
    

    private Animator anim;
    private float cooldownTimer;

    private int SelectedBullet;

    private Rigidbody2D body;

    private CapsuleCollider2D boxCollider;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        int SelectedBullet = 1; //PlayerPrefs.GetInt("SelectedBullet", 0);
    }

    private void Update() {
        //disable collide with player to attack
        Physics2D.IgnoreLayerCollision(12,8, true);
        Physics2D.IgnoreLayerCollision(12,9, true);
        Physics2D.IgnoreLayerCollision(12,13, true);


        cooldownTimer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.F) && AttackBTN.interactable)
        {
            Attack();
        }
    }


    public void Attack(){
        if(cooldownTimer > attackCooldown){
            anim.ResetTrigger("jump");
            anim.SetBool("grounded", false);
            anim.SetTrigger("attack");
            cooldownTimer = 0;
        }
        
    }

    private void Shoot() {
       ShootBall();
    }

    private void ShootFire() {
        SoundManager.instance.PlaySound(fireballSound);

        fireBalls[FindFireball()].transform.position = firePoint.position;
        fireBalls[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private void ShootBall() {
        SoundManager.instance.PlaySound(fireballSound);
        Balls[Findball()].transform.position = firePoint.position;
        Balls[Findball()].GetComponent<BallBounce>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball(){
        for(int i = 0; i < fireBalls.Length; i++) {
            if(!fireBalls[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private int Findball(){
        for(int i = 0; i < Balls.Length; i++) {
            if(!Balls[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // WalkingBug bug = collision.collider.GetComponent<WalkingBug>();

        if(other.gameObject.tag == "Enemy"){
            foreach(ContactPoint2D point in other.contacts) {
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                if(point.normal.y >= 0.9f){
                    GetComponent<PlayerMovement>().Jump();
                    other.gameObject.GetComponent<WalkingBug>().KillEnemy();
                    GetComponent<Scorer>().addScore(addEnemypoints);
                }else{
                    GetComponent<PlatformEffector2D>().useColliderMask = false;
                    GetComponent<Health>().TakeDamage(other.gameObject.GetComponent<WalkingBug>().damage);
                }
            }
        }
    }
    

        


    
}

