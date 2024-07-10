using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip Sound;
    
    public void PlaySound() {
        SoundManager.instance.PlaySound(Sound);
    }
}
