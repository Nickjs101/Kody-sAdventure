using UnityEngine;

public class BouncePad : MonoBehaviour {
    
    private bool bouncing = false;

    [Header("BouncePad")]
    [SerializeField] private float bounce = 20f;
    [SerializeField] private Animator anim;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Player")) {
            if (!bouncing) {
                anim.SetBool("SetAnimation", false);
                coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
                bouncing = true;
                //AudioManager.instance.PlaySFX(0);
            }
        }
    }

    void FixedUpdate () {
        if (bouncing) {
            anim.SetBool("SetAnimation", true); 
            bouncing = false;
        } 
    }
}


