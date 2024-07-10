using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A6Tweens : MonoBehaviour
{
    [SerializeField] private GameObject example;
    [SerializeField] private GameObject identifier;
    [SerializeField] private GameObject arrow1;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private float delay;

    [SerializeField] private Text explainText;
    [SerializeField] private string message;
    void Awake()
	{
        example.GetComponent<RectTransform>().localScale = Vector3.zero;
        LeanTween.alpha(identifier.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(arrow1.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(arrow2.GetComponent<RectTransform>(), 0f, 0f);
		explainText.text = "";
	}
    private void OnEnable() {

        LeanTween.scale(example, Vector3.one, 1f).setDelay(delay).setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(identifier.GetComponent<RectTransform>(), 1f, 1f).setDelay(delay + 1f);
        LeanTween.alpha(arrow1.GetComponent<RectTransform>(), 1f, 1f).setDelay(delay + 2f);
        LeanTween.alpha(arrow2.GetComponent<RectTransform>(), 1f, 1f).setDelay(delay + 3f);

        StartCoroutine(TypeSentence(message, delay + 4f));
    }

    IEnumerator TypeSentence (string sentence, float messagedelay)
	{
        yield return new WaitForSeconds(messagedelay);
		
		foreach (char letter in sentence.ToCharArray())
		{
			explainText.text += letter;
			yield return new WaitForSeconds(.05f);
		}
	}

	private void OnDisable() {
        LeanTween.reset();
        example.GetComponent<RectTransform>().localScale = Vector3.zero;
        LeanTween.alpha(identifier.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(arrow1.GetComponent<RectTransform>(), 0f, 0f);
        LeanTween.alpha(arrow2.GetComponent<RectTransform>(), 0f, 0f);
		explainText.text = "";
	}
}
