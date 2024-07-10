using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TFquizManaer : MonoBehaviour
{   
    [SerializeField] private Text ScoreText;
    [SerializeField] private GameObject isCorrectObj;
    [SerializeField] private Text isCorrectText;
    private string isCorrectMessage;
    //heal muna bigay
    [Header ("player Transform")]
    [SerializeField] private GameObject playerTransform;
    [SerializeField] private GameObject Item;
    [SerializeField] private int scorePerCorrect;
    [SerializeField] private int damagePerWrong;

    [Header ("Number of Questions")]
    [SerializeField] private int TotalCorrectNeed;

    [Header ("Available Questions")]
    [SerializeField] private TFQuestion[] AllQuestions;

    [Header ("Objects")]
    [SerializeField] private Text questionText;

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip WrongtSound;

    private static List<TFQuestion> ListOfQuestion;
    private TFQuestion currentQuestion;
    private int AnsweredCorrectly;

    
    private void OnEnable() {
        ReadyQuiz();
    }
    private void Update() {
        ScoreText.text = "Score: " + AnsweredCorrectly;
    }

    private void Awake() {

        LeanTween.reset();

        if(ListOfQuestion == null) return;
            ListOfQuestion.Clear();
    }

    private void GetQuestion() {
        if(AnsweredCorrectly != TotalCorrectNeed){
            int randomQuestionIndex = Random.Range(0, ListOfQuestion.Count);
            currentQuestion = ListOfQuestion[randomQuestionIndex];

            ShowQuestion();
            
            ListOfQuestion.RemoveAt(randomQuestionIndex);
            Debug.Log(ListOfQuestion.Count());
        }
        else{
                
            Debug.Log("END");
                
            CloseQuiz();
            
            if(Item != null){
                Item.SetActive(true);
            }
        }
        
    }

    private void ShowQuestion() {
        questionText.text = currentQuestion.question;
    }

    public void Answer(bool Answer) {
        
        if(Answer == currentQuestion.isTrue){
            Debug.Log("Correct!");
            AnsweredCorrectly++;

            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + scorePerCorrect);//try
            SoundManager.instance.PlaySound(correctSound);

            isCorrectText.color = new Color(0.2950789f, 1f, 0f, 1f);
            isCorrectMessage = "Correct!";
            StartCoroutine(AfterAnswer());
            
        }else{
            Debug.Log("wRONG!");

            playerTransform.GetComponent<Health>().TakeDamage(damagePerWrong);
            SoundManager.instance.PlaySound(WrongtSound);

            isCorrectText.color = new Color(1f, 0.3254762f, 0f, 1f);
            isCorrectMessage = "Wrong!";
            StartCoroutine(AfterAnswer());
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
        LeanTween.scale(gameObject, Vector3.zero, .12f);
    }

    private void ReadyQuiz() {
        
        AnsweredCorrectly = 0;
        
        
        if(ListOfQuestion == null || ListOfQuestion.Count == 0){
                ListOfQuestion = AllQuestions.ToList<TFQuestion>();
        }

        GetQuestion();
    }

    
    
}
