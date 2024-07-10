using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Collide Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;

    [Header ("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Health playerHealth;

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
            if(cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0){
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
            
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

    private void DamagePlayer() {
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }

    // object deactivation method
    public void Deactivate(){
        gameObject.SetActive(false);
    }
}
