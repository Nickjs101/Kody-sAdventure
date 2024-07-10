using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool canControl = true;

    private enum catchType{
        Keyword,
        Identifier
    }
    [SerializeField] private int pointPerCatch;
    [SerializeField] private int damagePerCatch;
    [SerializeField] private catchType type;

    [SerializeField] private CatchingGame catchingGame;

    int score = 0;

    private void Update() {
        if(!canControl) return;
        this.transform.position = new Vector2(Input.mousePosition.x,transform.position.y) ;  
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!canControl) return;
        if(type == catchType.Keyword){
            if(other.GetComponent<CatchingWord>().wordClass.isKeyword){
                Debug.Log(other.GetComponent<CatchingWord>().wordClass.isKeyword + " "+ other.GetComponent<CatchingWord>().wordClass.word+ " Score "+ score++);
                catchingGame.addPoints(pointPerCatch);
            }else{
                Debug.Log(other.GetComponent<CatchingWord>().wordClass.isKeyword  + " "+ other.GetComponent<CatchingWord>().wordClass.word + " Damage");
                catchingGame.subHealth(damagePerCatch);
            }
        }else{
            if(!other.GetComponent<CatchingWord>().wordClass.isKeyword){
                Debug.Log(other.GetComponent<CatchingWord>().wordClass.isKeyword + " " + other.GetComponent<CatchingWord>().wordClass.word+ " Score");
                catchingGame.addPoints(pointPerCatch);
            }else{
                Debug.Log(other.GetComponent<CatchingWord>().wordClass.isKeyword  + " "+ other.GetComponent<CatchingWord>().wordClass.word + " Damage");
                catchingGame.subHealth(damagePerCatch);
            }
        }
        
    }
}
