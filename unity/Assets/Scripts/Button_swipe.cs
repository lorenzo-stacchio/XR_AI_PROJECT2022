using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 


public class Button_swipe : MonoBehaviour
{

    private int elements;
    private int actual;
    public GameObject manager_owner; 
    private Model_manager manager;

    void Start(){
        this.actual = 0;
        Debug.Log("Manager" + this.manager_owner);
    }

    public void forward(){
        Debug.Log("Manager" + this.manager_owner);

        this.manager = this.manager_owner.GetComponent<Model_manager>();
        this.elements = this.manager.getnumchild();

        this.actual = (this.actual +1) % this.elements;
        this.manager.Set_active_model(this.actual);
        //forward to Model Manager
    }


    public void backward(){
        Debug.Log("Manager" + this.manager_owner);
        this.manager = this.manager_owner.GetComponent<Model_manager>();
        this.elements = this.manager.getnumchild();

        this.actual = (this.actual - 1);
        if (this.actual < 0){
            this.actual = this.elements -1;
        }
        this.manager.Set_active_model(this.actual);
    }



}
