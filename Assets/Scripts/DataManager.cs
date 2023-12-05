using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [HideInInspector] public string name_;
    [HideInInspector] public string email_;
    [HideInInspector] public int level_;
    [HideInInspector] public int id_;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int LevelUp()
    {
        DBManager.instance.UpdateUserLevel(id_, ++level_);
        return level_;
    }
}
