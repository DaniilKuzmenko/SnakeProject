using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        scoreText.text = "Score: " + score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
