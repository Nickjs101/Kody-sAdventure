using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{   
    [SerializeField] private MapPlayPrefs MapDM;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private AudioClip gameOVerSound;
    [SerializeField] private AudioClip PauseSound;
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private AudioMixer audioMixer;

    private LoadingManager loading;

    private void Awake(){
        
        Time.timeScale = 1; // error accur everytime i go to the map that stop the time

        //on start of the game disable the game over screen if not disabled
        gameOverScreen.transform.localScale = Vector3.zero;
        pauseScreen.transform.localScale = Vector3.zero;
        levelCompleteScreen.SetActive(false);

        loading = GetComponent<LoadingManager>();
    }

    public void GamePause() {
        LeanTween.scale(pauseScreen, Vector3.one, .12f);
        SoundManager.instance.PlaySound(PauseSound);
        StartCoroutine(onPause());
    }
    

    public void Play() {
        Time.timeScale = 1;
        LeanTween.scale(pauseScreen, Vector3.zero, .12f);
    }

    private IEnumerator onPause()
    {
        yield return new WaitForSeconds(.12f);
        Time.timeScale = 0;
    }


    public void GameOver() {
        LeanTween.scale(gameOverScreen, Vector3.one, .12f);
        SoundManager.instance.PlaySound(gameOVerSound);
        audioMixer.SetFloat("BG", -80f);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioMixer.SetFloat("BG", 0f);
    }

    public void MainMenu() {
        SceneManager.LoadScene("Map");
        audioMixer.SetFloat("BG", 0f);

    }

    public void NextLevel() {
        if(SceneManager.GetActiveScene().name.Equals("12")){
            SceneManager.LoadScene("Ending");
            return;
        }

        int scene = SceneManager.GetActiveScene().buildIndex + 1;
        string Scene = scene.ToString();
        loading.LoadScene(Scene);
        MapDM.UnlockNewLevel(scene);
        audioMixer.SetFloat("BG", 0f);
    }

    public void Quit() {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }


    public void LevelComplete() {
        levelCompleteScreen.SetActive(true);
        audioMixer.SetFloat("BG", -80f);
    }
}
