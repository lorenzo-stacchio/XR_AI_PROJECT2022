using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider_rotation : MonoBehaviour
{
    public GameObject manager_owner; 
    private Model_manager manager;
    private Slider slider; 

    private float z_offset;
    // Start is called before the first frame update
    void Start()
    {
        this.manager = manager_owner.GetComponent<Model_manager>();
        this.slider = this.GetComponent<Slider>();
        this.z_offset = this.manager.get_active_model().transform.localEulerAngles.z;
        Debug.Log("z_offset" + this.z_offset);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("z_offset update" + this.z_offset);

        GameObject to_rotate = this.manager.get_active_model();
        //rotate on y axis
        float offset = 360 * this.slider.value;

        
        Debug.Log("OFFSET" + offset);

        to_rotate.transform.eulerAngles = new Vector3(
            to_rotate.transform.eulerAngles.x,
            to_rotate.transform.eulerAngles.y,
            this.z_offset + offset
        );


    }
}
