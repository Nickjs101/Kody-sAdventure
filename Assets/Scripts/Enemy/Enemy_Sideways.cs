using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool movingLeft;
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private float leftPoint;
    private float rightPoint;

    private Rigidbody2D body;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        leftPoint = leftEdge.position.x;
        rightPoint = rightEdge.position.x;
    }
    // Update is called once per frame
    void Update()
    {  
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        //check if moving left or right
        if(movingLeft){
            // move the object to left if left edge is greater than current position
            if(transform.position.x > leftPoint){
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            //if left edge is reach then direction is not left
            else{
                movingLeft = false;
            }
        }
        else{
            // move the object to right if right edge is less than current position
            if(transform.position.x < rightPoint){
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            //if left edge is reach then direction is not left
            else{
                movingLeft = true;
            }
        }

        if(GetComponent<Rigidbody2D>() != null){
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    
}
