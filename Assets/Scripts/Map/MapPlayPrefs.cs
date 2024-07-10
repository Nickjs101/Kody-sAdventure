using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayPrefs : MonoBehaviour
{
    private int CurrentPoint;

    private void Awake() {
        CurrentPoint = PlayerPrefs.GetInt("CurrentPoint", 0);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        if(PlayerPrefs.GetInt("isSet", 0) == 0) return;

        PlayerPrefs.SetInt("isSet", 1);
        
        PlayerPrefs.SetInt("CurrentPoint", 0);

        PlayerPrefs.SetInt("Level1", 0);
        PlayerPrefs.SetInt("Level2", 0);
        PlayerPrefs.SetInt("Level3", 0);
        PlayerPrefs.SetInt("Level4", 0);
        PlayerPrefs.SetInt("Level5", 0);
        PlayerPrefs.SetInt("Level6", 0);
        PlayerPrefs.SetInt("Level7", 0);
        PlayerPrefs.SetInt("Level8", 0);
        PlayerPrefs.SetInt("Level9", 0);
        PlayerPrefs.SetInt("Level10", 0);
        PlayerPrefs.SetInt("Level11", 0);
        PlayerPrefs.SetInt("Level12", 0);

        PlayerPrefs.SetInt("Level1Stars", 0);
        PlayerPrefs.SetInt("Level2Stars", 0);
        PlayerPrefs.SetInt("Level3Stars", 0);
        PlayerPrefs.SetInt("Level4Stars", 0);
        PlayerPrefs.SetInt("Level5Stars", 0);
        PlayerPrefs.SetInt("Level6Stars", 0);
        PlayerPrefs.SetInt("Level7Stars", 0);
        PlayerPrefs.SetInt("Level8Stars", 0);
        PlayerPrefs.SetInt("Level9Stars", 0);
        PlayerPrefs.SetInt("Level10Stars", 0);
        PlayerPrefs.SetInt("Level11Stars", 0);
        PlayerPrefs.SetInt("Level12Stars", 0);
    }

    public void UpdateCurrentPoint(int point) {
        PlayerPrefs.SetInt("CurrentPoint", point);
    }

    //call this when the playeropen the level for the first time
    public void UnlockNewLevel(int Level){
        string level = "Level" + Level.ToString();

        PlayerPrefs.SetInt(level, 1);


        string levelPoint = "Level" + Level + "Point";
        int point = PlayerPrefs.GetInt(levelPoint);

        PlayerPrefs.SetInt("CurrentPoint", point);

        CurrentPoint = point;

        string levelStar = level + "Stars";
        PlayerPrefs.SetInt(levelStar, 0);
    }

    //call this when the player finish the level
    public void UpdateStars(int Stars){
        string levelStar = "Level" + CurrentPoint.ToString() + "Stars";
        int newStars = Stars >= 3 ? Stars : 0;
        PlayerPrefs.SetInt(levelStar, newStars);
    }


    public MapData GetMapData() {

        bool Level1 = PlayerPrefs.GetInt("Level1") == 1 ? true : false;
        bool Level2 = PlayerPrefs.GetInt("Level2") == 1 ? true : false;
        bool Level3 = PlayerPrefs.GetInt("Level3") == 1 ? true : false;
        bool Level4 = PlayerPrefs.GetInt("Level4") == 1 ? true : false;
        bool Level5 = PlayerPrefs.GetInt("Level5") == 1 ? true : false;
        bool Level6 = PlayerPrefs.GetInt("Level6") == 1 ? true : false;
        bool Level7 = PlayerPrefs.GetInt("Level7") == 1 ? true : false;
        bool Level8 = PlayerPrefs.GetInt("Level8") == 1 ? true : false;
        bool Level9 = PlayerPrefs.GetInt("Level9") == 1 ? true : false;
        bool Level10 = PlayerPrefs.GetInt("Level10") == 1 ? true : false;
        bool Level11 = PlayerPrefs.GetInt("Level11") == 1 ? true : false;
        bool Level12 = PlayerPrefs.GetInt("Level12") == 1 ? true : false;

        MapData data = new MapData(
            PlayerPrefs.GetInt("CurrentPoint"),
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7,
            Level8,
            Level9,
            Level10,
            Level11,
            Level12,
            PlayerPrefs.GetInt("Level1Stars"),
            PlayerPrefs.GetInt("Level2Stars"),
            PlayerPrefs.GetInt("Level3Stars"),
            PlayerPrefs.GetInt("Level4Stars"),
            PlayerPrefs.GetInt("Level5Stars"),
            PlayerPrefs.GetInt("Level6Stars"),
            PlayerPrefs.GetInt("Level7Stars"),
            PlayerPrefs.GetInt("Level8Stars"),
            PlayerPrefs.GetInt("Level9Stars"),
            PlayerPrefs.GetInt("Level10Stars"),
            PlayerPrefs.GetInt("Level11Stars"),
            PlayerPrefs.GetInt("Level12Stars")
        );
        
        return data;
    }

    public void SetPointForLevels(int a,int b,int c,int d,int e,int f,int g,int h,int i,int j,int k,int l) {
        PlayerPrefs.SetInt("Level1Point", a);
        PlayerPrefs.SetInt("Level2Point", b);
        PlayerPrefs.SetInt("Level3Point", c);
        PlayerPrefs.SetInt("Level4Point", d);
        PlayerPrefs.SetInt("Level5Point", e);
        PlayerPrefs.SetInt("Level6Point", f);
        PlayerPrefs.SetInt("Level7Point", g);
        PlayerPrefs.SetInt("Level8Point", h);
        PlayerPrefs.SetInt("Level9Point", i);
        PlayerPrefs.SetInt("Level10Point", j);
        PlayerPrefs.SetInt("Level11Point", k);
        PlayerPrefs.SetInt("Level12Point", l);
    }


    //get current level
    //unlock levels + stars
    //if level is unlock shows stars 
    //

}
