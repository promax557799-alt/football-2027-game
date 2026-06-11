using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private float matchDuration = 5400f; // 90 minutes
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text matchStatusText;

    private float timeRemaining;
    private int playerTeamScore = 0;
    private int opponentTeamScore = 0;
    private bool matchActive = true;
    private Ball ballScript;

    void Start()
    {
        timeRemaining = matchDuration;
        ballScript = FindObjectOfType<Ball>();
        UpdateUI();
    }

    void Update()
    {
        if (!matchActive) return;

        timeRemaining -= Time.deltaTime;
        
        if (timeRemaining <= 0)
        {
            EndMatch();
        }

        UpdateUI();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (matchActive && collision.CompareTag("Goal"))
        {
            if (collision.name.Contains("PlayerGoal"))
            {
                opponentTeamScore++;
            }
            else if (collision.name.Contains("OpponentGoal"))
            {
                playerTeamScore++;
            }
            
            ResetBall();
        }
    }

    private void ResetBall()
    {
        if (ballScript != null)
        {
            ballScript.ResetPosition(Vector3.zero);
        }
    }

    private void UpdateUI()
    {
        int minutes = (int)(timeRemaining / 60);
        int seconds = (int)(timeRemaining % 60);
        
        if (scoreText != null)
            scoreText.text = $"{playerTeamScore} - {opponentTeamScore}";
        
        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void EndMatch()
    {
        matchActive = false;
        
        string result;
        if (playerTeamScore > opponentTeamScore)
            result = "YOU WIN!";
        else if (playerTeamScore < opponentTeamScore)
            result = "YOU LOST!";
        else
            result = "DRAW!";
        
        if (matchStatusText != null)
            matchStatusText.text = result;
    }

    public int GetPlayerScore()
    {
        return playerTeamScore;
    }

    public int GetOpponentScore()
    {
        return opponentTeamScore;
    }

    public bool IsMatchActive()
    {
        return matchActive;
    }
}
