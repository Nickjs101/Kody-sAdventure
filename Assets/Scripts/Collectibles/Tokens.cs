using UnityEngine;

public class Tokens : MonoBehaviour
{
    [Header ("Collect Sound")]
    [SerializeField] private AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            SoundManager.instance.PlaySound(collectSound);
            PlayerPrefs.SetInt("CurrentTokens", PlayerPrefs.GetInt("CurrentTokens") + 1);
            Destroy(this.gameObject);
        }
    }
}

