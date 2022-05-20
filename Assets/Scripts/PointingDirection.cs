using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointingDirection : MonoBehaviour
{
    [SerializeField] private bool isMousePointing;
    [SerializeField] private Camera cam;

    private Vector2 mousePos;
    private Vector2 lookDir;
    private float angle;

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (isMousePointing)
        {
            lookDir = mousePos - (Vector2)transform.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle);
        }
    }
}
