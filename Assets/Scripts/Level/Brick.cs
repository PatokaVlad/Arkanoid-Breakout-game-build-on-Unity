using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private bool useRandomValues = true;

    [SerializeField]
    private int strength = 1;
    [SerializeField]
    private int scorePoints = 100;
    [SerializeField]
    private int bonusChance = 50;
    [SerializeField]
    private int neededBonusChance = 75;

    [SerializeField]
    private int maxStrength = 4;
    [SerializeField]
    private int minStrength = 1;
    [SerializeField]
    private int maxBonusChance = 100;
    [SerializeField]
    private int minBonusChance = 25;
    [SerializeField]
    private int scorePointsMultiplier = 25;

    [SerializeField]
    private BonusHandler _bonusHandler = null;

    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private AudioHandler _audioHandler;

    private const string ballTag = "Ball";

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _audioHandler = FindObjectOfType<AudioHandler>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();

        _player.IncreaseBricksCount();

        if (useRandomValues)
            GenerateRandomValues();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ballTag))
        {
            if (strength != 0)
            {
                TransparencyChange(strength, true);
                strength--;

                if (strength == 0)
                {
                    _audioHandler.PlayBreakClip();

                    gameObject.SetActive(false);

                    ThrowBonus();

                    _player.ScoreChange(scorePoints);
                    _player.DecreaseBricksCount();
                }    
            }
        }
    }

    private void TransparencyChange(float changeMultiplier, bool useDivision)
    {
        Color _brickColor = _spriteRenderer.color;

        if (useDivision)
            _brickColor.a -= _brickColor.a / changeMultiplier;
        else
            _brickColor.a = changeMultiplier / maxStrength;

        _spriteRenderer.color = _brickColor;
    }

    private void GenerateRandomValues()
    {
        strength = Random.Range(minStrength, maxStrength);
        scorePoints = strength * scorePointsMultiplier;
        bonusChance = Random.Range(minBonusChance, maxBonusChance);

        TransparencyChange(strength, false);
    }

    private void ThrowBonus()
    {
        if (bonusChance >= neededBonusChance)
        {
            _bonusHandler.SpawnBonus(_transform.position);
        }
    }
}
