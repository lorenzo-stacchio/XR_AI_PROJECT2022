using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Model_manager : MonoBehaviour
{

    List<GameObject> model_names;

    int active_index;

    // Start is called before the first frame update
    void Awake()
    {
        this.model_names = new List<GameObject>();
        //take all gameobject in child
        foreach (Transform child in transform)
        {
            Debug.Log("child" + child );
            this.model_names.Add(child.gameObject);
        }
        Debug.Log("num of 3d models" + this.model_names.Count);

        // activate first
        this.active_index = 0;
        this.model_names[active_index].SetActive(true);

    }

   

    public void Set_active_model(int index){
        this.model_names[this.active_index].SetActive(false);

        //set active
        this.active_index = index;
        this.model_names[this.active_index].SetActive(true);
    }

    public int getnumchild(){
        return  this.model_names.Count;
    }

}
