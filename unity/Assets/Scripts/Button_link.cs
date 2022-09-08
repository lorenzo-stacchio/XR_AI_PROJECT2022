using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 


public class Button_link : MonoBehaviour
{
    public string url;

    void Start(){

        this.GetComponent<Button>().onClick.AddListener(delegate { open_url(); });;
    }


    // Update is called once per frame
    void open_url()
    {   
        //Debug.Log("DEBUG" + this.scene_to_go);
        Application.OpenURL(this.url);
    }
}
