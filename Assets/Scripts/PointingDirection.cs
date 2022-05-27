using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PointingDirection : MonoBehaviour
{
    [SerializeField] private bool isMousePointing;
    [Header("For auto aiming")]
    [SerializeField] private Transform target;
    public void SetTarget (Transform transform)
    {
        target = transform;
    }
    [SerializeField] private Detect detection;
    //[SerializeField] private Camera cam;

    private Vector2 mousePos;
    private Vector2 lookDir;
    private float angle;
    //Raycast for attack-range
    private RaycastHit2D[] raycastHits;
    private float range;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.up * range, Color.red);
    }

    private void FixedUpdate()
    {
        if (isMousePointing)
        {
            lookDir = mousePos - (Vector2)transform.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle);
        }
        else
        {
            //TODO: erase after get detection for monster
            if (detection && !detection.EnemyDetected)
                return;
            if (detection && detection.EnemyDetected)
                target = detection.GetClosestEnemy();
            if (target)
                lookDir = (Vector2)target.position - (Vector2)transform.position;

            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle);
        }
    }

    public void UpdateAttackRange(float _range)
    {
        range = _range; 
    }

    public void ShootRay()
    {
        raycastHits = Physics2D.RaycastAll(transform.position, transform.up, range);
    }

    public bool IsTargetInRange()
    {
        ShootRay();
        for (int i = 0; i < raycastHits.Length; i++)
        {
            if (raycastHits[i])
            {
                //Debug.Log("Hit " + raycastHits[i].collider.name);
                if (target && raycastHits[i].transform.gameObject == target.gameObject)
                {
                    //Debug.Log("Hit target");
                    return true;
                }
            }
        }
        return false;
    }
}
