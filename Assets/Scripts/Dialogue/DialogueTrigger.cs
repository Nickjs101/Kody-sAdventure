using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	[SerializeField] private Dialogue dialogue;

    [SerializeField] private DialogueManager dialogueManager;

	[SerializeField] private BossDialogueManager bossDialogueManager;

	public void TriggerDialogue ()
	{
		if(dialogueManager != null){
			dialogueManager.StartDialogue(dialogue);
		}
		if(bossDialogueManager != null){
			bossDialogueManager.StartDialogue(dialogue);
		}
		
	}

}
