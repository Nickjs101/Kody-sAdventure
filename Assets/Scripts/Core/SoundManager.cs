using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //instance of Soundmanager class
    public static SoundManager instance { get; private set;}

    //audiosource component of the sound manager object
    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {   
        //when the game start the instance will be instantiated with current Soundmanager component
        instance = this;
        //get AudioSource component
        source = GetComponent<AudioSource>();

        //this code will check if instance is not instantiated then it will be instantiated and will not be destroyed
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //this will check will instance is two and the current instance is not the main instance, then destroy the extra instance
        else if(instance != null && instance != this){
            Destroy(gameObject);
        }
        
    }

    //function to play the souce of each event in the game 
    //centralized functio, so that scripts will not create its own function
    public void PlaySound(AudioClip _sound) {
        source.PlayOneShot(_sound);
    }

    public void Stop() {
        source.Stop();
        
    }
}
