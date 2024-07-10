using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private UIManager manager;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //do the code first, then call levl complete
            manager.LevelComplete();
        }
    }
}
