using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    private int currentPoint;

    [SerializeField] private bool oneTimeMovement;
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform platform;

    private Transform playerParent;

    private void Update() {
        if(oneTimeMovement){
            platform.position = Vector3.MoveTowards(platform.position, points[1].position, moveSpeed * Time.deltaTime);
        }else{
            MoveToNextPoint();
        }
        
    }

    private void MoveToNextPoint() {
        // Move towards the goal point
        platform.position = Vector3.MoveTowards(platform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        // Check if we are reaching the next point if so Move to the next one
        if(Vector3.Distance(platform.position, points[currentPoint].position) < .05f) {
            currentPoint++;

            if(currentPoint >= points.Length) {
                currentPoint = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            playerParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.transform.SetParent(playerParent);
        }
    }
}
