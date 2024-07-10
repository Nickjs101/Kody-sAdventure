using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//this script will change Scene of the game
public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private float speed;
    [SerializeField] private float delay;
    float progressValue;
    
    public void Awake()
    {
        Debug.Log(Slider.value);
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        
        yield return new WaitForSeconds(delay);

        progressValue = 0;

        while(Slider.value < 1f)
        {
            progressValue = Mathf.MoveTowards(progressValue, 1f, Time.deltaTime * speed);
            Slider.value += progressValue;
            Debug.Log(Slider.value);

            yield return null;
        }

        SceneManager.LoadScene(scene);
    }


}
