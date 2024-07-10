using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//this script will change Scene of the game
public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private Slider Slider;
    [SerializeField] private float speed;

    float progressValue;


    private void Awake() {
        LoadingScreen.SetActive(false);
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        progressValue = 0;

        LoadingScreen.SetActive(true);

        while(Slider.value < 1f)
        {
            progressValue = Mathf.MoveTowards(progressValue, 1f, Time.deltaTime * speed);
            Slider.value += progressValue;

            yield return null;
        }

        SceneManager.LoadScene(scene);
    }



}
