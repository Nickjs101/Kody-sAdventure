using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ML3 : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] public string title;
	[SerializeField] private float delay;

    private void OnEnable() {
        StartCoroutine(TypeSentence(title));
    }

    IEnumerator TypeSentence (string sentence)
	{
        yield return new WaitForSeconds(delay);

        titleText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			titleText.text += letter;
			yield return new WaitForSeconds(.05f);
		}

	}

	private void OnDisable() {
		titleText.text = ".append()";
	}
}
