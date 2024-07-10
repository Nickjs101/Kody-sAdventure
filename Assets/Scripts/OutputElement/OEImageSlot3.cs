using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OEImageSlot3 : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemsHolder;
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null){
            if(transform.childCount == 0){
                eventData.pointerDrag.GetComponent<DragableImg>().parentAfterDrag = transform;
                eventData.pointerDrag.GetComponent<Image>().color = Color.clear;

                transform.parent.parent.parent.parent.parent.GetComponent<CompleteCode>().submit(eventData.pointerDrag.transform.GetChild(0).GetComponent<Text>().text);
            }
            else{
                transform.DetachChildren();
                eventData.pointerDrag.GetComponent<DragableImg>().parentAfterDrag = transform;
                eventData.pointerDrag.GetComponent<Image>().color = Color.clear;

            }
        }
    }
}
