using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public List<Transform> target;
    public Vector3 offset;
    [Range(1,10)] [SerializeField] private float smoothFactor;
    [SerializeField] private Vector3 minValues, maxValues;

    void LateUpdate() {
        Follow();
    }

    void Follow() {
        if(target.Count == 0)return;

        Vector3 targetPosition = GetCenterpoint() + offset;
        //Verify if the targetPosition is out of bound or not. Limit it to the min and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x), 
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    Vector3 GetCenterpoint(){
        if(target.Count == 1){
            return target[0].position;
        }
        var bounds = new Bounds(target[0].position, Vector3.zero);
        for(int i = 0; i < target.Count; i++) {
            bounds.Encapsulate(target[i].position);
        }

        return bounds.center;
    }
}
