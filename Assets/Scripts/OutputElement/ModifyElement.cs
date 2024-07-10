using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ModifyElement : MonoBehaviour
{
    [SerializeField] private GameObject ToDisable;
    [SerializeField] private Text QuestionField;
    [SerializeField] private GameObject[] Questions;
    [SerializeField] private Text OutputField;
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

        OutputField.text = "";

        currentQuestion = Questions[Random.Range(0, Questions.Length)];

        QuestionField.text = currentQuestion.GetComponent<ModifyListQuest>().Question;


        setOptions();

        currentQuestion.SetActive(true);
    }

    public void submit(string answer) {
        StartCoroutine(TypeAnswer(answer));
    }

    IEnumerator TypeAnswer(string answer){
        ModifyListQuest MLQ = currentQuestion.GetComponent<ModifyListQuest>();
        
        List<string> list = MLQ.list.ToList<string>();
        
        if(currentQuestion.GetComponent<ModifyListQuest>().questionType == currentQuestion.GetComponent<ModifyListQuest>().change) {
            list[MLQ.indexToChange] = answer;
        }
        
        else if(currentQuestion.GetComponent<ModifyListQuest>().questionType == currentQuestion.GetComponent<ModifyListQuest>().add) {
            list.Add(answer);
        }
        
        else if(currentQuestion.GetComponent<ModifyListQuest>().questionType == currentQuestion.GetComponent<ModifyListQuest>().remove) {
            list.Remove(answer);
        }

        string stringList = string.Format("[{0}]", string.Join(", ", list));
        
        foreach (char letter in stringList.ToCharArray())
		{
			OutputField.text += letter;
			yield return new WaitForSeconds(.05f);
		}

        if(answer.Equals(MLQ.answer)){
            Debug.Log("Correct");

            //ADD POINTS
            Player.GetComponent<Scorer>().addScore(pointsPerCorrect);

            ToDisable.SetActive(false);
            
            //Correct
            CheckHolder.GetComponent<Image>().sprite = CorrectImg;
            CheckHolder.SetActive(true);
            SoundManager.instance.PlaySound(CheckSound);

            LeanTween.scale(completeMessage, Vector3.one, 1f).setEase(LeanTweenType.easeOutBack);
            LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(3f);

        }else{
            Debug.Log("Wrong");
            //Wrong

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

        currentQuestion.GetComponent<ModifyListQuest>().slot.transform.DetachChildren();

        OutputField.text = "";
    }

    void setOptions() {

        if(OptionList != null) {
            OptionList.Clear();
        }

        if(OptionList == null || OptionList.Count == 0){
            OptionList = currentQuestion.GetComponent<ModifyListQuest>().options.ToList<string>();
        }

        for(int i = 0; i < 4; i++) {
            int ind = Random.Range(0, OptionList.Count);

            DragableIndex[i].text = OptionList[ind];
            OptionList.RemoveAt(ind);
        }
    }
}

