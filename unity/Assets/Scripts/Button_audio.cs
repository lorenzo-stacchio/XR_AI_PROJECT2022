using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 


public class Button_audio : MonoBehaviour
{
    AudioSource audioData;
    bool play;

    void Start(){
        this.audioData = GetComponent<AudioSource>();
        this.play = false;
    }


    // Update is called once per frame
    public void stop_play()
    {   

        if (!this.play){
            this.audioData.Play(0); //delay start time
        }
        else {
            this.audioData.Pause();        
        }
        
        this.play = !this.play;


    }
}
