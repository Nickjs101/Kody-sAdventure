using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private enum MovementType{
        MovingTowards,
        LerpTowards
    }

    [SerializeField] private MovementType Type = MovementType.MovingTowards;
    [SerializeField] private MovementPath MyPath;
    [SerializeField] private float speed = 1;
    [SerializeField] private float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> pointInPath;
    private bool flip = true;

    private SpriteRenderer render;

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        
        if(MyPath == null){
            Debug.LogError("No Path to Follow", gameObject);
            return;
        } 
        
        pointInPath = MyPath.GetNextPathPoint();

        pointInPath.MoveNext();

        if(pointInPath.Current == null){
            Debug.LogError("no Path Points to follow", gameObject);
            return;
        }

        transform.position = pointInPath.Current.position;
    } 

    private void Update() {
        
        if(pointInPath == null || pointInPath.Current == null) return;

        if(Type == MovementType.MovingTowards){
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        if(Type == MovementType.LerpTowards){
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal){
            pointInPath.MoveNext();

            flip = !flip;
            render.flipX = flip;
            
        }
        
    }
    
}