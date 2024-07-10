using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OutputGuess2 : MonoBehaviour
{
    [SerializeField] private GameObject OnCompleteObject;
    [SerializeField] private Behaviour OnCompleteBehavior;
    [SerializeField] private FireTrap[] Firetrap;
    [Header("Player Setup")]
    [SerializeField] private GameObject Player;
    [SerializeField] private int pointsPerCorrect;
    [SerializeField] private int damagePerWrong;

    [Header("Quiz Setup")]
    [SerializeField] private int TargetCorrect;
    [SerializeField] private OGQuestion[] Questions;
    [SerializeField] private Text AnswerField;
    [SerializeField] private Text QuestionField;
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

    private void ShowQuestion() {
        QuestionField.text = "";
        AnswerField.text = "";
        CheckHolder.SetActive(false);

        int index = Random.Range(0, QuestionList.Count);
        currentQuestion = QuestionList[index];

        StartCoroutine(TypeQuestion(currentQuestion.Question));

        if(currentQuestion.Input != ""){
            StartCoroutine(TypeInput(currentQuestion.Input, currentQuestion.Question.ToCharArray().Length * .1f));
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

    IEnumerator ShowQuestionEnum(){
        yield return new WaitForSeconds(1f);
        ShowQuestion();
    }

    IEnumerator TypeQuestion(string text){

		foreach (char letter in text.ToCharArray())
		{
			QuestionField.text += letter;
			yield return new WaitForSeconds(.1f);
		}
    }

    IEnumerator TypeInput(string input, float delay){
        yield return new WaitForSeconds(delay);

		foreach (char letter in input.ToCharArray())
		{
			AnswerField.text += letter;
			yield return new WaitForSeconds(.1f);
		}
    }

    IEnumerator TypeAnswer(string answer){

		foreach (char letter in answer.ToCharArray())
		{
			AnswerField.text += letter;
			yield return new WaitForSeconds(.1f);
		}

        checkAnswer(answer);
    }

    public void Submit(int index) {
        string answerText = OptionFields[index].text;

        StartCoroutine(TypeAnswer(answerText));
    }

    private void checkAnswer(string answer) {
        if(answer == currentQuestion.Answer){
            CheckHolder.GetComponent<Image>().sprite = CorrectImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(CheckSound);
            Corrects++;

            Player.GetComponent<Scorer>().addScore(pointsPerCorrect);

            if(Corrects == TargetCorrect){
                //end Close
                LeanTween.scale(completeMessage, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
                LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(2f);

                if(OnCompleteObject != null){
                    OnCompleteObject.SetActive(true);
                }
                if(OnCompleteBehavior != null){
                    OnCompleteBehavior.enabled = true;
                }

                for(int i = 0; i < Firetrap.Length; i++) {
                    Firetrap[i].TrapOFF();
                }

                gameObject.SetActive(false);

            }else{
                StartCoroutine(ShowQuestionEnum());
            }
        }else{
            //wrong
            CheckHolder.GetComponent<Image>().sprite = WrongImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(WrongSound);

            Player.GetComponent<Health>().TakeDamage(damagePerWrong);

            StartCoroutine(ShowQuestionEnum());
        }
    }
}
