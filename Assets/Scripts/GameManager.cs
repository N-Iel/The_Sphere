using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Sound
    AudioSource audioSource;
    public AudioClip collectClip;
    public AudioClip lvlCompletedClip;
    public AudioClip gameCompleted;
    public AudioClip nohitCompleted;

    // Score
    int maxScore, score, phase;
    int[] maxScores = { 2, 4, 8, 6, 12, 9};
    bool noHit;

    // UI
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI timeText;
    public GameObject scoreUI;
    public GameObject winUI;
    public GameObject extraUI;
    public GameObject controlsUI;
    float seconds, minutes;
    bool countTime;

    // Player
    Vector3 origPlayerPos;
    GameObject player;

    // Phases
    public GameObject[] phases;

    #region StartUp
    // Start is called before the first frame update
    void Start()
    {
        SetUpScene();
    }

    void SetUpScene()
    {
        // Phase setUp
        phase = 0;
        score = 0;
        maxScore = maxScores[0];

        // Player
        player = GameObject.FindGameObjectWithTag("Player");
        origPlayerPos = player.transform.position;
        noHit = true;

        // Score text
        UpdateScoreText();
        countTime = true;
        seconds = 0;
        minutes = 0;

        // Audio
        audioSource= GetComponent<AudioSource>();
    }
    #endregion

    #region Update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");

        if (Input.GetKeyDown(KeyCode.Escape) && controlsUI.activeInHierarchy)
            Application.Quit();

        if (countTime)
            UpdateTime();
    }

    void UpdateTime()
    {
        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            seconds = 0;
            minutes++;
        }
        string min = minutes > 10 ? minutes.ToString() : "0" + minutes;
        string sec = seconds > 10 ? Mathf.Floor(seconds).ToString() : "0" + Mathf.Floor(seconds);
        timeText.text = min + ":" + sec;
    }
    #endregion

    #region Score
    // It will add one point to the score and trigger the sound
    public void AddScore()
    {
        audioSource.PlayOneShot(collectClip);
        score++;
        CheckScore();
        UpdateScoreText();
    }

    // It will update the score txt
    void UpdateScoreText()
    {
        scoreText.text = score + " / " + maxScore;
    }

    // It will check every +Score
    void CheckScore()
    {
        if (score == maxScore)
        {
            phase++;

            if (phase < phases.Length)
            {
                // Lvl complete audio
                audioSource.PlayOneShot(lvlCompletedClip);

                // Get next phase ready
                Invoke("NextPhase", 2);
            }
            else
            {
                Win();
            }
            
        }
            
    }
    #endregion

    #region Phase
    void NextPhase()
    {
        // Reset Player Pos
        ResetPlayerPosition();

        // Change phase
        SetNewPhase();

        // Reset Score
        score = 0;

        // Update maxScore
        maxScore = maxScores[phase];

        // Update text Score
        UpdateScoreText();
    }

    void SetNewPhase()
    {
        phases[phase - 1].SetActive(false);
        phases[phase].SetActive(true);
    }

    void Win()
    {
        countTime = false;
        scoreUI.SetActive(false);
        if (noHit)
        {
            victoryText.text = "Te sacaste la nohit";
            audioSource.PlayOneShot(nohitCompleted);
        }
        else
        {
            audioSource.PlayOneShot(gameCompleted);
        }
        winUI.SetActive(true);
        controlsUI.SetActive(true);
        Invoke("ExtraInfo", 2);
    }

    // It will show some extra features
    void ExtraInfo()
    {
        extraUI.SetActive(true);
    }

    public void ResetPhase()
    {
        // Reset player pos
        ResetPlayerPosition();

        // Active all the collectibles
        GameObject collectible = GameObject.FindGameObjectsWithTag("Collectibles")[phase];
        foreach (GameObject child in collectible.transform)
        {
            child.SetActive(true);
        }

        // Set score to 0
        score = 0;
    }
    #endregion

    #region Utils
    void ResetPlayerPosition()
    {
        player.transform.position = origPlayerPos;
    }

    // Fin de la no-hit
    public void HaSidoHit()
    {
        noHit = false;
        Debug.Log("Ha sido hit");
    }
    #endregion


}
