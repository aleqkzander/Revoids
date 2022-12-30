using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberWalkAround : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool walkRight = true;

    void Start()
    {
        InvokeRepeating("ChangeBool", 0.1f, 5);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayer);
    }

    private void ChangeBool()
    {
        switch (walkRight)
        {
            case true:
                walkRight = false;
                break;
            case false:
                walkRight = true;
                break;
        }
    }

    public void MovePlayer(Rigidbody2D rigidbody)
    {
        if (!isGrounded) return;

        if (walkRight)
        {
            rigidbody.velocity = Vector2.right * 1.0f;
        }
        else
        {
            rigidbody.velocity = Vector2.left * 1.0f;
        }
    }

}
