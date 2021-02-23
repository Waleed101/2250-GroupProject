using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public Transform cam;
    public float speed = 1f;
    public float turnSmoothTime = 0.1f;
    public float speedToChange = 2f;
    private float _turnSmoothVelocity;
    private Animator _anim;
    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 20f;
    public Camera mainCam;

    // Update is called once per frame
    private void Start()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
        _anim = GetComponent<Animator>();
    }
    void Update()
    {

        _anim.SetBool("Greeting", Input.GetKey(KeyCode.G));
        _anim.SetBool("Hit", Input.GetMouseButton(0));
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude < 0.1f)
            direction = new Vector3(horizontal, 0f, Input.GetAxisRaw("Run"));
        if (Input.GetKeyDown(KeyCode.Space))
            _anim.SetTrigger("Jump");
        if (direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg ;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //controller.Move(moveDir.normalized * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
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
        _anim.SetBool("Shooting", Input.GetKey(KeyCode.F));
        if (Input.GetKey(KeyCode.F))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg ;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            _anim.SetBool("Shooting", true);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
    public void Shoot()
    {
        print("shoot");
        GameObject arrowGO = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        Rigidbody arrowRB = arrowGO.GetComponent<Rigidbody>();
        arrowRB.velocity = cam.transform.forward * shootForce;


    }
}
