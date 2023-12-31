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
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TMP_Dropdown colorSelection;


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
            if (name_.text == "" || email_.text == "")
                return;

            WaitingPage.SetActive(true);
            LoginPage.SetActive(false);
            DBManager.instance.Login(name_.text, email_.text);
        });

        LvlUp.onClick.AddListener(() => 
        {
            lvlText.text = DataManager.instance.LevelUp().ToString();
        });

        colorSelection.onValueChanged.AddListener((change) => { DataManager.instance.color = change; });
    }

    public void LoginDone()
    {
        nameText.text = name_.text;
        lvlText.text = DataManager.instance.level_.ToString();
        LoginParent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
