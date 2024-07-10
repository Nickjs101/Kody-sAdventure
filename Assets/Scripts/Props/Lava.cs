using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.gameObject.SetActive(false);
            uiManager.GameOver();
        }
    }
}
