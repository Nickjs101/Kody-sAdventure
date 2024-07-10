using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogueManager : MonoBehaviour {
	[SerializeField] private GameObject BossQuiz;
	[SerializeField] private Text dialogueText;

	[SerializeField] private GameObject DialogueBox;

	private List<string> sentences;

	private int currentSentenceIndex = 100;

	// Use this for initialization
	void Start () {
		sentences = new List<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		LeanTween.moveY(DialogueBox.GetComponent<RectTransform>(), 173f, .5f);

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Add(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count - 1 == currentSentenceIndex)
		{
			EndDialogue();
			BossQuiz.SetActive(true);//open quiz
			return;
		}
		if(currentSentenceIndex != 100){
			currentSentenceIndex++;
		}else{

			currentSentenceIndex = 0;
		}

		string sentence = sentences[currentSentenceIndex];
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	public void DisplayPrevSentence ()
	{
		if (currentSentenceIndex == 0) return;

		currentSentenceIndex--;

		string sentence = sentences[currentSentenceIndex];
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		LeanTween.moveY(DialogueBox.GetComponent<RectTransform>(), -209f, .5f);
	}

}
