﻿using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Mongo : MonoBehaviour
{
    public static string Url = "http://lloyd-pearson.co.uk/";

    public void Start()
    {
        // StartCoroutine(GetRequest("widgets", printData));
        //
        // StartCoroutine(CreateOrUpdateWidget("{\"instanceId\":3, \"pcId\":1, \"type\":\"cow\" }", printData));

        // StartCoroutine(GetRequest("data/1", printData));

        // StartCoroutine(PatchRequest("data/5e35ba0b54988257208394c5", "{type:\"moo\"}", printData));
    }

    private void printData(string data)
    {
        Debug.Log("PRINTING: " + data);
    }

    public static string stripEnds(string text)
    {
        var index = text.Length;
        text = text.Remove(0, 1);
        text = text.Remove(index - 2, 1);
        return text;
    }
    ///////////////////////////// GETTERS ////////////////////////////////////////////

    public static IEnumerator GetRequest(string slug, Action<string> callbackFunc)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Url + slug))
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

    public static IEnumerator GetWidgets(Action<JSONObject> callbackFunc, int pcId = -1)
    {
        var genUrl = Url + "widgets";

        if (pcId != -1)
        {
            genUrl += "?where={\"pcId\":" + pcId + "}";
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(genUrl))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(genUrl + ": Get Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(genUrl + ":\nGet Received: \n" + webRequest.downloadHandler.text);
                callbackFunc(new JSONObject(webRequest.downloadHandler.text));
                yield return webRequest.downloadHandler.text;
            }
        }
    }

    public static IEnumerator GetData(Action<JSONObject> callbackFunc, int dataId = -1)
    {
        var genUrl = Url + "data";

        if (dataId != -1)
        {
            genUrl += "?where={\"dataId\":" + dataId + "}";
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(genUrl))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            if (webRequest.isNetworkError)
            {
                Debug.Log(genUrl + ": Get Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(genUrl + ":\nGet Received: \n" + webRequest.downloadHandler.text);
                callbackFunc(new JSONObject(webRequest.downloadHandler.text));
                yield return webRequest.downloadHandler.text;
            }
        }
    }

    /////////////////////////// POST ////////////////////////////////////////////////////////
    public static IEnumerator PostRequest(string slug, string data, Action<string> callbackFunc)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(data);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(Url + slug, bytes))
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

    public static IEnumerator CreateOrUpdateWidget(
        string data
        , Action<string> callbackFunc
    )
    {
        var genUrl = Url + "widgets";
        var where = "";

        var update = false;
        var etag = "";
        var id = "";

        var dataObj = new JSONObject(data);

        if (!dataObj.HasField("pcId") || !dataObj.HasField("instanceId"))
        {
            Debug.LogError("Not enough data you n00b");
        }

        var instanceId = (int) dataObj["instanceId"].n;
        var pcId = (int) dataObj["pcId"].n;

        if (instanceId != -1 && pcId != -1)
        {
            where += "?where={ \"$and\": [{\"instanceId\":" + instanceId + "},{\"pcId\":" + pcId + "}]}";
        }
        else if (instanceId != -1)
        {
            where += "?where={\"instanceId\":" + instanceId + "}";
        }
        else if (pcId != -1)
        {
            where += "?where={\"pcId\":" + pcId + "}";
        }

        if (where != "")
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(genUrl + where))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + where + ": Get Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + where + ":\nGet Received: \n" + webRequest.downloadHandler.text);
                    JSONObject obj = new JSONObject(webRequest.downloadHandler.text);
                    if (!obj.HasField("_error"))
                    {
                        if (obj.HasField("_items") && obj["_items"].list.Count > 1)
                        {
                            update = true;
                            etag = stripEnds(obj["_items"][0]["_etag"].ToString());
                            id = stripEnds(obj["_items"][0]["_id"].ToString());
                        }
                        else
                        {
                            Debug.Log("_items count " + obj["_items"].list.Count);
                        }
                    }

                    yield return webRequest.downloadHandler.text;
                }
            }
        }

        byte[] bytes = Encoding.UTF8.GetBytes(data);

        if (update)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(genUrl + "/" + id, bytes))
            {
                webRequest.SetRequestHeader("X-HTTP-Method-Override", "PATCH");
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("If-Match", etag);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + "/" + id + data + ": Patch Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + "/" + id + data + ":\n Patch Received: \n" + webRequest.downloadHandler.text);
                    Debug.Log("Etag " + etag + " ID " + id);
                    callbackFunc(webRequest.downloadHandler.text);
                    yield return webRequest.downloadHandler.text;
                }
            }
        }
        else
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(genUrl, bytes))
            {
                webRequest.SetRequestHeader("X-HTTP-Method-Override", "POST");
                webRequest.SetRequestHeader("Content-Type", "application/json");

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + data + ": Post Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + data + ":\n Post Received: \n" + webRequest.downloadHandler.text);
                    callbackFunc(webRequest.downloadHandler.text);
                    yield return webRequest.downloadHandler.text;
                }
            }
        }
    }

    public static IEnumerator CreateOrUpdateData(
        string data
        , Action<string> callbackFunc
    )
    {
        var genUrl = Url + "data";
        var where = "";

        var update = false;
        var etag = "";
        var id = "";

        var dataObj = new JSONObject(data);

        if (!dataObj.HasField("dataId"))
        {
            Debug.LogError("Not enough data you n00b");
        }

        var dataId = (int) dataObj["dataId"].n;

        if (dataId != -1)
        {
            where += "?where={\"dataId\":" + dataId + "}";
        }

        if (where != "")
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(genUrl + where))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + where + ": Get Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + where + ":\nGet Received: \n" + webRequest.downloadHandler.text);
                    JSONObject obj = new JSONObject(webRequest.downloadHandler.text);
                    if (!obj.HasField("_error"))
                    {
                        if (obj.HasField("_items") && obj["_items"].list.Count > 1)
                        {
                            update = true;
                            etag = stripEnds(obj["_items"][0]["_etag"].ToString());
                            id = stripEnds(obj["_items"][0]["_id"].ToString());
                        }
                    }

                    yield return webRequest.downloadHandler.text;
                }
            }
        }

        byte[] bytes = Encoding.UTF8.GetBytes(data);

        if (update)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(genUrl + "/" + id, bytes))
            {
                webRequest.SetRequestHeader("X-HTTP-Method-Override", "PATCH");
                webRequest.SetRequestHeader("Content-Type", "application/json");
                webRequest.SetRequestHeader("If-Match", etag);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + "/" + id + data + ": Patch Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + "/" + id + data + ":\n Patch Received: \n" + webRequest.downloadHandler.text);
                    callbackFunc(webRequest.downloadHandler.text);
                    yield return webRequest.downloadHandler.text;
                }
            }
        }
        else
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Put(genUrl, bytes))
            {
                webRequest.SetRequestHeader("X-HTTP-Method-Override", "POST");
                webRequest.SetRequestHeader("Content-Type", "application/json");

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log(genUrl + data + ": Post Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(genUrl + data + ":\n Post Received: \n" + webRequest.downloadHandler.text);
                    callbackFunc(webRequest.downloadHandler.text);
                    yield return webRequest.downloadHandler.text;
                }
            }
        }
    }
}