using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{   
    private enum ControlType{
        Keyboard,
        Joystick
    }


    [Header ("Player Control")] 
    [SerializeField] private ControlType playerControl;
    

    [Header ("JoyStick")] 
    [SerializeField] private Joystick joystick;

    [Header ("Movement Parameters")]  
    //Settings for running speed and jump power
    [SerializeField] private float speed;
    [SerializeField] public float jumpPower;

    [Header ("Cayote Time")]
    //settings: can jump while in the air as long as time does not run out
    [SerializeField] private float cayoteTime;
    private float cayoteCounter;


    [Header ("WallJump Parameters")]
    //setings for how high and distance when wall jumping
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;


    [Header ("Layers")]
    
    //layer setting
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask Ladderlayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask enemyLayer;
    //body of the character
    private Rigidbody2D body;
    //character animations
    private Animator anim;
    
    [SerializeField] private CapsuleCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;
    private float verticalInput;

    private bool wasOnGround;


    [SerializeField] private float groundCheckerAdjustmentHorizontal;
    [SerializeField] private float groundCheckerAdjustmentVertical;
    
    private UIManager uiManager;


    [Header ("Sound FX")]
    //sound setting
    [SerializeField] private AudioClip jumpSound;


    [Header("LadderClimb")]
    [SerializeField] private float ladderspeed = 8f; //Speed that the player climbs the ladder
    private Transform LadderTransform;

    public bool enableMovement = true;

    

    

    //do the code everytime the script reload
    private void Awake()
    {   
        //get character body object in unity
        body = GetComponent<Rigidbody2D>();
        //get character animation in unity
        anim = GetComponent<Animator>();
        
        uiManager = GameObject.FindWithTag("LevelUI").GetComponent<UIManager>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        //disable collide with player to attack
        Physics2D.IgnoreLayerCollision(10,8, true);
        //get horizontal inputs from user 1 for right and -1 for left
        if(enableMovement){
            if(playerControl == ControlType.Joystick){
                horizontalInput = joystick.Horizontal;
                verticalInput = joystick.Vertical;
            }else{
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
            }
        }else{
            horizontalInput = 0;
            verticalInput = 0;
        }
        
        
        if(horizontalInput < 0.25f && horizontalInput > -0.25f){
            horizontalInput = 0; 
        }
        
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //if horizontal input is right -> change char direction to right
        if(horizontalInput > 0.01f){
            transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        //if horizontal input is left -> change char direction to left
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        //animate running to true if user click left or right
        anim.SetBool("run", horizontalInput > 0.25f || horizontalInput < -0.25f && isGrounded());
        //animate idle if ground is true and jump if false
        anim.SetBool("grounded", isGrounded());
        

        //check if user click Space bar then do the jump
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        //after clicking space or space key is up, the gravity will increase
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0){
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        // character touch the ground reset the extra jump timer and counter
        if(isGrounded()){
            cayoteCounter = cayoteTime;
        }
        //if character is not on the ground then run the can jump timer
        else{
            cayoteCounter -= Time.deltaTime;
        }
        

        if(inLadder()){
            body.gravityScale = 0;
            body.velocity = new Vector2(body.velocity.x, verticalInput * ladderspeed);
        }else{
            body.gravityScale = 2.5f;
        }


        if(isHeadGrounded()) {
            
        }

    }

    // void FixedUpdate()
    // {
        

    // }

    public void Jump() {
        if(!enableMovement) return;
        
        anim.SetTrigger("jump");

        //if can still jump reach zero
        // and no extra jump available
        // then dont do jump
        if(cayoteCounter <= 0) return;

        //play sound
        SoundManager.instance.PlaySound(jumpSound);
        //if on the ground then jump the player normally
        if(isGrounded()) {
            //change move the char up if jump() is called
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        //else if not on ground but jump is called do the extra jump or can still jump
        else{
                //check if timer is greater than zero then player can still jump
            if(cayoteCounter > 0){
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
        }
        //if already jump then set the can still jump to zero,
        //so the player can only dp normal or extra jump
        cayoteCounter = 0;
        
        
    }

    private bool inLadder(){
        //check if the player if the foot of the character is collided with ground object
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.zero, 1f, Ladderlayer);
        if(raycastHit.collider != null){
            LadderTransform = raycastHit.transform;
        }
        return raycastHit.collider != null;
        
    }

    private bool isGrounded(){
        //check if the player if the foot of the character is collided with ground object
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector3(boxCollider.bounds.size.x * groundCheckerAdjustmentHorizontal, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.down, 1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool isHeadGrounded(){
        //check if the player if the foot of the character is collided with ground object
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector3(boxCollider.bounds.size.x * groundCheckerAdjustmentHorizontal, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.up, 1f, groundLayer);
        return raycastHit.collider != null;
        
    }

    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, new Vector3(boxCollider.bounds.size.x * groundCheckerAdjustmentHorizontal, boxCollider.bounds.size.y * groundCheckerAdjustmentVertical, boxCollider.bounds.size.z));
    }

    

    


    
    


    public void ShowGameOver() {
        uiManager.GameOver();
    }

    /*for wall jumping
    private bool onWall(){
        //check if the player if character is collided with wall object
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void WallJump() {
        //addForce will do the wall jump
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        //reset wall jump cooldown
        wallJumpCooldown = 0;
    }*/
}
