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
        
    }

    public void Login(string name, string email)
    {
        FetchAccount(name, email);
    }

    IEnumerator FetchAccount(string name, string email)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("email", email);

        // Create a download object
        using (UnityWebRequest w = UnityWebRequest.Post(URL_PREFIX + getUser_url, form))
        {

            // Wait until the download is done
            yield return w.SendWebRequest();

            if (w.result == UnityWebRequest.Result.ConnectionError ||
                w.result == UnityWebRequest.Result.ProtocolError)
            {
                print("Error: " + w.error);
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

                    MainMenu.instance.LoginDone();
                }
                else if(tex == "")
                {
                    StartCoroutine(CreateAccount(name, email));
                }
                else
                    print("fail to connect db");
            }
        }
    }

    IEnumerator CreateAccount(string name, string email)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        form.AddField("name", name);
        form.AddField("email", email);

        // Create a download object
        using (UnityWebRequest w = UnityWebRequest.Post(URL_PREFIX + addUser_url, form))
        {

            // Wait until the download is done
            yield return w.SendWebRequest();

            if (w.result == UnityWebRequest.Result.ConnectionError ||
                w.result == UnityWebRequest.Result.ProtocolError)
            {
                print("Error: " + w.error);
            }
            else
            {
                string tex = w.downloadHandler.text;

                if (tex != "" && tex != "1: Connection failed")
                {
                    print("new account added: " + name + ", "+ email);
                    FetchAccount(name, email);
                }
                else
                {
                    print("fail to add new account, " + tex);
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
                print("Error: " + w.error);
            }
            else
            {
                string tex = w.downloadHandler.text;

                if (tex != "" && tex != "1: Connection failed")
                {
                    print("level updated for id: " + userID);
                }
                else
                {
                    print("fail to update level for id: " + userID + ", " + tex);
                }
            }
        }
    }
}
