using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImgHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemsHolder;
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null){
            if(transform.childCount == 0){
                eventData.pointerDrag.GetComponent<DragableImg>().parentAfterDrag = transform;
                eventData.pointerDrag.GetComponent<Image>().color = Color.clear;
            }
            else{
                transform.DetachChildren();
                eventData.pointerDrag.GetComponent<DragableImg>().parentAfterDrag = transform;
                eventData.pointerDrag.GetComponent<Image>().color = Color.clear;

            }
        }
    }
}
