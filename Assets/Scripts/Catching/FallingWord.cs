using UnityEngine;
using UnityEngine.UI;

public class FallingWord : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private CatchingWord myWord;

    private void Awake() {

        // myWord = GetComponent<CatchingWord>();

        // transform.GetChild(0).GetComponent<Text>().text = myWord.wordClass.word; // add getchild
    }

    private void Update() {
        myWord = GetComponent<CatchingWord>();

        transform.GetChild(0).GetComponent<Text>().text = myWord.wordClass.word;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Floor"){
            gameObject.SetActive(false);
        }
        if(other.tag == "Player"){
            gameObject.SetActive(false);
        }
    }
}
