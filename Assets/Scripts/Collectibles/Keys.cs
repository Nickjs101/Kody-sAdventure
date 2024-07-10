using UnityEngine;

public class Keys : MonoBehaviour
{
    [Header ("Collect Sound")]
    [SerializeField] private AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            SoundManager.instance.PlaySound(collectSound);
            PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") + 1);
            Destroy(this.gameObject);
        }
    }
}
