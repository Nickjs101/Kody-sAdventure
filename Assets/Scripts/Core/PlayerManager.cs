using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    //[SerializeField] private UnityEngine.U2D.Animation.SpriteLibrary Character;
    //[SerializeField] private UnityEngine.U2D.Animation.SpriteLibraryAsset[] Characters;


    private void Awake() {

        
        int SelectedCharacater = PlayerPrefs.GetInt("SelectedCharacter", 0);

        //Character.spriteLibraryAsset = Characters[SelectedCharacater];

        LeanTween.reset();

        LeanTween.init(800);

    }
    
}
