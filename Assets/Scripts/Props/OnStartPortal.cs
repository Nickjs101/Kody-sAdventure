using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartPortal : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform PlayerPos;

    private void Awake() {
        LeanTween.reset();
        Player.transform.localScale = Vector3.zero;
        Player.transform.position = transform.position;

        Player.SetActive(false);

        StartCoroutine(Opening());
        
    }

    IEnumerator Opening(){
        yield return new WaitForSeconds(.5f);
        Player.SetActive(true);
        LeanTween.scale(Player, Vector3.one, .5f);
        LeanTween.move(Player, PlayerPos.position, .5f);
    }
}

