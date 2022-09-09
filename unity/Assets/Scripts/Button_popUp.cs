using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 


public class Button_popUp : MonoBehaviour
{

    GameObject metadata_desc; 

    void Start(){
        this.metadata_desc = this.gameObject.transform.Find("Metadata").gameObject;
    }

    // Update is called once per frame
    public void change_active()
    {   
        this.metadata_desc.SetActive(!this.metadata_desc.active);
    }
}
