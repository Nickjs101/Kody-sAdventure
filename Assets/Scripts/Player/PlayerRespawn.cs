using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip CpSound;
    private Transform CurrentCheckPoint;
    private Health playerHealth;
    //private UIManager uiManager;

    private void Awake() {
        playerHealth = GetComponent<Health>();
        //uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckpointRespawn() {
        //check if checkpoint is available, if not then gameover
        if(CurrentCheckPoint == null){
            //uiManager.GameOver();
            return;
        }

        //move the player to the last checkpoint
        transform.position = CurrentCheckPoint.position;
        //call respawn in Health script
        //playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Checkpoint"){
            CurrentCheckPoint = collision.transform;
            SoundManager.instance.PlaySound(CpSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
