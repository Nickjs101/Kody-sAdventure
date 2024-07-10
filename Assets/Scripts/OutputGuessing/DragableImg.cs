using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragableImg : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Image Image;
    private Text txt;
    private Color myColor;
    [HideInInspector] public Transform parentAfterDrag;

    private Transform itemsHolder;

    private void Awake() {
        Image = GetComponent<Image>();
        myColor = Image.color;
        txt = transform.GetChild(0).gameObject.GetComponent<Text>();
        itemsHolder = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
        txt.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parentAfterDrag);
        Image.raycastTarget = true;
        txt.raycastTarget = true;
    }

    private void Update() {
        if(transform.parent == null){
            transform.SetParent(itemsHolder);
            parentAfterDrag = itemsHolder;
            
        }

        if(transform.parent == itemsHolder){
            Image.color = myColor;
        }
    }

    
}
