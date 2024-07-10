using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	[SerializeField] private Text SlideNumber;
	[SerializeField] private GameObject[] LessonImg;

	[SerializeField] private Text dialogueTitle;
	[SerializeField] private Text dialogueText;

	[SerializeField] private GameObject DialogueBox;

	[SerializeField] private GameObject NextBtn;
	[SerializeField] private GameObject PrevBtn;
	[SerializeField] private GameObject DoneBtn;

	private List<string> sentences;
	private List<string> topics;

	private int currentSentenceIndex = 100;

	private bool idle = false;

	int SlideIndex = 0;

	private void Update() {
		SlideNumber.text = SlideIndex + "/" + LessonImg.Length;

		if(SlideIndex == LessonImg.Length){
			PrevBtn.SetActive(true);
			DoneBtn.SetActive(true);
			NextBtn.SetActive(false);
		}else{
			PrevBtn.SetActive(true);
			DoneBtn.SetActive(false);
			NextBtn.SetActive(true);
		}
		
	}

	// Use this for initialization
	void Start () {
		sentences = new List<string>();
		topics = new List<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		LeanTween.scale(DialogueBox, Vector3.one, .25f);
		
		currentSentenceIndex = 100;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Add(sentence);
		}

		topics.Clear();
		foreach (string topic in dialogue.Topics)
		{
			topics.Add(topic);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		
		//will check if dialogue is open for the first time, then reset index to zero
		//else mean it starts in 0 and going up by 1
		if(currentSentenceIndex != 100){
			currentSentenceIndex++;
		}else{
			currentSentenceIndex = 0;
		}
		
		string sentence = sentences[currentSentenceIndex];
		string topicholder = topics[currentSentenceIndex];
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence, topicholder));
		EnableObject(currentSentenceIndex);
	}

	public void DisplayPrevSentence ()
	{
		if (currentSentenceIndex == 0) return;

		currentSentenceIndex--;

		string sentence = sentences[currentSentenceIndex];
		string topicholder = topics[currentSentenceIndex];
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence, topicholder));
		EnableObject(currentSentenceIndex);
	}

	IEnumerator TypeSentence (string sentence,string Topic)
	{	
		dialogueTitle.text = "";
		if(Topic != null){
			dialogueTitle.text = Topic;
		}
		
		yield return new WaitForSeconds(.25f);



		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	public void EndDialogue()
	{	
		ResetObjects();
		LeanTween.scale(DialogueBox, Vector3.zero, .5f);
	}

	private void EnableObject(int index){
		for(int i = 0; i < LessonImg.Length; i++) {
			LessonImg[i].SetActive(false);
			
		}

		if(index >= LessonImg.Length) return;

		LessonImg[index].SetActive(true);
		SlideIndex = index + 1;
	}

	private void ResetObjects() {
		for(int i = 0; i < LessonImg.Length; i++) {
			LessonImg[i].SetActive(false);
		}
	}


	
}
