using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoStreamer : MonoBehaviour
{
    [SerializeField] private bool isIntro;
    [SerializeField] private RawImage videoCanvas;
    [SerializeField] private VideoPlayer videoPlayer;

    private bool isLoading = true;
    private void Start() {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo(){
        videoPlayer.Prepare();

        while(!videoPlayer.isPrepared) {
            isLoading = true;
            yield return new WaitForSeconds(1f);
            break;
        }

        videoCanvas.texture = videoPlayer.texture;
        videoPlayer.Play();
        isLoading = false;
    }

    public void SkipVideo() {
        videoPlayer.Stop();
        if(isIntro){
            SceneManager.LoadScene("1");
        }else{
            SceneManager.LoadScene("MainMenu");
        }
        
    }


    private void Update() {
        if(!videoPlayer.isPlaying && !isLoading) {
            if(isIntro){
                SceneManager.LoadScene("1");
            }else{
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
