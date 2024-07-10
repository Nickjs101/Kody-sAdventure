using UnityEngine;

public class CameraController : MonoBehaviour
{
    //follow player
    [SerializeField] private Transform player;//this input for player object
    [SerializeField] private float aheadDistance;//number of distance the camera will show advance view
    [SerializeField] private float cameraSpeed;//speed of the camera movement
    private float lookAhead;//total calculation of advance view

    [SerializeField] private Transform leftBorder;
    [SerializeField] private Transform rightBorder;
    [SerializeField] private float rightAdjust;
    [SerializeField] private float leftAdjust;


    private void Update() {
        
        //computation of advance view
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        //change camera position horizontally with addition to lookAhead distance
        //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(player.position.x + lookAhead, leftBorder.position.x - leftAdjust, rightBorder.position.x - rightAdjust), 
                            transform.position.y, transform.position.z);
        
    }
}
