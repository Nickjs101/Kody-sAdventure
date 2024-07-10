using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OutputElement : MonoBehaviour
{
    [SerializeField] private BouncePad bouncepad;
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

    private static List<GameObject> ListOfQuestion;
    
    private GameObject currentQuestion;
    private ListQuest currentList;

    private static List <Text> DragableItems;

    private void Awake() {
        if(ListOfQuestion == null || ListOfQuestion.Count == 0){
            ListOfQuestion = Questions.ToList<GameObject>();
        }

        GetQuestion();
    }

    private void GetQuestion() {
        CheckHolder.SetActive(false);

        OutputField.text = "";

        currentQuestion = Questions[Random.Range(0, ListOfQuestion.Count)];
        currentList = currentQuestion.GetComponent<ListQuest>();

        QuestionField.text = currentList.Question;

        currentQuestion.SetActive(true);
        
        setDragableItems();
    }





    public void submit(string index) {
        StartCoroutine(TypeAnswer(index));
    }

    IEnumerator TypeAnswer(string index){
        int idx = int.Parse(index);
        string answer = currentList.answer;
        
        foreach (char letter in currentList.list[idx].ToCharArray())
		{
			OutputField.text += letter;
			yield return new WaitForSeconds(.05f);
		}

        if(OutputField.text.Equals(currentList.answer)){
            Debug.Log("Correct");

            //ADD POINTS
            Player.GetComponent<Scorer>().addScore(pointsPerCorrect);

            bouncepad.enabled = true;
            
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

        currentList.slot.transform.DetachChildren();

        OutputField.text = "";
    }

    private void setDragableItems() {
        if(DragableItems != null) {
            DragableItems.Clear();
        }

        if(DragableItems == null || DragableItems.Count == 0){
            DragableItems = DragableIndex.ToList<Text>();
        }

        for(int i = 0; i < 4; i++) {
            int ind = Random.Range(0, DragableItems.Count);
            DragableItems[ind].text = i.ToString();
            DragableItems.RemoveAt(ind);
        }
    }

}
