using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private UIHandler _uiHandler = null;

    [SerializeField]
    private int startLivesCount = 3;
    [SerializeField]
    private int bricksCount = 0;
    private int score = 0;
    private int lives;

    [SerializeField]
    private bool stopGame = false;

    public bool StopGame { get => stopGame; }

    public delegate void OnLivesDecrease();
    public event OnLivesDecrease onLivesDecrease;

    private void Start()
    {
        lives = startLivesCount;

        _uiHandler.ChangeLivesText(lives);
        _uiHandler.ChangeScoreText(score);
    }

    public void IncreaseBricksCount() { bricksCount++; }

    public void DecreaseBricksCount()
    {
        if (bricksCount != 0)
        {
            bricksCount--;

            if (bricksCount == 0)
            {
                GameOver(true, true);
            }
        }
    }

    public void LivesDecrease()
    {
        if (lives != 0)
        {
            lives--;
            _uiHandler.ChangeLivesText(lives);
            RaiseOnLivesDecrease();

            if (lives == 0)
            {
                GameOver(true, true);
            }
        }
    }

    public bool LivesZeroCheck()
    {
        return lives == 0;
    }

    public void ScoreChange(int count)
    {
        score += count;
        _uiHandler.ChangeScoreText(score);
    }

    private void RaiseOnLivesDecrease()
    {
        if(onLivesDecrease != null)
        {
            onLivesDecrease();
        }
    }

    public void AddLive(int count)
    {
        lives += count;
        _uiHandler.ChangeLivesText(lives);
    }

    public void Restart()
    {
        lives = startLivesCount;
        score = 0;

        _uiHandler.ChangeLivesText(lives);
        _uiHandler.ChangeScoreText(score);

        RaiseOnLivesDecrease();

        GameOver(false, false);
    }

    private void GameOver(bool stopPlay, bool activeGameOver)
    {
        stopGame = stopPlay;
        _uiHandler.StopGame(score, activeGameOver);
    }
}
