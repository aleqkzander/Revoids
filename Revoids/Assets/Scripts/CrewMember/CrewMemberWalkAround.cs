using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMemberWalkAround : MonoBehaviour
{
    public float timeToChangeDirection = 2f;

    private void Start()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        float angle = 0f;
        float left = -180;
        float right = 180;

        float randomNumber = Random.Range(0,1);

        if (randomNumber > 0.5f)
        {
            angle = right;
        }
        else
        {
            angle = left;
        }

        Quaternion quat = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 newUp = quat * Vector3.up;
        newUp.z = 0;
        newUp.Normalize();
        transform.up = newUp;
        timeToChangeDirection = 1.5f;
    }

    public void MovePlayer(Rigidbody2D rigidbody)
    {
        timeToChangeDirection -= Time.fixedDeltaTime;

        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();
        }

        rigidbody.velocity = transform.up * 2;
    }
}
