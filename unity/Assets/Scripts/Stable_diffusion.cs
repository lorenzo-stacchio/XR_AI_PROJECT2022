using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

public class Stable_diffusion : MonoBehaviour
{
    // Start is called before the first frame update
    private bool called;
    private string Token;
    private TouchScreenKeyboard keyboard;
    private string text_generation_input;
    private int TimerNextGeneration;

    private void Start()
    {
        this.called = false;
        this.Token = "a2663f6b0ca76bf3bad79743eda1fc5b2eb6251d";
        this.TimerNextGeneration = 5;
        //this.generate();
        //StartCoroutine("generate_async");
    }

    // Update is called once per frame
    public void generate()
    {
        this.text_generation_input = "A ripped fresco of the Madonna and Child and saints from the neck down.";
        Debug.Log("GENERATEEEE --> " +  this.text_generation_input);
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        SceneManager.LoadScene("Stable_diffusion_game", LoadSceneMode.Single);
        // if (!this.called)
        // {
        //     StartCoroutine(this.generate_async(this.text_generation_input));
        //     this.called = true;
        // }
    }


    IEnumerator generate_async(string text_generation_input = "Dog playing in a grass field.")
    {
        //MAKE THE POST CALL TO GENERATE USING REPLICATE APIS

        string post_url = "https://api.replicate.com/v1/predictions";
        var diffusion_post_uwr = new UnityWebRequest(post_url, "POST");

        string jsonRequestDiffusion = "{ \"version\": \"a9758cbfbd5f3c2094457d996681af52552901775aa2d6dd0b17fd15df959bef\", \"input\": { \"prompt\":\"" + this.text_generation_input + "\"}}";
        //Debug.Log(jsonRequestDiffusion);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonRequestDiffusion);
        diffusion_post_uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        diffusion_post_uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        diffusion_post_uwr.SetRequestHeader("Content-type", "application/json");
        diffusion_post_uwr.SetRequestHeader("Authorization", "Token "+this.Token);
        yield return diffusion_post_uwr.SendWebRequest();

        if (diffusion_post_uwr.isNetworkError)
        {
            Debug.Log("Error While Sending Post: " + diffusion_post_uwr.error);
        }
        else
        {
            Debug.Log("Diffusion post text: " + diffusion_post_uwr.downloadHandler.text);
            JObject json_post_result = JObject.Parse(diffusion_post_uwr.downloadHandler.text);
            var string_get_url = JValue.Parse(JsonConvert.SerializeObject(json_post_result["urls"]["get"]));
            Debug.Log(string_get_url);
            // Serialize response json and download the result when it is ready

            var imageUrl = ""; // this variable serves the purpose of waiting for the predictions to be made
            while (imageUrl == "")
            {
                var uwr = new UnityWebRequest(string_get_url.ToString(), "GET");
                uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                uwr.SetRequestHeader("Content-type", "application/json");
                uwr.SetRequestHeader("Authorization", "Token " + this.Token);
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError)
                {
                    Debug.Log("Error While Sending: " + uwr.error);
                }
                else
                {
                    //Debug.Log(uwr.downloadHandler.text);
                    JObject json_get = JObject.Parse(uwr.downloadHandler.text);
                    //Debug.Log("json_get " + json_get);
                    // get download url and do the last request
                    // First serialize the value object in key output and then parse it as a value for c# language data structure parsing
                    var list_url_unique = JValue.Parse(JsonConvert.SerializeObject(json_get["output"]));
                    //Debug.Log("DEBUG list url" +  (list_url_unique.ToString() == ""));

                    //Debug.Log("type list url" + list_url_unique.GetType());
                    if (list_url_unique.ToString() != "")
                    {
                        imageUrl = list_url_unique[0].ToString(); //get image url
                    }
                    //Debug.Log("unique " + imageUrl);

                }
            }

            UnityWebRequest www_texture = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www_texture.SendWebRequest();
            if (www_texture.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www_texture.error);
            }
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www_texture.downloadHandler).texture;
                Texture2D texture2D = (Texture2D)myTexture;
                GameObject.Find("cube_brush").GetComponent<Renderer>().material.mainTexture = texture2D;
            }
        }

        yield return new WaitForSeconds(TimerNextGeneration);
        //this.called = false; //reset the generation variable
        StartCoroutine(this.generate_async(text_generation_input));


    }
}