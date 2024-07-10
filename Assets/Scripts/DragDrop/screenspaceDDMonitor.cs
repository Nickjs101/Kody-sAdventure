using UnityEngine;
using UnityEngine.EventSystems;

public class screenspaceDDMonitor : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemsHolder;
    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag != null ){
            eventData.pointerDrag.GetComponent<DragObject>().parentAfterDrag = itemsHolder.transform;
        }
    }
}
