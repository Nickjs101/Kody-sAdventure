using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemsHolder;
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null){
            if(transform.childCount == 0){
                eventData.pointerDrag.GetComponent<DragObject>().parentAfterDrag = transform;
            }
            else{
                transform.DetachChildren();
                eventData.pointerDrag.GetComponent<DragObject>().parentAfterDrag = transform;
            }
        }
    }
}
