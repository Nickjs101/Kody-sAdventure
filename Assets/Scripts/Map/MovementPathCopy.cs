using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementPathCopy : MonoBehaviour
{
    [SerializeField] MapPlayPrefs MapDM;

    [SerializeField] private int CurrentPoint;

    [SerializeField] private Transform[] PathSequence;
    [SerializeField] private Transform[] Levels;
    
    [SerializeField] private int[] lPoint;

    private MapData data;

    private void Awake() {
        // for(int i = 0; i < PathSequence.Length - 1; i++) {
        //     Debug.Log(PathSequence[1]);
        // }

        data = MapDM.GetMapData();
        CurrentPoint = data.CurrentPoint;


        MapDM.SetPointForLevels(lPoint[0], lPoint[1], lPoint[2], lPoint[3], lPoint[4], lPoint[5], lPoint[6], lPoint[7], lPoint[8], lPoint[9], lPoint[10], lPoint[11]);


    }

    private void OnDrawGizmos(){
        if(PathSequence == null || PathSequence.Length < 2) return;

        for(int i = 1; i < PathSequence.Length; i++) {
            //drawline from last point to current point
            Gizmos.DrawLine(PathSequence[i-1].position, PathSequence[i].position);
        }
    }

    public Transform GetNextPathPoint(int movementDirection = 1){
        CurrentPoint += movementDirection;
        MapDM.UpdateCurrentPoint(CurrentPoint);
        return PathSequence[CurrentPoint];
    }

    public Transform GetCurrentPoint() { // convert level to pathsequence point
        return PathSequence[CurrentPoint];
    }

    public bool checkPoint(int point) { //check if level is unlock
        int LevelNum = pointToLevel(point);
        data = MapDM.GetMapData();

        Debug.Log(JsonUtility.ToJson(data, true));

        switch (LevelNum - 1) { //check the last level
            case 1:
                return data.Level1Stars >= 1 ? true : false;
                break;
            case 2:
                return data.Level2Stars >= 1 ? true : false;
                break;
            case 3:
                return true;
                // return data.Level3Stars >= 1 ? true : false;
                break;
            case 4:
                return data.Level4Stars >= 1 ? true : false;
                break;
            case 5:
                return data.Level5Stars >= 1 ? true : false;
                break;
            case 6:
                return data.Level6Stars >= 1 ? true : false;
                break;
            case 7:
                return data.Level7Stars >= 1 ? true : false;
                break;
            case 8:
                return data.Level8Stars >= 1 ? true : false;
                break;
            case 9:
                return data.Level9Stars >= 1 ? true : false;
                break;
            case 10:
                return data.Level10Stars >= 1 ? true : false;
                // return true;
                break;
            case 11:
                // return data.Level11Stars >= 1 ? true : false;
                return true;
                break;
            case 12:
                return data.Level12Stars >= 1 ? true : false;
                break;
            default :
                return false;
                break;
        }
        

        return false;
    }

    public int pointToLevel(int point){
        return Array.IndexOf(Levels, PathSequence[point]) + 1;
    }

    public int UnlockAndGetLevel(int point) { // return Exact level number
        int LevelNum = pointToLevel(point);
        MapDM.UnlockNewLevel(LevelNum);
        return LevelNum;
    }

    public void NewCurrentPoint(int Level) {
        MapDM.UpdateCurrentPoint(Level);
    }
}
