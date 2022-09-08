using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public enum Scenes
{
    Custom_target_runtime,
    Stable_diffusion_game,
}

public class Button_change_scene : MonoBehaviour
{
    public Scenes scene_to_go;

    // Start is called before the first frame update
    void Start(){

        this.GetComponent<Button>().onClick.AddListener(delegate { change_scene(); });;
    }

    // Update is called once per frame
    void change_scene()
    {   
        //Debug.Log("DEBUG" + this.scene_to_go);
        SceneManager.LoadScene(this.scene_to_go.ToString(), LoadSceneMode.Single);
    }
}
