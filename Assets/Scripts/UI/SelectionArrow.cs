using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{

    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip EnterSound;
    private RectTransform rect;
    private int currentPosition;

    void Awake() {
        rect = GetComponent<RectTransform>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            ChangePosition(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            ChangePosition(1);
        }

        if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter)){
            Interact();
        }
    }

    private void ChangePosition(int _change) {
        currentPosition += _change;

        if(_change != 0){
            SoundManager.instance.PlaySound(changeSound);
        }

        if(currentPosition < 0){
            currentPosition = options.Length - 1;
        }else if(currentPosition > options.Length - 1) {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact() {
        SoundManager.instance.PlaySound(EnterSound);
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
