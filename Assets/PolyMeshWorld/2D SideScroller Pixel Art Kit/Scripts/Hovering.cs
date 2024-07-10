using UnityEngine;

public class Hovering : MonoBehaviour {
    
    private Vector3 originalPos;
    private Vector3 tempPosition;

    [Range(0, 5)] public float verticalSpeed;
    [Range(0, 5)] public float amplitude;

    private void Start() {
        tempPosition = originalPos = transform.position;
    }

    private void FixedUpdate() {
        tempPosition = originalPos;
        tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;
        transform.position = tempPosition;
    }
}
