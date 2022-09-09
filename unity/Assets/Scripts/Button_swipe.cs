using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 


public class Button_swipe : MonoBehaviour
{

    private int elements;
    private int actual;

    void Start(){
        this.elements = 3;
        this.actual = 0;
    }

    public void forward(){
        this.actual = (this.actual +1) % this.elements;
    }


    public void backward(){
        this.actual = (this.actual - 1);
        if (this.actual < 0){
            this.actual = this.elements -1;
        }
    }



}
