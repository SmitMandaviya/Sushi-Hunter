using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
        public static UI instance;
    
    
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI ammo;

    private int scorevalue;

    [Space]

    [SerializeField] private GunController guncontroller;
    //[SerializeField] private GameObject restartbutton;
    [SerializeField] private GameObject gameoverscreen;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 1)
        {
            timer.text = Time.time.ToString("#,#");
        }

        ammo.text = guncontroller.currentbullet + " / " + guncontroller.maxbullet;

    }

    //addition to the total score
    public void addscore()
    {
        scorevalue++;
        score.text = scorevalue.ToString();
    }

    //activate end screen
    public void OpenEndScreen()
    {
        Time.timeScale = 0;
        //restartbutton.SetActive(true);
        gameoverscreen.SetActive(true);
    }
    
    //restart / reload the game scene
    public void restartgame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);

    }
    //to quit the game
    public void quit()
    {
        Application.Quit();
    }
}
