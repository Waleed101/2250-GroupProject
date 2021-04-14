using System.Collections; //Libraries
using System.Collections.Generic;
using UnityEngine;


// Manages third person movement
public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam; //initiating game objects and variables

    // Reference to the character info file
    [SerializeField] private CharacterInfo _characterRef;

    // Reference to speed and movmeent
    private float _speed;
    public float turnSmoothTime = 0.1f;
    private float _speedToChange;
    private float _turnSmoothVelocity;
    private Animator _anim;

    // Reference to spawned in arrows and bow
    public GameObject arrow;
    public GameObject swordHand;
    public GameObject bowHand;
    public Transform arrowSpawn;
    public float shootForce = 50f;
    public Camera mainCam;

    //Allow for potion to be drunk
    public int potion ;

    private bool _movementEnabled = true;
    
    
    private void Start()
    {
        Cursor.visible = false; //lock the cursor into the game view and make it invisible 
        Screen.lockCursor = true; 
        _anim = GetComponent<Animator>();
        _speed = _characterRef.GetSpeed();
        if (_speed == 0)
            _speed = 1f;
        _speedToChange = _speed * 1.8f;
        
    }
    void Update()
    {
        if(_movementEnabled)
        {
            _anim.SetBool("Greeting", Input.GetKey(KeyCode.G));//Greeting animation is done if the player presses down on 'G"
            _anim.SetBool("Drinking", Input.GetKeyDown(KeyCode.L) && ButtonHandler.potionAmount >= 1);//Drinking Potion animation if player presses down on "L"
              
                if (ButtonHandler.potionAmount >= 1 && Input.GetKeyDown(KeyCode.L))
                {
                
                    FindObjectOfType<AudioTriggers>().PlayDrinking();
                    int index = ButtonHandler.potionInventory.Count - ButtonHandler.potionAmount;
                    ButtonHandler.potionAmount--;
                    print(ButtonHandler.potionInventory[index]+" Was Drank");
                    FindObjectOfType<PotionEffects>().AffectPlayer(ButtonHandler.potionInventory[index]);
            }
            
            float horizontal = Input.GetAxisRaw("Horizontal");//declare the horizontal mouse movements into a float
            float vertical = Input.GetAxisRaw("Vertical");//declare the vertical mouse movements into a float
            if (horizontal == 0 || vertical == 0)
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//not moving at all
            else
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude < 0.1f) // determines the direction in which the player will run
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
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }

            // Set movement animation based on movement
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                FindObjectOfType<AudioTriggers>().PlayWalk();
                _anim.SetFloat("Walking", Mathf.Abs(Input.GetAxis("Vertical")));
                _anim.SetFloat("Running", 0f);
                transform.Translate(Vector3.forward * Mathf.Abs(Input.GetAxis("Vertical")) * Time.deltaTime);
            }
            else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                FindObjectOfType<AudioTriggers>().PlayWalk();
                _anim.SetFloat("Walking", Mathf.Abs(Input.GetAxis("Horizontal")));
                _anim.SetFloat("Running", 0f);
                transform.Translate(Vector3.forward * Mathf.Abs(Input.GetAxis("Horizontal")) * Time.deltaTime);
            }
            else if (Input.GetAxis("Run") > 0)
            {
                FindObjectOfType<AudioTriggers>().PlayRun();
                _anim.SetFloat("Running", Input.GetAxis("Run"));
                _anim.SetFloat("Walking", 0f);
                transform.Translate(Vector3.forward * Input.GetAxis("Run") * Time.deltaTime * _speedToChange);
            }
            else
            {
                _anim.SetFloat("Running", 0f);
                _anim.SetFloat("Walking", 0f);
            }

            // Activiate based on active shooting method
            _anim.SetBool("Shooting", Input.GetMouseButton(0) && bowHand.activeSelf);

            _anim.SetBool("Hit", Input.GetMouseButton(0) && swordHand.activeSelf);

            // Play swinging sound effect
            if (_anim.GetBool("Hit"))
                FindObjectOfType<AudioTriggers>().PlaySwordSwing();

            // Move camera based on mouse movement
            if (Input.GetMouseButton(0) && (bowHand.activeSelf || swordHand.activeSelf))
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg ;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    // Shoot method
    public void Shoot()
    {
        // Create arrow and apply force
        GameObject arrowGO = Instantiate(arrow, arrowSpawn.transform);
        Rigidbody arrowRB = arrowGO.GetComponent<Rigidbody>();
        arrowRB.velocity = cam.transform.forward * shootForce;
        // Play sound effect
        FindObjectOfType<AudioTriggers>().PlayShootBow();
    }

    public void EnableMovement() { _movementEnabled = true; }
    public void DisableMovement() { _movementEnabled = false; }

    public void IncrementSpeed (float incSpeed)
    {
        _speed += incSpeed;
        _speedToChange += incSpeed;
    }
}
