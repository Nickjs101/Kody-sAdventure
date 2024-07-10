
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    [SerializeField] private Scorer scorer;
    private Text ScoreText;
    private void Awake() {
        ScoreText = GetComponent<Text>();
    }
    private void Update() {
        ScoreText.text = "Score: " +scorer.getScore().ToString() + " / "+ scorer.maxScore.ToString();
    }
}
