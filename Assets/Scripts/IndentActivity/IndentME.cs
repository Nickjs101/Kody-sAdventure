using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndentME : MonoBehaviour
{
    [SerializeField] private GameObject Key;
    [SerializeField] private Transform Player;
    [SerializeField] private int damagePerWrong;
    [SerializeField] private int scorePerCorrect;
    [SerializeField] private GameObject[] Tasks;
    [SerializeField] private AudioClip CheckSound;

    [SerializeField] private Sprite CorrectImg;
    [SerializeField] private Sprite WrongImg;

    [SerializeField] private GameObject completeMessage;

    private Image[] codeBG;
    private Slider[] sliders;
    private float[] answers;
    private GameObject[] checks;

    private Slider selectedSlider = null;

    private Color selectedColor;

    int correct = 0; 


    private void Awake() {
        LeanTween.reset();
        
        GameObject CurrentTask = Tasks[Random.Range(0, Tasks.Length)];
        Task task = CurrentTask.GetComponent<Task>();

        codeBG = task.codeBG;
        sliders = task.sliders;
        answers = task.answers;
        checks = task.checks;

        selectedColor = codeBG[0].color;

        reset();

        CurrentTask.SetActive(true);

    }

    public void Submit() {
        SoundManager.instance.PlaySound(CheckSound);

        for(int i = 0; i < sliders.Length; i++) {
            if(sliders[i].value == answers[i]){
                //correct
                checks[i].GetComponent<Image>().sprite = CorrectImg;
                checks[i].SetActive(true);


                correct += 1;
            }else{
                //wrong
                checks[i].GetComponent<Image>().sprite = WrongImg;
                checks[i].SetActive(true);

            }
        }

        //check if perfect
        if(correct == sliders.Length){
            //close
            //Give Key
            LeanTween.scale(completeMessage, Vector3.one, 1f).setDelay(1f).setEase(LeanTweenType.easeOutBack);
            LeanTween.scale(gameObject, Vector3.zero, .25f).setDelay(2f);
            Player.GetComponent<Scorer>().addScore(scorePerCorrect);
            Key.SetActive(true);
        }else{
            //reset try again
            StartCoroutine(resetAfterSeconds());
            Player.GetComponent<Health>().TakeDamage(damagePerWrong);
        }
        
    }

    public void Tab() {
        if(selectedSlider == null) return;

        if(selectedSlider.value == 10f) return;

        selectedSlider.value += 2f;
    }

    public void BackSpace() {

        if(selectedSlider == null) return;

        if(selectedSlider.value == 0f) return;

        selectedSlider.value -= 2f;
    }

    public void SelectCode(int index) {
        if(selectedSlider == null){
            resetColors();

            codeBG[index].color = selectedColor;

            selectedSlider = sliders[index];
        }else{
            if(selectedSlider != sliders[index]){
                resetColors();

                selectedSlider = sliders[index];

                codeBG[index].color = selectedColor;

            }else{
                resetColors();
                selectedSlider = null;
            }
            
        }
    }


    private void resetColors() {
        for(int i = 0; i < codeBG.Length; i++) {
            codeBG[i].color = Color.clear;
        }
    }


    IEnumerator resetAfterSeconds(){
        yield return new WaitForSeconds(1f);

        reset();
    }

    private void reset() {
        for(int i = 0; i < checks.Length; i++) {
            checks[i].SetActive(false);
        }

        correct = 0; 

        selectedSlider = null;

        resetColors();

        for(int i = 0; i < sliders.Length; i++) {
            sliders[i].value = 0f;
        }
    }
    
}
