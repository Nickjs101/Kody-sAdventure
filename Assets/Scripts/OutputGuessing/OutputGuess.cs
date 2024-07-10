using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class OutputGuess : MonoBehaviour
{
    [Header("Player Setup")]
    [SerializeField] private GameObject Player;
    [SerializeField] private int pointsPerCorrect;
    [SerializeField] private int damagePerWrong;

    [Header("Quiz Setup")]
    [SerializeField] private int TargetCorrect;
    [SerializeField] private OGQuestion[] Questions;
    [SerializeField] private GameObject AnswerField;
    [SerializeField] private Text QuestionField;
    [SerializeField] private Text InputField;
    [SerializeField] private Text[] OptionFields;

    [Header("Action Setup")]
    [SerializeField] private GameObject CheckHolder;
    [SerializeField] private Sprite CorrectImg;
    [SerializeField] private Sprite WrongImg;
    [SerializeField] private AudioClip CheckSound;
    [SerializeField] private AudioClip WrongSound;
    [SerializeField] private GameObject completeMessage;

    private static List<OGQuestion> QuestionList;

    private OGQuestion currentQuestion;

    private List <string> currentOption;

    private int Corrects = 0;

    private void Awake() {
        LeanTween.reset();

        if(QuestionList != null){
            QuestionList.Clear();
        }
            
        if(QuestionList == null || QuestionList.Count == 0){
            QuestionList = Questions.ToList<OGQuestion>();
        }

        ShowQuestion();

    }

    public void Submit() {
        string answer = AnswerField.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text;

        if(answer.Equals(currentQuestion.Answer)){
            //Correct
            //Add points
            //add 1 to correct answer;
            //get new Question if < Required
            CheckHolder.GetComponent<Image>().sprite = CorrectImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(CheckSound);

            // Player.GetComponent<Scorer>().addScore(pointsPerCorrect);

            Corrects++;

            if(Corrects == TargetCorrect){
                //end Close
                LeanTween.scale(completeMessage, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
                LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(2f);
            }else{
                StartCoroutine(ShowQuestionEnum());
            }



        }else{
            //wrong
            //damage player
            CheckHolder.GetComponent<Image>().sprite = WrongImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(WrongSound);

            // Player.GetComponent<Health>().TakeDamage(damagePerWrong);

            StartCoroutine(ShowQuestionEnum());
        }
    }

    IEnumerator ShowQuestionEnum(){
        yield return new WaitForSeconds(1f);
        ShowQuestion();
    }

    private void ShowQuestion() {
        AnswerField.transform.DetachChildren();
        CheckHolder.SetActive(false);

        int index = Random.Range(0, QuestionList.Count);
        currentQuestion = QuestionList[index];

        QuestionField.text = currentQuestion.Question;
        
        if(InputField != null){
            InputField.text = currentQuestion.Input;
        }
        
        
        insertOptions();

        QuestionList.RemoveAt(index);
    }

    private void insertOptions() {
        if(currentOption != null) {
            currentOption.Clear();
        }

        currentOption = currentQuestion.Options.ToList<string>();

        for(int i = 0; i < OptionFields.Length; i++) {
            int index = Random.Range(0, currentOption.Count);

            OptionFields[i].text = currentOption[index];

            currentOption.RemoveAt(index);
        }
    }

}
