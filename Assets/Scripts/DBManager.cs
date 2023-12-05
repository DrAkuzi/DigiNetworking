using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DBManager : MonoBehaviour
{
    const string URL_PREFIX = "https://interactivewedding.com/digipenInterview/";
    const string addUser_url = "addNewUser.php";
    const string updateUserLevel_url = "updateUserLevel.php";
    const string getUser_url = "getUser.php";
    const string test_url = "test.php";

    public static DBManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartCoroutine(Test());
    }

    public void Login(string name, string email)
    {
        StartCoroutine(FetchAccount(name, email));
    }

    IEnumerator FetchAccount(string name, string email)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("email", email);

        Debug.Log("trying to fetch account");

        // Create a download object
        using (UnityWebRequest w = UnityWebRequest.Post(URL_PREFIX + getUser_url, form))
        {
            // Wait until the download is done
            yield return w.SendWebRequest();

            if (w.result == UnityWebRequest.Result.ConnectionError ||
                w.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + w.error + ", " + w.result);
            }
            else
            {
                string tex = w.downloadHandler.text;

                if (tex != "" && tex != "1: Connection failed")
                {
                    string[] info = tex.Split('|');
                    //print(line);
                    int id = Convert.ToInt32(info[0]);
                    int lvl = Convert.ToInt32(info[1]);

                    DataManager.instance.id_ = id;
                    DataManager.instance.level_ = lvl;
                    DataManager.instance.name_ = name;
                    DataManager.instance.email_ = email;

                    Debug.Log("login successful");
                    MainMenu.instance.LoginDone();
                }
                else if(tex == "")
                {
                    StartCoroutine(CreateAccount(name, email));
                }
                else
                    Debug.Log("fail to connect db");
            }
        }
    }

    IEnumerator CreateAccount(string name, string email)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("name", name);
        form.AddField("email", email);

        Debug.Log("creating new account");

        // Create a download object
        using (UnityWebRequest w = UnityWebRequest.Post(URL_PREFIX + addUser_url, form))
        {

            // Wait until the download is done
            yield return w.SendWebRequest();

            if (w.result == UnityWebRequest.Result.ConnectionError ||
                w.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + w.error + ", " + w.result);
            }
            else
            {
                string tex = w.downloadHandler.text;

                if (tex != "" && tex != "1: Connection failed")
                {
                    Debug.Log("new account added: " + name + ", "+ email);
                    FetchAccount(name, email);
                }
                else
                {
                    Debug.Log("fail to add new account, " + tex);
                }
            }
        }
    }

    public void UpdateUserLevel(int userID, int lvl)
    {
        StartCoroutine(UpdateUserLevelQuery(userID, lvl));
    }

    IEnumerator UpdateUserLevelQuery(int userID, int lvl)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("id", userID);
        form.AddField("level", lvl);

        // Create a download object
        using (UnityWebRequest w = UnityWebRequest.Post(URL_PREFIX + updateUserLevel_url, form))
        {

            // Wait until the download is done
            yield return w.SendWebRequest();

            if (w.result == UnityWebRequest.Result.ConnectionError ||
                w.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + w.error + ", " + w.result);
            }
            else
            {
                string tex = w.downloadHandler.text;

                if (tex != "" && tex != "1: Connection failed")
                {
                    Debug.Log("level updated for id: " + userID);
                }
                else
                {
                    Debug.Log("fail to update level for id: " + userID + ", " + tex);
                }
            }
        }
    }

    IEnumerator Test()
    {
        using (UnityWebRequest w = UnityWebRequest.Get(URL_PREFIX + test_url))
        {
            // Wait until the download is done
            yield return w.SendWebRequest();

            switch (w.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error for presets: " + w.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error for presets: " + w.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + w.downloadHandler.text);
                    break;
            }
        }
    }
}
