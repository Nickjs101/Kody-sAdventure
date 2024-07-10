using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [SerializeField] GameObject triggerButton;
    [Header ("Trigger Parameters")]
    [SerializeField] private CapsuleCollider2D CapsCollider;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private LayerMask triggerLayer;
    
    private Transform triggerObject;

    void Update()
    {
        triggerButton.SetActive(ObjectInSight());

        if(Input.GetKeyDown(KeyCode.E) && ObjectInSight()){
            TriggerObj();
        }
    }

    public void TriggerObj() {
        if(triggerObject.tag == "Lever"){
            triggerObject.GetComponent<Lever>().Open();
        }
        if(triggerObject.tag == "Door"){
            triggerObject.GetComponent<DoorUnlock>().Open();
        }
        if(triggerObject.tag == "Monitor"){
            triggerObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        if(triggerObject.tag == "ActivityBox"){
            triggerObject.GetComponent<QuestBox>().OpenActivity();
        }
        if(triggerObject.tag == "Chest"){
            triggerObject.GetComponent<ChestBox>().OpenChest();
        }
    }

    private bool ObjectInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(CapsCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                            new Vector3(CapsCollider.bounds.size.x * range, CapsCollider.bounds.size.y, CapsCollider.bounds.size.z), 
                            0, Vector2.left, 0, triggerLayer);

        if(hit.collider != null)
            triggerObject = hit.transform;

        return hit.collider != null;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CapsCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
                                new Vector3(CapsCollider.bounds.size.x * range, CapsCollider.bounds.size.y, CapsCollider.bounds.size.z));
    }
}