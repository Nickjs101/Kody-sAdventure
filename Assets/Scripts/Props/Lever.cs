using UnityEngine;

public class Lever : MonoBehaviour
{
    private enum ObjectToOpen{
        UpDownDoor,
        ObjectScript
    }
    [SerializeField] private bool useKey;
    [SerializeField] private ObjectToOpen objectToOpen;
    [SerializeField] private GameObject upDownDoor;
    [SerializeField] private Behaviour ScriptToActivate;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private GameObject message;

    public void Open() {
        if(!useKey){
            if(objectToOpen == ObjectToOpen.UpDownDoor){
                GetComponent<SpriteRenderer>().sprite = onSprite;
                upDownDoor.GetComponent<UDDoorUnlock>().Unlock();
            }
            if(objectToOpen == ObjectToOpen.ObjectScript){
                ScriptToActivate.enabled = true;
                GetComponent<SpriteRenderer>().sprite = onSprite;
            } 
        }else{
            if(PlayerPrefs.GetInt("Keys") >= 1){
                PlayerPrefs.SetInt("Keys", PlayerPrefs.GetInt("Keys") - 1);
                if(objectToOpen == ObjectToOpen.UpDownDoor){
                    GetComponent<SpriteRenderer>().sprite = onSprite;
                    upDownDoor.GetComponent<UDDoorUnlock>().Unlock();
                }
                if(objectToOpen == ObjectToOpen.ObjectScript){
                    ScriptToActivate.enabled = true;
                    GetComponent<SpriteRenderer>().sprite = onSprite;
                } 
            }else{
                LeanTween.scale(message, new Vector3(.2f, .2f, .2f), .12f);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        LeanTween.scale(message, Vector3.zero, .12f);
    }
}
