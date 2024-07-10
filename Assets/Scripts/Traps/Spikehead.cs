using UnityEngine;

public class Spikehead : EnemyDamage
{   
    //movement speed of the spike head
    [SerializeField] private float speed;
    //range that can detect player
    [SerializeField] private float range;
    //delay before spikehead attack
    [SerializeField] private float checkDelay;
    //layer of the player
    [SerializeField] private LayerMask playerLayer;
    //timer
    private float checkTimer;
    //this will be the player position and target destination of spikehead
    private Vector3 destination;
    //set spikehead attack mode
    private bool attacking; 

    [Header ("hit Sound")]
    [SerializeField] private AudioClip hitSound;

    //array of 4 directions
    private Vector3[] directions = new Vector3[4];

    //onEnable is called when the object is activated
    private void OnEnable() {
        Stop();
    }

    private void Update() {
        //move the spikehead on the destination if the spikehead is attacking
        if(attacking){
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else{
            //increase the checktimer time
            checkTimer += Time.deltaTime;
            //check if the timer is greater than delay, check for player
            if(checkTimer > checkDelay){
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer(){
        CalculateDirections();
        //check player for all direction
        for(int i = 0; i < directions.Length; i++) {
            //drawing ray
            Debug.DrawRay(transform.position, directions[i], Color.red);
            //check if ray hit the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            //check if raycast hit the player and spikehead is not attacking
            if(hit.collider != null && hit.transform.tag == "Player" && !attacking){
                //if true
                //set attack mode to true
                attacking = true;
                //set the destination of the attack to the direction detected
                destination = directions[i];
                //reset timer to zero
                checkTimer = 0;
            }
        }

    }
    private void CalculateDirections(){
        directions[0] = transform.right * range;//right dir
        directions[1] = -transform.right * range;//left dir
        directions[2] = transform.up * range;//top dir
        directions[3] = -transform.up * range;//down dir
    }
    
    private void Stop(){
        //set the destination to current position so that spikehead will not move anywhere
        destination = transform.position;
        //set attack mode to false
        attacking = false;
    }

    //do the code when the arrow collided with other object
    private void OnTriggerEnter2D(Collider2D collision) {
        //this will call the ontrigger method of EnemyDamage class which will check if the arrow is collided with player
        base.OnTriggerEnter2D(collision);
        //stop movement if hit 
        Stop();
    }
}
