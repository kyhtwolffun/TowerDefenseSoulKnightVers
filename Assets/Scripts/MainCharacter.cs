using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [Header("Character's properties")]
    [SerializeField] private float speed;

    [Header("Weapons")]
    [SerializeField] private WeaponSystem weaponSystem;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private bool isFacingRight = true;
    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            FlipCharacter();
        }
        animator.SetFloat("Magnitude", movement.sqrMagnitude);

        //Input Attack
        if (Input.GetKeyDown(KeyCode.A))
            Attack();

        //Input Switch Owned Weapon
        if (Input.GetKeyDown(KeyCode.Space))
            SwitchWeapon();
    }

    private void FixedUpdate()
    {
        Move(movement);
    }

    private void Move(Vector2 movement)
    {
        /*rb.velocity = movement * speed;*/
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        weaponSystem.Attack();
    }

    private void SwitchWeapon()
    {
        weaponSystem.SwitchWeapon();
    }

    private void FlipCharacter()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        isFacingRight = !isFacingRight;
    }

    private void FlipObject()
    {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }
}
