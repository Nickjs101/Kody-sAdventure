using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes{
        Linear,
        Loop
    }
    [SerializeField] private PathTypes PathType;
    [SerializeField] private int movementDirection = 1;
    [SerializeField] private int movingTo = 0;
    [SerializeField] private Transform[] PathSequence;

    private void OnDrawGizmos(){
        if(PathSequence == null || PathSequence.Length < 2) return;

        for(int i = 1; i < PathSequence.Length; i++) {
            //drawline from last point to current point
            Gizmos.DrawLine(PathSequence[i-1].position, PathSequence[i].position);
        }

        // if(PathType == PathTypes.Loop){
        //     Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
        // }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if(PathSequence == null || PathSequence.Length < 1) yield break;

        while(true){
            yield return PathSequence[movingTo];

            if(PathSequence.Length == 1) continue;

            if(PathType == PathTypes.Linear){
                //back and forth movement
                if(movingTo <= 0){
                    movementDirection = 1;
                }else if(movingTo >= PathSequence.Length - 1){
                    movementDirection = -1;
                }
            }

            movingTo = movingTo + movementDirection;

            // if(PathType == PathTypes.Loop){
            //     if(movingTo >= PathSequence.Length){
            //         movingTo = 0;
            //     }else if(movingTo < 0){
            //         movingTo = PathSequence.Length - 1;
            //     }
            // }
        }
    }
}
