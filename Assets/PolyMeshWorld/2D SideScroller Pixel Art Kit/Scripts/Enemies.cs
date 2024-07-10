using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
 
    private int nextID = 0; //The int value for next point index
    int idChangeValue = 1; //the value of that applies to ID for changing

    [Header("MovingSystem")]
    [SerializeField] private List<Transform> points; //Reference to waypoints
    [SerializeField] private float speed = 2; //Speed of movement of flying
    
    private void Reset() {
        Init();
    }

    private void Init() {
        GetComponent<BoxCollider2D>().isTrigger = true; //Make Box collider trigger

        GameObject root = new GameObject(name + "_Root"); //Create Root object
        root.transform.position = transform.position; //Reset Position of Root to enemy object;
        transform.SetParent(root.transform); //Set enemy as child of root
        GameObject waypoints = new GameObject("Waypoints"); //Create Waypoint object
        waypoints.transform.SetParent(root.transform); //Reset waypoints position to root, Make waypoints object child of root
        waypoints.transform.position = root.transform.position;
        //Create two points (gameobject) and reset their position to waypoints objects
        //Make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform); 
        points.Add(p2.transform); 
    }

    private void Update() {
        MoveToNextPoint();
    }

    private void MoveToNextPoint() {
        Transform goalPoint = points[nextID]; //Get the next Point transform
        //Flip the enemy transform to look into the point's direction
        if(goalPoint.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (goalPoint.transform.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1, 1, 1);
        } if(goalPoint.transform.position.y < transform.position.y || goalPoint.transform.position.y > transform.position.y) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime); //Move the enemy towards the goal point
        //Check the distance between enemy and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position) < 0.2f) {
            //Check if we are at the end of the line (make the change -1)
            if(nextID == points.Count - 1) {
                idChangeValue = -1;
            }
            if(nextID == 0) {
                idChangeValue = 1;
            }
            nextID += idChangeValue; //Apply the change on the nextID          
        }
    }
}
