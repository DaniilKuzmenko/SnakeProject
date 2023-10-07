using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel, gamePanel;
    public Text startText;
    [SerializeField]
    private Text scoreText, bestScoreText;
    public int score;
    void Start()
    {
        score = 0;
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
         scoreText.text = "Score: " + score.ToString();
    }
    private void BackToMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void SwitchPause(){
        pausePanel.SetActive(!pausePanel.activeSelf);
        gamePanel.SetActive(!gamePanel.activeSelf);

        switch(Time.timeScale)
        {
            case 1:
                Time.timeScale = 0;
                break;
            case 0:
                Time.timeScale = 1;
                break;
        }
    }
    
}
