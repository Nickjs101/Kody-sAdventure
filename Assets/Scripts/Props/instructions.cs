using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructions : MonoBehaviour
{
    [SerializeField] private GameObject instruction;

    [SerializeField] private AudioClip CompletionSound;
    [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject LadderWall;
    [SerializeField] private GameObject TriggerWall;
    [SerializeField] private GameObject AttackWall;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject testEnemy;
    [Header ("JoyStick")] 
    [SerializeField] private Joystick joystick;

    private bool Intruction1isCompleted = false;
    private bool Intruction2isCompleted = false;

    private float leftClick = 0;
    private float rightClick = 0;

    private Title instructText;

    bool CheckingInstruction1 = true;
    bool CheckingInstruction2 = false;
    bool CheckingInstruction3 = false;
    bool CheckingInstruction4 = false;

    private bool titleFirst = true;

    private void Awake() {
        instructText = instruction.GetComponent<Title>();
        instructText.title = "MOVE THE JOYSTICK LEFT AND RIGHT TO PROCEED";
        StartCoroutine(TitleFirst());
    }

    IEnumerator TitleFirst(){
        yield return new WaitForSeconds(2f);
        titleFirst = false;
        instruction.SetActive(true);
    }

    void Update()
    {
        if(CheckingInstruction1){

            if(Intruction1isCompleted == false && !titleFirst){
                if(Input.GetAxis("Horizontal") > 0 || joystick.Horizontal > 0){
                    rightClick = 1f;
                }
                if(Input.GetAxis("Horizontal") < 0  || joystick.Horizontal < 0){
                    leftClick = 1f;
                }
                if(leftClick == 1 && rightClick == 1){
                    Intruction1isCompleted = true;
                    instruction.SetActive(false);
                    SoundManager.instance.PlaySound(CompletionSound);
                }
            }
            else{
                if(playerTransform.position.x < Wall1.transform.position.x){
                    Wall1.SetActive(false);
                    Wall2.SetActive(false);
                }else{
                    Wall1.SetActive(true);
                    Wall2.SetActive(true); 

                    instructText.title = "JUMP OVER THE ENEMY TO DEFEAT IT";
                    instruction.SetActive(true);
                }
                
            }

            if(Intruction2isCompleted == false){

                if(testEnemy.activeSelf == false){

                    Intruction2isCompleted = true;
                    instruction.SetActive(false);
                    SoundManager.instance.PlaySound(CompletionSound);
                    Wall1.SetActive(false);
                    Wall2.SetActive(false);
                    CheckingInstruction1 = false;
                    CheckingInstruction2 = true;
                    // gameObject.SetActive(false);

                }

            }
        }

        if(CheckingInstruction2){
            if(playerTransform.position.x < LadderWall.transform.position.x) return;

            instructText.title = "MOVE THE JOYSTICK UP TO CLIMB THE LADDER";
            instruction.SetActive(true);

            StartCoroutine(disableInstruction());
            CheckingInstruction2 = false;
            CheckingInstruction3 = true;
        }

        if(CheckingInstruction3){

            if(playerTransform.position.x < TriggerWall.transform.position.x) return;

            instructText.title = "USE TRIGGER BUTTON TO TURN ON / UNLOCK OBJECTS";
            instruction.SetActive(true);

            StartCoroutine(disableInstruction());
            CheckingInstruction3 = false;
            CheckingInstruction4 = true;
        }

        if(CheckingInstruction4){
            if(playerTransform.position.x < AttackWall.transform.position.x) return;

            instructText.title = "USE ATTACK BUTTON TO SHOOT THE ENEMIES";
            instruction.SetActive(true);

            StartCoroutine(disableInstruction());
            CheckingInstruction4 = false;
        }
    }

    IEnumerator disableInstruction(){
        yield return new WaitForSeconds(5);
        instruction.SetActive(false);
    }

    
}
