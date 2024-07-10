using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonViewer : MonoBehaviour
{
    [SerializeField] private GameObject lessonViewer;
    [SerializeField] private GameObject Key;


    private void Awake() {
        lessonViewer.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);

        if(Key != null){
            Key.transform.position = new Vector3(transform.position.x - .7f, transform.position.y, transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            LeanTween.scale(lessonViewer, new Vector3(.01f, .01f, .01f), .12f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            LeanTween.scale(lessonViewer, Vector3.zero, .12f);
        }
    }
}
