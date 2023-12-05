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

    public static SceneChanger instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneHasLoaded;
    }

    void SceneHasLoaded(Scene scene, LoadSceneMode arg1)
    {
        switch(scene.buildIndex)
        {
            case 1:
                return;
            case 2:
                if(isServer)
                    NetworkManager.Singleton.StartServer();
                else
                    NetworkManager.Singleton.StartClient();
                return;
        }    
    }

    private void Awake()
    {
        instance = this;
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
            LoadPlayScene();
        else
            SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene(2);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneHasLoaded;
    }
}
