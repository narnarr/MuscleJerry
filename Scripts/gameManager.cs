using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public GameObject panel;
    public GameObject cat;
    public bool gameEnd = false;

    float totalScore = 0;
    public Text scoreText;
    public Text panelScoreText;

    public float bestScore;
    public Text bestScoreText;

    private void Awake()
    {
        I = this;
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
        bestScoreText.text = bestScore.ToString("N2");
    }

    // Start is called before the first frame update
    void Start()
    {
        initGame();
    }

    void initGame()
    {
        Time.timeScale = 1.0f;
        totalScore = 0;
    }

    public void addScore(float score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString("N2");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(cat.transform.position);

        if (gameEnd == false && pos.y < 0)
        {
            gameEnd = true;
            endGame();
        }
        //scoreText.text = totalScore.ToString("N2");
    }

    void endGame()
    {
        panelScoreText.text = totalScore.ToString("N2");

        if (totalScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", totalScore);
        }

        float endY = Camera.main.ScreenToWorldPoint(new Vector3(0, 639, 0)).y;
        panel.transform.position += new Vector3(0, endY, 0);
        panel.SetActive(true);
       
        Time.timeScale = 0;
        Application.Quit();
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    
}
