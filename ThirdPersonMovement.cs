using System.Collections; //Libraries
using System.Collections.Generic;
using UnityEngine;


// Manages third person movement
public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam; //initiating game objects and variables

    // Reference to speed and movmeent
    public float speed = 1f;
    public float turnSmoothTime = 0.1f;
    public float speedToChange = 2f;
    private float _turnSmoothVelocity;
    private Animator _anim;

    // Reference to spawned in arrows and bow
    public GameObject arrow;
    public GameObject swordHand;
    public GameObject bowHand;
    public Transform arrowSpawn;
    public float shootForce = 20f;
    public Camera mainCam;

    
    
    private void Start()
    {
        Cursor.visible = false; //lock the cursor into the game view and make it invisible 
        Screen.lockCursor = true; 
        _anim = GetComponent<Animator>();
    }
    void Update()
    {

        _anim.SetBool("Greeting", Input.GetKey(KeyCode.G));//Greeting animation is done if the player presses down on 'G"
        
        float horizontal = Input.GetAxisRaw("Horizontal");//declare the horizontal mouse movements into a float
        float vertical = Input.GetAxisRaw("Vertical");//declare the vertical mouse movements into a float
        if (horizontal == 0 || vertical == 0)
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//not moving at all
        else
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude < 0.1f) // determines the direction in which the player will run
            direction = new Vector3(horizontal, 0f, Input.GetAxisRaw("Run"));
        if (Input.GetKeyDown(KeyCode.Space))// if the space bar is pressed the jump animation will occur 
            _anim.SetTrigger("Jump");
        if (direction.magnitude >= 0.1f)
        {
            //Determining proper orientation 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //angle at which the camera rotates on 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
           //translate the game object
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // Set movement animation based on movement
        if (Input.GetAxis("Vertical") > 0)
        {
            _anim.SetFloat("Walking", Input.GetAxis("Vertical"));
            _anim.SetFloat("Running", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime);
        }
        else if (Input.GetAxis("Run") > 0)
        {
            _anim.SetFloat("Running", Input.GetAxis("Run"));
            _anim.SetFloat("Walking", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Run") * Time.deltaTime * speedToChange);
        }
        else
        {
            _anim.SetFloat("Running", 0f);
            _anim.SetFloat("Walking", 0f);
        }

        // Activiate based on active shooting method
        _anim.SetBool("Shooting", Input.GetMouseButton(0) && bowHand.activeSelf);
        _anim.SetBool("Hit", Input.GetMouseButton(0) && swordHand.activeSelf);
        // Move camera based on mouse movement
        if (Input.GetMouseButton(0) && (bowHand.activeSelf || swordHand.activeSelf))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    // Shoot method
    public void Shoot()
    {
        // Create arrow and apply force
        GameObject arrowGO = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        Rigidbody arrowRB = arrowGO.GetComponent<Rigidbody>();
        arrowRB.velocity = cam.transform.forward * shootForce;
    }
}
