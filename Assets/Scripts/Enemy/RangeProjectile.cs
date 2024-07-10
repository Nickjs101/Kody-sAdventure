using UnityEngine;

public class RangeProjectile : MonoBehaviour
{   
    //set the speed of fireball
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    //boolean for checking if fireball hit something
    private bool hit;
    //direction of fireball (-) for left/ (+) for right
    private float direction;
    //number of sec the fireball is active
    private float lifetime;

    //box collider component initialization
    private BoxCollider2D boxCollider;
    //animator component initialization
    private Animator anim;

    private void Awake() {
        //get boxcollider component of player when the screen open
        boxCollider = GetComponent<BoxCollider2D>();
        //get Animator component of player when the screen open
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {   
        //if fireball already collided with other,dont let the fireball continue moving
        if(hit) return;
        //else do these
        //compute the speed of arrows direction depends on number sign
        float movementSpeed = speed * Time.deltaTime * direction;
        //move the fireball horizontally
        transform.Translate(movementSpeed, 0, 0);

        //increase lifetime everytime the fireball is active
        lifetime += Time.deltaTime;
        //lifetime of fireball is greater than 5secs, deavtivate fireball
        if(lifetime > resetTime) gameObject.SetActive(false);
    }
    //do the code when the fireball is collided
    private void OnTriggerEnter2D(Collider2D collision) {
        //set the collision boolean to true
        hit = true;
        //disable the boxcollider because it is already collided 
        boxCollider.enabled = false;
        //do the explode animation 
        anim.SetTrigger("explode");
        //check if attack hit enemy, then inflict damage to the enemy
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    //Attack method 
    public void SetDirection (float _direction) {
        //reset the life time of fireball 
        lifetime = 0;
        //direction is equal to parameter
        direction = _direction;
        //set fireball active
        gameObject.SetActive(true);
        //set collision to false because it is only starting
        hit = false;
        //set collider component active
        boxCollider.enabled = true;

        //set the direction face of object to object localscale
        float localScaleX = transform.localScale.x;
        //check if the localscale(face direction) of the object is not equal to movement direction 
        if(Mathf.Sign(localScaleX) != _direction){
            //change the face object direction of the object
            localScaleX = -localScaleX;
        } 
        //lasly change the actual movement face of the object
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // object deactivation method
    private void Deactivate(){
        gameObject.SetActive(false);
    }
}
