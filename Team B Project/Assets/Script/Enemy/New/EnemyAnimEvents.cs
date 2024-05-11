using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvents : MonoBehaviour
{
    private Enemy_Skeleton enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy_Skeleton>();
    }

    private void AnimationTrigger()
    {
        enemy.AttackOver();
    }
}
