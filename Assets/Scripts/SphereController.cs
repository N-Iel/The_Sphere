using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

    #region Variables
    // Public
    public float speed, thrust;
    public GameManager gameManager;

    // Movement
    float h, v;
    bool onGround, jump;
    Vector3 movement;
    Rigidbody rb;

    // Raycats
    Ray ray;
    RaycastHit hit;
    public LayerMask layerGround;
    #endregion

    #region Start
    // Assignments
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    #endregion

    #region Update Checks
    // Input and raycast checks
    void Update()
    {
        InputPlayer();
        CheckGround();
    }

    //It will get the inputs and update the movement
    void InputPlayer()
    {
        // Movement
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        movement = new Vector3(h, 0, v);
        movement.Normalize();

        // Jump
        if (Input.GetMouseButtonDown(0) && onGround)
        {
            jump = true;
        }
    }

    // It will keep track of when the player is on the ground
    void CheckGround()
    {
        ray.origin = transform.position;
        ray.direction = Vector3.down;

        if (Physics.Raycast(ray, out hit, 1, layerGround))
            onGround = true;
        else
            onGround = false;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
    }
    #endregion

    #region FixedUpdate Physics
    // Update applied to physics
    private void FixedUpdate()
    {
        // Movement
        Movement();

        // Jump
        if(jump) Jump();
    }

    // It will apply the movement to the player rb
    void Movement()
    {
        rb.AddForce(movement * speed);
    }

    // It will apply vertical force to the player rb
    void Jump()
    {
        rb.AddForce(Vector3.up * thrust);
        jump = false;
    }
    #endregion

    #region colliders

    // It will check for collisions with the walls
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            gameManager.HaSidoHit();
    }

    #endregion
}
