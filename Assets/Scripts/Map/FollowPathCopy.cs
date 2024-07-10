using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FollowPathCopy : MonoBehaviour
{
    private enum MovementType{
        MovingTowards,
        LerpTowards
    }

    [SerializeField] private MovementType Type = MovementType.MovingTowards;

    [SerializeField] private MovementPathCopy MyPath;

    [SerializeField] private float speed = 1;

    [SerializeField] private LoadingManager loading;

    private Transform currentPoint;

    private int targetPoint;

    private Animator anim;

    bool ActivateLevel = false;

    private void Start() {
        Time.timeScale = 1; // error accur everytime i go to the map that stop the time

        anim = GetComponent<Animator>();

        currentPoint = MyPath.GetCurrentPoint();

        targetPoint = Convert.ToInt32(currentPoint.name);

        transform.position = currentPoint.position;
    } 

    private void Update() {
        
        
        if(MyPath == null || currentPoint == null) return;

        if(Type == MovementType.MovingTowards){
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, Time.deltaTime * speed);
        }

        // if(Type == MovementType.LerpTowards){
        //     transform.position = Vector3.Lerp(transform.position, currentPoint.position, Time.deltaTime * speed);
        // }


        if(transform.position == currentPoint.position){
            if(Convert.ToInt32(currentPoint.name) != targetPoint){ //continuosly move next point until target point is reach

                if(Convert.ToInt32(currentPoint.name) > targetPoint){
                    currentPoint = MyPath.GetNextPathPoint(-1);
                }else{
                    currentPoint = MyPath.GetNextPathPoint();
                }
                    

                // Debug.Log(currentPoint.name);

                if(currentPoint.position.x > transform.position.x){
                    SetDirection(1);
                }
                else if(currentPoint.position.x < transform.position.x){
                    SetDirection(2);
                }
                else if(currentPoint.position.y > transform.position.y){
                    SetDirection(3);
                }
                else if(currentPoint.position.y < transform.position.y){
                    SetDirection(4);
                }
            }
            else{
                SetDirection(0);
                ActivateScene(targetPoint);
            }
        }
    }

    public void MovePlayer(int point) {

        
        
        if(Convert.ToInt32(currentPoint.name) > point){
            targetPoint = point;
            ActivateLevel = true; // only activate level when this button is click

        }
        else if(MyPath.pointToLevel(point) == 1){
            targetPoint = point;
            ActivateLevel = true; // only activate level when this button is click

        }else if(Convert.ToInt32(currentPoint.name) == point){
            targetPoint = Convert.ToInt32(currentPoint.name);
            ActivateLevel = true; // only activate level when this button is click
        }
        else{
            if(MyPath.checkPoint(point) == true){ // Move player if level can be unlock
                targetPoint = point;
                ActivateLevel = true; // only activate level when this button is click
            }else{
                ActivateLevel = false;
            }
            //dont move if cant be activated
        }

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

    public void ActivateScene(int point) {

        if(ActivateLevel){
            ActivateLevel = false;

            if(MyPath.UnlockAndGetLevel(point).ToString().Equals("1") && PlayerPrefs.GetInt("FirstOpen", 0) == 0){
                PlayerPrefs.SetInt("FirstOpen", 1);
                loading.LoadScene("Intro");
            }else{
                loading.LoadScene(MyPath.UnlockAndGetLevel(point).ToString()); //level
                Debug.Log("Level "+MyPath.UnlockAndGetLevel(point).ToString()+" is Loaded");
            }
            
        }
        
    }
}
