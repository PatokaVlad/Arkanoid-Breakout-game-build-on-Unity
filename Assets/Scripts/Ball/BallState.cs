using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallState : ScriptableObject
{
    public BallMovement Ball { get; set; }
    public bool IsFinished { get; set; }

    public abstract void Move();
}
