using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrols : MonoBehaviour
{
    [SerializeField]
    private EnemyPatrolInfo[] Patrols;

    [SerializeField]
    private LayerMask area;

    [SerializeField]
    private int max;

    public int get_max() { return this.max; }
    public EnemyPatrolInfo [] GetPatrols()
    {
        return Patrols;
    }

    public LayerMask GetLayer()
    {
        return area;
    }
}
