using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapSceneManager : MonoBehaviour
{   
    [SerializeField] private Sprite[] stars;
    [SerializeField] private Image[] buttonsImg;
    
    private MapData data;

    private void Awake() {
        
        string starString = "";
        string levelString = "";

        for(int i = 0; i < 12; i++) {
            int lvl = i + 1;
            starString = "Level" + lvl + "Stars";
            
            levelString = "Level" + lvl;
            
            if(PlayerPrefs.GetInt(levelString) == 1){
                buttonsImg[i].sprite = stars[PlayerPrefs.GetInt(starString)]; // 0 1 2 3
            }else{
                buttonsImg[i].sprite = stars[4]; // 4 for inactive
            }
        }
        
    }
    
}
    
    
