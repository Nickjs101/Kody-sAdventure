using UnityEngine;
using UnityEngine.EventSystems;

public class OutDragMonitor : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemsHolder;
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null ){
            eventData.pointerDrag.GetComponent<DragableImg>().parentAfterDrag = itemsHolder.transform;
        }
    }
}
