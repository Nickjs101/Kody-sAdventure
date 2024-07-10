using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CompleteCode : MonoBehaviour
{
    [SerializeField] private GameObject ToActivate;
    [SerializeField] private Text QuestionField;
    [SerializeField] private GameObject[] Questions;
    [SerializeField] private Text[] DragableIndex;

    [Header("Player Setup")]
    [SerializeField] private GameObject Player;
    [SerializeField] private int pointsPerCorrect;
    [SerializeField] private int damagePerWrong;

    [Header("Action Setup")]
    [SerializeField] private GameObject CheckHolder;
    [SerializeField] private Sprite CorrectImg;
    [SerializeField] private Sprite WrongImg;
    [SerializeField] private AudioClip CheckSound;
    [SerializeField] private AudioClip WrongSound;
    [SerializeField] private GameObject completeMessage;

    private GameObject currentQuestion;

    private static List <string> OptionList; 


    private void Awake() {
        LeanTween.reset();
        CheckHolder.SetActive(false);


        currentQuestion = Questions[Random.Range(0, Questions.Length)];

        QuestionField.text = currentQuestion.GetComponent<CompleteQuestion>().Question;


        setOptions();

        currentQuestion.SetActive(true);
    }

    public void submit(string answer) {
        if(currentQuestion.GetComponent<CompleteQuestion>().answer == answer){
            //ADD POINTS
            Player.GetComponent<Scorer>().addScore(pointsPerCorrect);

            ToActivate.SetActive(true);

            //Correct
            CheckHolder.GetComponent<Image>().sprite = CorrectImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(CheckSound);

            LeanTween.scale(completeMessage, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
            LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(3f);

        }else{

            //DAMAGE PLAYER
            Player.GetComponent<Health>().TakeDamage(damagePerWrong);

            CheckHolder.GetComponent<Image>().sprite = WrongImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(WrongSound);
            
            StartCoroutine(resetAfterSeconds());

        }
    }

    

    IEnumerator resetAfterSeconds(){
        yield return new WaitForSeconds(2);

        CheckHolder.SetActive(false);

        currentQuestion.GetComponent<CompleteQuestion>().slot.transform.DetachChildren();

    }

    void setOptions() {

        if(OptionList != null) {
            OptionList.Clear();
        }

        if(OptionList == null || OptionList.Count == 0){
            OptionList = currentQuestion.GetComponent<CompleteQuestion>().options.ToList<string>();
        }

        for(int i = 0; i < 4; i++) {
            int ind = Random.Range(0, OptionList.Count);

            DragableIndex[i].text = OptionList[ind];
            OptionList.RemoveAt(ind);
        }
    }
}
