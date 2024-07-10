using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [Header ("DisableObjects")]
    [SerializeField] private GameObject[] DisableObjects;

    [Header ("DisablePlayerBehaviours")]
    [SerializeField] private GameObject Player;

    [Header ("Camera")]
    [SerializeField] private GameObject Camera;

    [SerializeField] private GameObject Boss;
    [SerializeField] private Vector3 _offset;

    [Header ("Dialogue trigger")]
    [SerializeField] private GameObject Dialogue;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    [Header ("Player Place Object")]
    [SerializeField] private GameObject playerPlace;

    [Header ("UI")]
    [SerializeField] private GameObject BossHealthBar;
    [SerializeField] private GameObject MenuButton;


    public bool inPosition = false;
    private bool Continue = false;
    private Animator BossAnimator; 
    private BossScript bossScript;

    void Awake()
    {
        BossAnimator = Boss.GetComponent<Animator>();
        bossScript = Boss.GetComponent<BossScript>();

        for(int i = 0; i < DisableObjects.Length; i++) {
            DisableObjects[i].SetActive(false);
        }
    }
   
    private void Update() { 

        if(inPosition){
            StartCoroutine(BossStart());
            playerPlace.SetActive(false);
            LeanTween.moveY(MenuButton.GetComponent<RectTransform>(), -170f, .5f);
            LeanTween.moveX(BossHealthBar.GetComponent<RectTransform>(), -247f, .5f).setDelay(.5f);
            inPosition = false;
        }
        if(Continue){
            BossAnimator.SetTrigger("intro");
            dialogueTrigger.TriggerDialogue();
            Continue = false;
        }
            
        
    }

    IEnumerator BossStart()
    {
        
        Player.GetComponent<PlayerMovement>().enableMovement = false;
        Camera.GetComponent<CameraFollow>().target.Add(Boss.transform);
        Camera.GetComponent<CameraFollow>().offset = _offset;
        yield return new WaitForSeconds(1);
        Continue = true;
        
        //OpenDialogQuiz
    }


}
