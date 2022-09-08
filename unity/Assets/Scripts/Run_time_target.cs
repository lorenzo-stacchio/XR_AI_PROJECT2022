using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Run_time_target : MonoBehaviour
{
    public Texture2D textureFile;
    public float printedTargetSize;
    public string targetName;
    public GameObject to_spawn;
    public GameObject input_Text;

    private Dictionary<string, GameObject> dict_of_targets;

    void Start()
    {
        // Use Vuforia Application to invoke the function after Vuforia Engine is initialized
        this.dict_of_targets = new Dictionary<string, GameObject>();
        VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideloadedTexture;
        
    }

    void CreateImageTargetFromSideloadedTexture()
    {
        var mTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(
            textureFile,
            printedTargetSize,
            targetName);
        // add the Default Observer Event Handler to the newly created game object
        mTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        Debug.Log("Instant Image Target created " + mTarget.TargetName);
        GameObject target = GameObject.Find(targetName);// target gameobject
        // Spawn cube
        GameObject cube = Instantiate(to_spawn, transform.position, Quaternion.identity);
        cube.name = "cube_" + targetName;
        cube.transform.parent = target.transform;
        cube.transform.position = new Vector3(0, 0, 0);
        cube.transform.transform.localScale = new Vector3(0.1f, 0.0006f, 0.07f); // old --> Vector3(0.199000001, 0.000999999931, 0.129999995)
        // rotate cube to manage flip image on server
        cube.transform.eulerAngles = new Vector3(
            cube.transform.eulerAngles.x,
            cube.transform.eulerAngles.y + 180,
            cube.transform.eulerAngles.z
        );

        // Spawn input field
        GameObject inputField = Instantiate(input_Text, transform.position, Quaternion.identity);
        inputField.name = "inputField" + targetName;
        inputField.transform.parent = target.transform;
        inputField.transform.position = new Vector3(0, 0, 0);
        inputField.transform.transform.localScale = new Vector3(0.1f, 0.0006f, 0.07f); // old --> Vector3(0.199000001, 0.000999999931, 0.129999995)
        // rotate cube to manage flip image on server
        inputField.transform.eulerAngles = new Vector3(
            inputField.transform.eulerAngles.x,
            inputField.transform.eulerAngles.y + 180,
            inputField.transform.eulerAngles.z
        );



        // Add to object the component Stable_diffusion
        Stable_diffusion sd = target.AddComponent<Stable_diffusion>();

        // Add event
        DefaultObserverEventHandler target_handler = target.GetComponent<DefaultObserverEventHandler>();
        target_handler.OnTargetFound = new UnityEngine.Events.UnityEvent(); // Unity event is empty by default, init by yourself
        //Debug.Log("Target handler" + target_handler);
        //Debug.Log("ALL COMPONENTS --> " + target.GetComponents(typeof(MonoBehaviour)));
        //Debug.Log("Target found" + target_handler.OnTargetFound);
        //target_handler.OnTargetFound.AddListener(sd.generate);
        target_handler.OnTargetFound.AddListener(Ping);

        // Add Input Field


        this.dict_of_targets.Add(targetName, target);
    }

    private void Update()
    {
        foreach (KeyValuePair<string, GameObject> p in this.dict_of_targets)
        {
            Debug.Log(p.Key.ToString());
            DefaultObserverEventHandler target_handler = p.Value.GetComponent<DefaultObserverEventHandler>();
            // loop through both
            // Debug.Log("Update target handler --> " + target_handler.OnTargetFound);
            // Debug.Log("Update target handler2 --> " + (target_handler.GetType().GetProperty("OnTargetFound")!= null));
            // Debug.Log("Update ALL COMPONENTS --> " + p.Value.GetComponents(typeof(MonoBehaviour)));
        }

        //Debug.Log("Target found" + target_handler.OnTargetFound);

    }

    public void Ping()
    {
        //Debug.Log("PONG");
    }
}
