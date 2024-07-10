using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private Scorer scorer;
    [SerializeField] private GameObject[] Stars;
    [SerializeField] private Text ScoreText;
    [SerializeField] private AudioClip LevelCompleteSound;
    
    
    void Awake() {
        for(int i = 0; i < Stars.Length; i++) {
            Stars[i].transform.localScale = Vector3.zero;
        }
    }
    
    void Start() {

        int Stars = 0;

        float scorePercentage = scorer.getScorePercent();
        int score = scorer.getScore();

        if(scorePercentage < 30){
            Stars = 0;
        }else if(scorePercentage >= 30 && scorePercentage < 60){
            Stars = 1;
        }else if(scorePercentage >= 60 && scorePercentage < 90 ){
            Stars = 2;
        }else if(scorePercentage >= 90){
            Stars = 3;
        }

        string level = SceneManager.GetActiveScene().name;
        Debug.Log("Level Name" + level);

        string setLevelStar = "Level" + level + "Stars";
        PlayerPrefs.SetInt(setLevelStar, Stars);

        string levelName = "Level" + level + "Score"; 
        PlayerPrefs.SetInt(levelName, PlayerPrefs.GetInt(levelName, 0) < score ? score :  PlayerPrefs.GetInt(levelName, 0));

        PlayerPrefs.SetInt("Tokens", PlayerPrefs.GetInt("Tokens") + PlayerPrefs.GetInt("CurrentTokens"));

    //TEST
        // int Stars = 3;
        // int score = 100;
        SoundManager.instance.PlaySound(LevelCompleteSound);

        StartCoroutine(DisplayScoreStar(Stars, score));
    }

    IEnumerator DisplayScoreStar(int Star, int Score){
        int star = 0;
        while(star < Star) {
            LeanTween.scale(Stars[star], Vector3.one, .5f).setEase(LeanTweenType.easeOutBack);
            star++;
            
            yield return new WaitForSeconds(.3f);
        }

        int Counter = 0;
        while(Counter < Score){
            Counter++;
            ScoreText.text = Counter.ToString();
            yield return null;
        }
    }
}
