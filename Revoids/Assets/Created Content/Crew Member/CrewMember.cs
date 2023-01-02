using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private RocketController rocketController;
    private bool isGrounded;
    private Vector2 startPosition;
    private Vector2 leftPosition;


    private void Start()
    {
        // get rigidbody
        rigidbody = GetComponent<Rigidbody2D>();

        // get rocketcontroller
        rocketController = GameObject.FindGameObjectWithTag("Player").GetComponent<RocketController>();

        // create a random value
        int randomValue = Random.Range(3,6);

        // get gameobject start position
        startPosition.x = transform.position.x;

        // set left position
        leftPosition = new Vector2(startPosition.x + randomValue, 0);
    }


    private void FixedUpdate()
    {
        // return when not on ground
        if (!isGrounded) return;
        
        // move to player when on ground
        if (rocketController.isGrounded) MoveTowardsPlayer();

        // move around randomly
        else MoveAround();
    }


    /// <summary>
    /// move randomy around
    /// </summary>
    public void MoveAround()
    {
        if (transform.position.x == startPosition.x) rigidbody.MovePosition(leftPosition * 1.5f * Time.deltaTime);
        else if (transform.position.x == leftPosition.x) rigidbody.MovePosition(startPosition * 1.5f * Time.deltaTime);
    }


    /// <summary>
    /// move towards player
    /// </summary>
    public void MoveTowardsPlayer()
    {
        // get player position
        Vector2 playerPosition = new Vector2(rocketController.transform.position.x, 0);

        // get member position
        Vector2 memberPosition = new Vector2(transform.position.x, 0);

        // calulcate direction
        Vector3 direction = (playerPosition - memberPosition).normalized;

        // move towards player with speed 2.5
        rigidbody.MovePosition(transform.position + direction * 2.5f * Time.deltaTime);
    }


    /// <summary>
    /// enter ship when colliding with player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RocketStatistic statistic = collision.gameObject.transform.GetChild(1).GetComponent<RocketStatistic>();
            statistic.PickupCrewMember(2000, gameObject);
        }
    }


    #region set ground check
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
    #endregion
}
