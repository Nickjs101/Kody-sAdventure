using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDataManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        PlayerPrefs.SetInt("SelectedCharacter", 0);
        PlayerPrefs.SetInt("SelectedBullet", 0);
        PlayerPrefs.SetInt("Character1", 1);
        PlayerPrefs.SetInt("Character2", 0);
        PlayerPrefs.SetInt("Character3", 0);
        PlayerPrefs.SetInt("Character4", 0);
        PlayerPrefs.SetInt("Character5", 0);
        PlayerPrefs.SetInt("Bullet1", 1);
        PlayerPrefs.SetInt("Bullet2", 0);
        PlayerPrefs.SetInt("Bullet3", 0);
        PlayerPrefs.SetInt("Bullet4", 0);
        PlayerPrefs.SetInt("Bullet5", 0);
    }

    public void UpdateSelectedCharacter(int CharacterIndex) {
        PlayerPrefs.SetInt("SelectedCharacter", CharacterIndex);
    }

    public void UpdateSelectedBullet(int BulletIndex) {
        PlayerPrefs.SetInt("SelectedBullet", BulletIndex);
    }

    public void UnlockChar(int CharacterIndex) {
        int newIndex = CharacterIndex + 1;
        string Char = "Character" + newIndex.ToString();

        PlayerPrefs.SetInt(Char, 1);
    }

    public void UnlockBullet(int BulletIndex) {
        int newIndex = BulletIndex + 1;
        string Bullet = "Bullet" + newIndex.ToString();

        PlayerPrefs.SetInt(Bullet, 1);
    }

    public ShopData GetData() {
        bool Character1 = PlayerPrefs.GetInt("Character1") == 1  ? true : false; 
        bool Character2 = PlayerPrefs.GetInt("Character2") == 1  ? true : false; 
        bool Character3 = PlayerPrefs.GetInt("Character3") == 1  ? true : false; 
        bool Character4 = PlayerPrefs.GetInt("Character4") == 1  ? true : false; 
        bool Character5 = PlayerPrefs.GetInt("Character5") == 1  ? true : false; 
        bool Bullet1 = PlayerPrefs.GetInt("Bullet1") == 1  ? true : false;
        bool Bullet2 = PlayerPrefs.GetInt("Bullet2") == 1  ? true : false;
        bool Bullet3 = PlayerPrefs.GetInt("Bullet3") == 1  ? true : false;
        bool Bullet4 = PlayerPrefs.GetInt("Bullet4") == 1  ? true : false;
        bool Bullet5 = PlayerPrefs.GetInt("Bullet5") == 1  ? true : false;

        ShopData data = new ShopData(
            PlayerPrefs.GetInt("SelectedCharacter"),
            PlayerPrefs.GetInt("SelectedBullet"),
            Character1,
            Character2,
            Character3,
            Character4,
            Character5,
            Bullet1,
            Bullet2,
            Bullet3,
            Bullet4,
            Bullet5
        );

        return data;
    }

}
