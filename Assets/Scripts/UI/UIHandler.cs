using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _livesText = null;

    [SerializeField]
    private GameObject _gameOver = null;
    [SerializeField]
    private Text _finalScoreText = null;

    public void ChangeLivesText(int lives)
    {
        _livesText.text = "Lives: " + lives.ToString();
    }

    public void ChangeScoreText(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver(int score, bool active)
    {
        _gameOver.SetActive(active);

        _finalScoreText.text = "Final score: " + score.ToString();

        _livesText.gameObject.SetActive(!active);
        _scoreText.gameObject.SetActive(!active);
    }
}
