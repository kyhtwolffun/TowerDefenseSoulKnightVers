using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Monster : MonoBehaviour
{
    [SerializeField] WeaponSystem weaponSystem;
    [SerializeField] PointingDirection pointingDirection;
    [SerializeField] float minimunAttackDelay;
    [SerializeField] [Range(0f, 2f)] float attackDelayNoise;

    [SerializeField] DamageableBase health;
    [SerializeField] AIPath aiPath;

    private bool attackFlag = true;

    public void Init(MonsterData monsterData, float buffPercent = 0)
    {
        health.InitHealth((int)(monsterData.Health * (1 + buffPercent)));
        aiPath.maxSpeed = monsterData.Speed * (1 + buffPercent);
    }

    private void FixedUpdate()
    {
        if (attackFlag && pointingDirection.IsTargetInRange())
        {
            weaponSystem.Attack();
            StartCoroutine(CoolDownAttackDelay());
        }
    }

    private IEnumerator CoolDownAttackDelay()
    {
        attackFlag = false;
        yield return new WaitForSeconds(minimunAttackDelay + Random.Range(0f, attackDelayNoise));
        attackFlag = true;
    }
}
