using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public GameObject pickup;
    new private Rigidbody2D rigidbody;
    private RocketController rocketController;
    private bool isGrounded = false;
    private bool walkRight = false;
    private float checkRadius = 0.22f;
    private int moveValue = 0;


    private void Start()
    {
        // get rigidbody
        rigidbody = GetComponent<Rigidbody2D>();

        // get rocketcontroller
        rocketController = GameObject.FindGameObjectWithTag("Player").GetComponent<RocketController>();

        // create a random move value
        moveValue = Random.Range(1,3);

        // invoke movearound every 2 seconds
        InvokeRepeating("ChangeDirection", 0.25f, 2);
    }


    private void FixedUpdate()
    {
        // set isgrounded
        isGrounded = Physics2D.OverlapCircle(transform.position, checkRadius, LayerMask.GetMask("Ground"));

        // return when not on ground
        if (!isGrounded) return;
       
        // move around randomly
        if (!rocketController.isGrounded) MoveAround();

        // move to player
        else if (rocketController.isGrounded) MoveTowardsPlayer();
    }


    /// <summary>
    /// ivoke this method to change direction
    /// </summary>
    private void ChangeDirection()
    {
        // change state
        switch (walkRight)
        {
            case false:
                walkRight = true;
                break;
            case true:
                walkRight = false;
                break;
        }
    }


    /// <summary>
    /// move around ramdomly
    /// </summary>
    public void MoveAround()
    {
        // walk left or right
        if (walkRight == true) rigidbody.velocity = Vector2.right * moveValue; 
        if (walkRight == false) rigidbody.velocity = Vector2.left * moveValue;
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
            Instantiate(pickup, transform.position, Quaternion.identity);
            statistic.PickupCrewMember(2000, gameObject);
        }
    }
}
