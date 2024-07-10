using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //[SerializeField] private Image totalHealtbar;
    [SerializeField] private Slider currentHealthbar;
    [SerializeField] private Image fill;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Gradient colors;

    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(delay());
    }

    // Update is called once per frame
    private void Update()
    {

        currentHealthbar.value = playerHealth.currentHealth;


        if(playerHealth.gameObject.tag == "Player"){
            fill.color = colors.Evaluate(currentHealthbar.normalizedValue);
        }
        
        
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(1);
        currentHealthbar.maxValue = playerHealth.currentHealth;
        currentHealthbar.value = playerHealth.currentHealth;
        if(playerHealth.gameObject.tag == "Player"){
            fill.color = colors.Evaluate(1f);
        }
    }
}
