using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Range Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    [Header ("Collide Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header ("Fire Ball Sound")]
    [SerializeField] private AudioClip fireballSound;

    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private EnemyPatrol enemyPatrol;

    void Awake() {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown){
                cooldownTimer = 0;
                anim.SetTrigger("rangeAttack");
            }
        }

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
            
    }

    private void RangeAttack() {
        SoundManager.instance.PlaySound(fireballSound);
        cooldownTimer = 0;
        fireBalls[FindFireBall()].transform.position = firePoint.position;
        fireBalls[FindFireBall()].GetComponent<RangeProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall() {
        for(int i = 0; i < fireBalls.Length; i++) {
            if(!fireBalls[i].activeInHierarchy)
                return i;
        }

        return 0;
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
                            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // object deactivation method
    public void Deactivate(){
        gameObject.SetActive(false);
    }
}
