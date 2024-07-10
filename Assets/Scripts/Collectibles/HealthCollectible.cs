using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [Header ("Collect Sound")]
    [SerializeField] private AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            SoundManager.instance.PlaySound(collectSound);
            collision.GetComponent<Health>().addHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
