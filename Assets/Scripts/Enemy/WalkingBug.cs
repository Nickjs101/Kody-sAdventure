using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkingBug : MonoBehaviour
{
    private Animator animator;
    [SerializeField] public int damage;
    [SerializeField] private Behaviour movementScript;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private AudioClip dieSound;

    private BoxCollider2D collider;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        //weakpoint = transform.GetChild(0).gameObject;
    }
    

    public void KillEnemy() {
        movementScript.enabled = false;
        animator.enabled = false;
        SoundManager.instance.PlaySound(dieSound);
        if(body != null){
            StartCoroutine(Fall());
        }else{
            StartCoroutine(Die());
        }
        
    }

    IEnumerator Die()
    {
        transform.localScale = new Vector3(transform.localScale.x, .2f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - .11f, transform.position.z);
        yield return new WaitForSeconds(.25f);
        gameObject.SetActive(false);
    }

    IEnumerator Fall()
    {
        collider.isTrigger = true;
        body.gravityScale = 5f;
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
}
