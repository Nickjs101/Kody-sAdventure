using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Text txt;
    [HideInInspector] public Transform parentAfterDrag;

    private Transform itemsHolder;

    private void Awake() {
        txt = GetComponent<Text>();
        itemsHolder = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.gameObject.transform.parent);
        transform.SetAsLastSibling();
        txt.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        txt.raycastTarget = true;
    }

    private void Update() {
        if(transform.parent == null){
            transform.SetParent(itemsHolder);
            parentAfterDrag = itemsHolder;
        }
    }

    
}
