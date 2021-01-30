using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bounce ball state")]
public class BounceBallState : BallState
{
    public override void Move()
    {
        if(!IsFinished)
        {
            Ball.Bounce();
        }
        else
        {
            return;
        }
    }
}
