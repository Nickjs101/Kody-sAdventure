using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FollowPath : MonoBehaviour
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

    private int targetPoint;

    private Animator anim;

    private void Start() {

        anim = GetComponent<Animator>();
        
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


        if(transform.position == pointInPath.Current.position){
            if(Convert.ToInt32(pointInPath.Current.name) != targetPoint){

                pointInPath.MoveNext();

                if(pointInPath.Current.position.x > transform.position.x){
                    SetDirection(1);
                }
                else if(pointInPath.Current.position.x < transform.position.x){
                    SetDirection(2);
                }
                else if(pointInPath.Current.position.y > transform.position.y){
                    SetDirection(3);
                }
                else if(pointInPath.Current.position.y < transform.position.y){
                    SetDirection(4);
                }
            }
            else{
                ActivateScene(targetPoint);
            }
        }
        /*
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal){
            pointInPath.MoveNext();
        }*/
    }

    public void MovePlayer(int point) {
        targetPoint = point;
    }

    private void SetDirection(int direction) {

       switch (direction) {
        case 1:
            anim.SetBool("right", true);
            anim.SetBool("left", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            break;
        case 2:
            anim.SetBool("right", false);
            anim.SetBool("left", true);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            break;
        case 3:
            anim.SetBool("right", false);
            anim.SetBool("left", false);
            anim.SetBool("up", true);
            anim.SetBool("down", false);
            break;
        case 4:
            anim.SetBool("right", false);
            anim.SetBool("left", false);
            anim.SetBool("up", false);
            anim.SetBool("down", true);
            break;
        default:
            anim.SetBool("right", false);
            anim.SetBool("left", false);
            anim.SetBool("up", false);
            anim.SetBool("down", false);
            break;
       }

        
    }

    public void ActivateScene(int level) {
       switch (level) {
        case 2:
            SceneManager.LoadScene("1");
            break;
        default :
            
            break;
       }
    }
}
