using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    [SerializeField] private Button Attack;

    public bool isAttackEnabled;

    void Update()
    {
        if(isAttackEnabled){
            Attack.interactable = true;
        }else{
            Attack.interactable = false;
        }
    }

    public void EnableAttack() {
        isAttackEnabled = true;
    }

    public void DisableAttack() {
        isAttackEnabled = false;
    }


}
