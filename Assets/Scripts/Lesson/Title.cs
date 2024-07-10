using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{	
	[SerializeField] private bool isLevelName;
    [SerializeField] private Text titleText;
	[TextArea(3, 10)]
    [SerializeField] public string title;
	[SerializeField] private float delay;
    [SerializeField] private AudioClip TypingScound;

	void Awake()
	{
		titleText.text = "";

	}
    private void OnEnable() {
        StartCoroutine(TypeSentence(title));
    }

    IEnumerator TypeSentence (string sentence)
	{
        yield return new WaitForSeconds(delay);
		if(TypingScound != null){
			SoundManager.instance.PlaySound(TypingScound);
		}
        
		foreach (char letter in sentence.ToCharArray())
		{
			titleText.text += letter;
			yield return new WaitForSeconds(.05f);
		}
        SoundManager.instance.Stop();

		if(isLevelName){
			yield return new WaitForSeconds(1f);
			gameObject.SetActive(false);
		}

	}

	private void OnDisable() {
		titleText.text = "";
		SoundManager.instance.Stop();
	}
}
