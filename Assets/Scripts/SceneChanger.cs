using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    bool isServer;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneHasLoaded;
    }

    void SceneHasLoaded(Scene scene, LoadSceneMode arg1)
    {
        switch(scene.buildIndex)
        {
            case 1:
                GameObject.Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() =>
                {
                    SceneManager.LoadScene(2);
                });
                return;
            case 2:
                if(isServer)
                    NetworkManager.Singleton.StartServer();
                else
                    NetworkManager.Singleton.StartClient();
                return;
        }    
    }

    void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-launch-as-server")
            {
                //
                isServer = true;
                break;
            }
        }
        //if(!isServer) 
        DontDestroyOnLoad(gameObject);
        if(isServer)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneHasLoaded;
    }
}
