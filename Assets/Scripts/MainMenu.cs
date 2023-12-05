using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button LoginBtn;
    [SerializeField] Button PlayBtn;
    [SerializeField] Button LvlUp;
    [SerializeField] GameObject LoginParent;
    [SerializeField] GameObject LoginPage;
    [SerializeField] GameObject WaitingPage;
    [SerializeField] TMP_InputField name_;
    [SerializeField] TMP_InputField email_;
    [SerializeField] TextMeshProUGUI lvlText;

    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBtn.onClick.AddListener(() =>
        {
            SceneChanger.instance.LoadPlayScene();
        });

        LoginBtn.onClick.AddListener(() =>
        {
            WaitingPage.SetActive(true);
            LoginPage.SetActive(false);
            DBManager.instance.Login(name_.text, email_.text);
        });

        LvlUp.onClick.AddListener(() => 
        {
            lvlText.text = DataManager.instance.LevelUp().ToString();
        });
    }

    public void LoginDone()
    {
        LoginParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
