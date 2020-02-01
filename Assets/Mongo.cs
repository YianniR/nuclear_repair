using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Mongo : MonoBehaviour
{
    public string Url = "http://lloyd-pearson.co.uk/";

    public void Start()
    {
        StartCoroutine(GetRequest("data/1", printData));

        StartCoroutine(PostRequest("data", "{\"id\":3, \"data\":\"cow\"}", printData));

        StartCoroutine(GetRequest("data/3", printData));
    }

    private void printData(string data)
    {
        Debug.Log("PRINTING: " + data);
    }

    public IEnumerator GetRequest(string slug, Action<string> callbackFunc)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Url+slug))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(Url + slug + ": Get Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(Url + slug + ":\nGet Received: \n" + webRequest.downloadHandler.text);
                callbackFunc(webRequest.downloadHandler.text);
                yield return webRequest.downloadHandler.text;
            }
        }
    }

    public IEnumerator PostRequest(string slug, string data, Action<string> callbackFunc)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(Url+slug, bytes))
        {

            webRequest.SetRequestHeader("X-HTTP-Method-Override", "POST");
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(Url + slug + data + ": Post Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(Url + slug + data + ":\n Post Received: \n" + webRequest.downloadHandler.text);
                callbackFunc(webRequest.downloadHandler.text);
                yield return webRequest.downloadHandler.text;
            }
        }
    }
}