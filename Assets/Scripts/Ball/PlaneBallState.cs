using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plane ball state")]
public class PlaneBallState : BallState
{
    public override void Move()
    {
        if(!IsFinished)
        {
            Ball.StickToPlane();
        }
        else
        {
            return;
        }
    }
}
