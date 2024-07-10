using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDDoorUnlock : MonoBehaviour
{
    private enum OpenType{
        KeyOpen,
        LeverOpen
    }
    [SerializeField] private OpenType openType;
    [SerializeField] private float speed;
    
    private BoxCollider2D boxCollider;

    private int keys;
    private bool Move = false;

    Vector3 target;

    void Awake()
    {
        target = new Vector3(transform.position.x, transform.position.y - 2.01f, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && openType == OpenType.KeyOpen){
            keys = PlayerPrefs.GetInt("Keys");
            if(keys >= 1){
                PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") - 1);
                Move = true;
            }
        }
    }
    void Update()
    {
        
        if(Move){
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            if(transform.position == target) {
                Move = false;
            }       
        }
    }
    public void Unlock()
    {
        Move = true;
    }

}
