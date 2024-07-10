using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;
using System;


public class StatementComment : MonoBehaviour
{   
    
    [SerializeField] private GameObject Key;
    [SerializeField] private Transform Player;
    [SerializeField] private int pointsPerCorrect;
    [SerializeField] private int damagePerWrong;
    [SerializeField] private Text[] ItemHolders;

    [SerializeField] private GameObject[] StatementSlots;
    [SerializeField] private GameObject[] StatementCheck;

    [SerializeField] private GameObject[] CommentSlots;
    [SerializeField] private GameObject[] CommentCheck;


    [SerializeField] private SOCItems[] WordBankStatements;
    [SerializeField] private SOCItems[] WordBankComments;

    [SerializeField] private AudioClip CheckSound;

    [SerializeField] private Sprite CorrectImg;
    [SerializeField] private Sprite WrongImg;

    [SerializeField] private Transform ItemGroup;

    [SerializeField] private GameObject completeMessage;


    [Header("Time Before Start")]
    [SerializeField] private Text TimerText;
    [SerializeField] private GameObject Timer;
    [SerializeField] private float StartingTime;

    private List<SOCItems> playerStatements = new List<SOCItems>(); // S word bank
    private List<SOCItems> playerComments = new List<SOCItems>(); // C word bank

    private List<int> playerStatementsIndex = new List<int>(); //pick S words
    private List<int> playerCommentsIndex = new List<int>(); //pick C word

    private List<SOCItems> playerWords = new List<SOCItems>(); // all picked word

    public bool isCompleted = false;

    int numberOfCorrects = 0;

    //timer
    private float currentTime;

    private void Awake() {
        ClearLists();
        disableChecks();

        

        //get statement items from wordbank
        for(int i = 0; i < 3; i++) {

            int index = Random.Range(0, WordBankStatements.Length - 1);

            while(playerStatementsIndex.Contains(index)) {
                index = Random.Range(0, WordBankStatements.Length - 1);
            }
            
            playerStatementsIndex.Add(index);

            playerStatements.Add(WordBankStatements[index]);  

        }

        //get comment items from wordbank
        for(int i = 0; i < 3; i++) {

            int index = Random.Range(0, WordBankComments.Length - 1);

            while(playerCommentsIndex.Contains(index)) {
                index = Random.Range(0, WordBankComments.Length - 1);
            }
            
            playerCommentsIndex.Add(index);

            playerComments.Add(WordBankComments[index]);  
        }


        playerWords.AddRange(playerStatements);
        playerWords.AddRange(playerComments);

        //input items to holders
        for(int i = 0; i < ItemHolders.Length; i++) {
            int randint = Random.Range(0, playerWords.Count - 1);
            ItemHolders[i].text = playerWords[randint].word;
            playerWords.RemoveAt(randint);
        }
        

        for(int i = 0; i < playerWords.Count; i++) {
            Debug.Log(playerWords[i].word);
        }

        
       
    }

    public void Submit() {
        for(int i = 0; i < ItemHolders.Length; i++) {
            if(ItemHolders[i].transform.parent == ItemGroup) return;
        }
        numberOfCorrects = 0;
        SoundManager.instance.PlaySound(CheckSound);
        checkStatementSlots();
        checkCommentSlots();

        if(numberOfCorrects != 6 ){
            Player.GetComponent<Health>().TakeDamage(damagePerWrong);
            StartCoroutine(resetAfterSeconds());
        }else{
            LeanTween.scale(completeMessage, Vector3.one, 1f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
            LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(2f);
            Player.GetComponent<Scorer>().addScore(pointsPerCorrect);
            Key.SetActive(true);
            isCompleted = true;
        }
    }

    private void checkStatementSlots() {

        for(int i = 0; i < StatementSlots.Length; i++) {
            if(StatementSlots[i].transform.GetChild(0) != null){
                string text = StatementSlots[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;
            
                if(isStatement(text)){
                    StatementCheck[i].GetComponent<Image>().sprite = CorrectImg;
                    StatementCheck[i].SetActive(true);
                    
                    numberOfCorrects += 1;  
                    
                    
                    
                }else{
                    StatementCheck[i].GetComponent<Image>().sprite = WrongImg;

                    StatementCheck[i].SetActive(true);
                    
                    
                }
            }
        }
    }
    private bool isStatement(string text){
        bool inStatements = false;
        for(int i = 0; i < playerStatements.Count; i++) {
            if(playerStatements[i].word == text){
                inStatements = true;
            }
        }
        return inStatements;
    }



    private void checkCommentSlots() {
        for(int i = 0; i < CommentSlots.Length; i++) {
            if(CommentSlots[i].transform.GetChild(0) != null){
                string text = CommentSlots[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;
            
                if(isComment(text)){
                    CommentCheck[i].GetComponent<Image>().sprite = CorrectImg;
                    CommentCheck[i].SetActive(true);
                    
                    numberOfCorrects += 1;  

                }else{
                    CommentCheck[i].GetComponent<Image>().sprite = WrongImg;
                    CommentCheck[i].SetActive(true);
                    
                    // Player.GetComponent<Health>().TakeDamage(damagePerWrong);
                }
            }
        }
    }
    private bool isComment(string text){
        bool inComments = false;
        for(int i = 0; i < playerComments.Count; i++) {
            if(playerComments[i].word == text){
                inComments = true;
            }
        }
        return inComments;
    }


    private void ClearLists() {
        playerStatements.Clear();
        playerComments.Clear();
        playerWords.Clear();
        playerStatementsIndex.Clear();
        playerCommentsIndex.Clear();
    }

    private void disableChecks() {
        for(int i = 0; i < StatementCheck.Length; i++) {
            StatementCheck[i].SetActive(false);
        }
        for(int i = 0; i < CommentCheck.Length; i++) {
            CommentCheck[i].SetActive(false);
        }
    }

    IEnumerator resetAfterSeconds(){
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < ItemHolders.Length; i++) {
            ItemHolders[i].transform.SetParent(null);
        }

        disableChecks();
    }

    private void TimerClose() {
        LeanTween.scale(Timer,  Vector3.zero, .25f);
    }
    public void TimerOpen() {
        currentTime = StartingTime;
        LeanTween.scale(Timer,  new Vector3(1f,1f,1f),.25f);
    }

    private void Update() {
        //Timer
        if(currentTime > 0){
            currentTime -= 1 * Time.deltaTime;
            TimerText.text = currentTime.ToString("0");

            if(currentTime <= 0){
                currentTime = 0;
                TimerClose();
            }
        } 
    }
}
