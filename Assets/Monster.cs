using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] WeaponSystem weaponSystem;
    [SerializeField] PointingDirection pointingDirection;
    [SerializeField] float minimunAttackDelay;
    [SerializeField] [Range(0f, 2f)] float attackDelayNoise;

    private bool attackFlag = true;

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
