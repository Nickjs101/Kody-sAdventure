using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchingGame : MonoBehaviour
{   

    [Header("Disable Traps")]
    [SerializeField] private GameObject[] Traps;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip CheckSound;

    [Header("UI Settings")]
    [SerializeField] private Text pointText;
    [SerializeField] private GameObject completeMessage;
    [SerializeField] private Text TimerText;
    [SerializeField] private GameObject Timer;
    [SerializeField] private float StartingTime;

    [Header("Player Settings")]
    [SerializeField] private GameObject Player;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private GameObject KEY;

    [Header("Task Settings")]
    [SerializeField] private WordClass[] words;
    [SerializeField] private RectTransform[] points;
    [SerializeField] private GameObject[] textObject;

    [SerializeField] private float dropCooldown;
    private float cooldownTimer;

    private int TotalPoints = 0;

    private bool canDrop = false;

    //timer
    private float currentTime;
    

    private void Awake() {
        for(int i = 0; i < Traps.Length; i++) {
            Traps[i].SetActive(false);
        }

        LeanTween.reset();
        
        TimerOpen();
    }
    private void Update() {
        pointText.text = "Word Catch: " + TotalPoints.ToString();

        //Timer
        if(currentTime > 0){
            currentTime -= 1 * Time.deltaTime;
            TimerText.text = currentTime.ToString("0");

            if(currentTime <= 0){
                currentTime = 0;
                TimerClose();
                canDrop = true;
            }
        } 

        if(TotalPoints == 10){
            TaskComplete();
            TotalPoints = 11; //this will stop calling TaskComplete Loop
        }

        cooldownTimer += Time.deltaTime;

        if(cooldownTimer > dropCooldown) {
            
            if(!canDrop) return;
            Drop();
            
            cooldownTimer = 0;
        }
    }

    private void Drop() {
        textObject[FindWordObj()].GetComponent<CatchingWord>().wordClass = words[Random.Range(0, words.Length)]; // i remove -1 in range
        textObject[FindWordObj()].GetComponent<RectTransform>().anchoredPosition = points[Random.Range(0, points.Length)].anchoredPosition;
        textObject[FindWordObj()].SetActive(true);
    }

    private int FindWordObj(){
        for(int i = 0; i < textObject.Length; i++) {
            if(!textObject[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    public void addPoints(int point) {
        TotalPoints += point / 10;
        Player.GetComponent<Scorer>().addScore(point);
    }

    public void subHealth(int damage) {
        Player.GetComponent<Health>().TakeDamage(damage);
    }

    private void TaskComplete(){
        SoundManager.instance.PlaySound(CheckSound);

        canDrop = false;
        playerControl.canControl = false;
        LeanTween.scale(completeMessage, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
        LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(2f).setOnComplete(() => {
            KEY.SetActive(true);

            for(int i = 0; i < Traps.Length; i++) {
                Traps[i].SetActive(true);
            }
            gameObject.SetActive(false);
        });

        
    }

    
    private void TimerClose() {
        LeanTween.scale(Timer,  Vector3.zero, .25f);
    }
    private void TimerOpen() {
        currentTime = StartingTime;
        LeanTween.scale(Timer,  new Vector3(1f,1f,1f),.25f);
    }
}
