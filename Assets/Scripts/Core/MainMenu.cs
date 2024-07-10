using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void ShowMap() {
        SceneManager.LoadScene("Map");
    }

    public void Shop() {
        SceneManager.LoadScene("Shop");
    }

    public void Lessons() {
        SceneManager.LoadScene("Lessons");
    }
}
