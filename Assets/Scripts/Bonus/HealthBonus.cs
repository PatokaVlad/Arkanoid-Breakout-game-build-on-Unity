using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : BonusMovement
{
    private Player _player = null;

    protected override void Initialize()
    {
        _player = FindObjectOfType<Player>();
    }

    protected override void ActivateBonus()
    {
        _player.AddLive(1);
    }
}
