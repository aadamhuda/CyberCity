using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolInfo : MonoBehaviour
{
    public Vector3[] PatrolPoints;

    // Store of patrol for enemies
    public Vector3 GetFirst ()
    {
        return PatrolPoints[0];
    }

    public Vector3[] GetPath()
    {
        return PatrolPoints;
    }
}
