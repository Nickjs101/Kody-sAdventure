using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BossQuizManager : MonoBehaviour
{   
    [SerializeField] private UIManager uiManager;
    [Header ("Player & BossHealth")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health bossHealth;

    [Header ("Timer")]
    [SerializeField] private Text TimerText;
    [SerializeField] private GameObject Timer;
    [SerializeField] private float StartingTime;
    [SerializeField] private float waitingTime;
    //heal muna bigay
    [Header ("player & boss")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Boss;
    [Header ("Number of Questions")]
    [SerializeField] private int QuestionCount;

    [Header ("Available Questions")]
    [SerializeField] private Question[] AllQuestions;

    [Header ("Objects")]
    [SerializeField] private Text questionText;
    [SerializeField] private Text[] optionText;
    [SerializeField] private Button[] optionButton;

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip WrongtSound;

    private static List<Question> ListOfQuestion;
    private Question currentQuestion;
    private int QuestionAnswered;

    //timer
    private float currentTime;

    //boss
    private Animator BossAnimator; 
    private BossScript bossScript;
    private PlayerAttack playerAttack;

    private bool isComplete = false;

    private void OnEnable() {
        ReadyQuiz();

        
    }

    private void Awake() {
        //ReadyQuiz();


        BossAnimator = Boss.GetComponent<Animator>();
        bossScript = Boss.GetComponent<BossScript>();
        playerAttack = Player.GetComponent<PlayerAttack>();
    }

    private void GetQuestion() {

        Debug.Log(ListOfQuestion.Count);

        if(ListOfQuestion.Count != 0){//check kung nareach na yung q na pwede nya sagutan
            int randomQuestionIndex = Random.Range(0, ListOfQuestion.Count);
            currentQuestion = ListOfQuestion[randomQuestionIndex];

            ShowQuestion();

            TimerOpen();//start timer
            
            ListOfQuestion.RemoveAt(randomQuestionIndex);
        }
        else{
            Debug.Log("END");

                CloseQuiz();
            
        }
        
    }

    private void ShowQuestion() {
        questionText.text = currentQuestion.question;

        for(int i = 0; i < 4; i++) {
            optionText[i].text = currentQuestion.options[i];
        }
    }

    public void Answer(int AnswerIndex) {
        QuestionAnswered++;
        if(AnswerIndex == currentQuestion.answerIndex){
            TimerClose();
            StartCoroutine(PlayerAttack());
            
        }else{ 
            TimerClose();
            StartCoroutine(BossAttack());
            
        }
    }

    private void CloseQuiz() {
        //gameObject.SetActive(false);
        LeanTween.scale(gameObject, Vector3.zero, .12f);

        if(playerHealth.currentHealth <= 0){
            
            uiManager.GameOver();
        }else{
            uiManager.LevelComplete();
        }
            
    }

    private void ReadyQuiz() {
        QuestionAnswered = 0;

        ListOfQuestion = new List<Question>();

        if(ListOfQuestion == null || ListOfQuestion.Count == 0){
                ListOfQuestion = AllQuestions.ToList<Question>();
        }

        GetQuestion();
    }

    
    
    IEnumerator PlayerAttack()
    {
        playerAttack.Attack();
        isQuizInteractable(false);
        yield return new WaitForSeconds(waitingTime);
        isQuizInteractable(true);
        GetQuestion();
    }
    IEnumerator BossAttack()
    {
        bossScript.Attack();
        isQuizInteractable(false);
        yield return new WaitForSeconds(waitingTime);
        isQuizInteractable(true);
        GetQuestion();
    }

    private void TimerClose() {
        LeanTween.scale(Timer,  Vector3.zero, .25f);
    }
    private void TimerOpen() {
        currentTime = StartingTime;
        LeanTween.scale(Timer,  new Vector3(1f,1f,1f),.25f);
    }

    void Update()
    {
        if(currentTime > 0){
            currentTime -= 1 * Time.deltaTime;
            TimerText.text = currentTime.ToString("0");

            if(currentTime <= 0){
                currentTime = 0;
                TimerClose();
                StartCoroutine(BossAttack());
            }
        } 

        if(playerHealth.currentHealth <= 0 || bossHealth.currentHealth <= 0 ){
            if(isComplete) return;
                CloseQuiz();
                isComplete = true;
        }
            
    }

    private void isQuizInteractable(bool isInteractable) {
        optionButton[0].interactable = isInteractable;
        optionButton[1].interactable = isInteractable;
        optionButton[2].interactable = isInteractable;
        optionButton[3].interactable = isInteractable;
    }
    
    
}
