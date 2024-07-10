using UnityEngine;
using UnityEngine.UI;

public class Scorer : MonoBehaviour
{
    [Header ("Progress References")]
    [SerializeField] private Slider FillSlider;

    [Header ("Scores")]
    [SerializeField] public float maxScore;
    [SerializeField] private int TokenPoints;
    [SerializeField] private int EnemyPoints;
    
    private void Awake() {
        PlayerPrefs.SetInt("Score", 0);
    }
    // Update is called once per frame
    void Update()
    {   
        // percent = (current / max) * 100
        FillSlider.value = (getScore() / maxScore) * 100;
    }

    public void ActivityBonus(int points) {
        addScore(points);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Token"){
            addScore(TokenPoints);
        }
    }


    public int getScore(){
        return PlayerPrefs.GetInt("Score");
    }

    public void addScore(int point){
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + point);
    }

    public float getScorePercent(){
        return FillSlider.value;
    }
}
