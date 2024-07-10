using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{   
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject isCorrectObj;
    [SerializeField] private Text isCorrectText;
    private string isCorrectMessage;
    
    [Header ("player Transform")]
    [SerializeField] private GameObject Item;
    [SerializeField] private int scorePerCorrect;
    [SerializeField] private int damagePerWrong;
    //heal muna bigay
    [Header ("player Transform")]
    [SerializeField] private GameObject playerTransform;

    [Header ("Number of Correct")]
    [SerializeField] private int QuestionCount;

    [Header ("Available Questions")]
    [SerializeField] private Question[] AllQuestions;

    [Header ("Objects")]
    [SerializeField] private Text questionText;
    [SerializeField] private Text[] optionText;

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip WrongtSound;

    private static List<Question> ListOfQuestion;
    private Question currentQuestion;
    private int QuestionAnswered = 0;

    public bool isCorrect;

    //boss
    private Animator BossAnimator; 
    private BossScript bossScript;
    
    private void Update() {
        ScoreText.text = "Score: " + QuestionAnswered;
    }
    private void OnEnable() {
        ReadyQuiz();
    }

    private void Awake() {
        isCorrect = false;
        
        if(ListOfQuestion == null) return;
            ListOfQuestion.Clear();
    }

    private void GetQuestion() {
        if(ListOfQuestion.Count != 0){
            int randomQuestionIndex = Random.Range(0, ListOfQuestion.Count - 1 );
            currentQuestion = ListOfQuestion[randomQuestionIndex];

            ShowQuestion();
            
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
        
        if(AnswerIndex == currentQuestion.answerIndex){
            Debug.Log("Correct!");

            

            QuestionAnswered++;

            if(QuestionAnswered == QuestionCount){
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scorePerCorrect);//try
                SoundManager.instance.PlaySound(correctSound);

                CloseQuiz();
                isCorrect = true;
                
                if(Item != null){
                    Item.SetActive(true);
                }
            }else{
                PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scorePerCorrect);//try
                SoundManager.instance.PlaySound(correctSound);
                
                isCorrectText.color = new Color(0.2950789f, 1f, 0f, 1f);
                isCorrectMessage = "Correct!";
                StartCoroutine(AfterAnswer());
            }
            
            
        }else{
            
            Debug.Log("wRONG!");
            playerTransform.GetComponent<Health>().TakeDamage(damagePerWrong);//try
            SoundManager.instance.PlaySound(WrongtSound);

            isCorrectText.color = new Color(1f, 0.3254762f, 0f, 1f);
            isCorrectMessage = "Wrong!";
            StartCoroutine(AfterAnswer());
            isCorrect = false;
        }
    }

    IEnumerator AfterAnswer()
    {   
        
        isCorrectText.text = isCorrectMessage;
        LeanTween.scale(isCorrectObj, Vector3.one, 2f).setEase(LeanTweenType.easeOutElastic);
        yield return new WaitForSeconds(2f);
        LeanTween.scale(isCorrectObj, Vector3.zero, .12f);
        GetQuestion();
    }

    private void CloseQuiz() {
        //gameObject.SetActive(false);
        LeanTween.scale(gameObject, Vector3.zero, .12f);
    }

    private void ReadyQuiz() {
        QuestionAnswered = 0;

        if(ListOfQuestion == null || ListOfQuestion.Count == 0){
                ListOfQuestion = AllQuestions.ToList<Question>();
        }

        GetQuestion();
    }

    
    
}
