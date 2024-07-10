using UnityEngine;

public class Information : MonoBehaviour
{
    [SerializeField] private GameObject message;

    void Awake()
    {
        message.transform.position = new Vector3(transform.position.x, transform.position.y + 2.82f, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            LeanTween.scale(message, Vector3.one, .12f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            LeanTween.scale(message, Vector3.zero, .12f);
        }
    }
}
