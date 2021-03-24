using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Holds reference to the rigidbody and animator 
    private Rigidbody _rb;
    private Animator _anim;

    // Mouse senstivity
    public float mouseSenstivity = 1f;

    public Camera cm;

    // Running speed
    public float speedToChange = 2f;

    // Reference to the arrow prefab and where to spawn
    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.visible = false;
        Screen.lockCursor = true;

        // Get animator reference
        _anim = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        // Rotate player
        transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X"), 0) * mouseSenstivity);

        // Turn on based on what the action currentl is
        _anim.SetBool("Greeting", Input.GetKey(KeyCode.G));
        _anim.SetBool("Hit", Input.GetMouseButton(0));
        _anim.SetBool("Shooting", Input.GetKey(KeyCode.F));
        // If they jump
        if (Input.GetKeyDown(KeyCode.Space))
            _anim.SetTrigger("Jump");

        // Based on how they're walking/running
        if(Input.GetAxis("Vertical") > 0) {
            _anim.SetFloat("Walking", Input.GetAxis("Vertical"));
            _anim.SetFloat("Running", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime);
        } else if (Input.GetAxis("Run") > 0) {
            _anim.SetFloat("Running", Input.GetAxis("Run"));
            _anim.SetFloat("Walking", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Run") * Time.deltaTime * speedToChange);
        } else {
            _anim.SetFloat("Running", 0f);
            _anim.SetFloat("Walking", 0f);
        }
    }

    // Method to shoot
    public void Shoot()
    {
        // Arrow created and force applied
        GameObject arrowGO = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        Rigidbody arrowRB = arrowGO.GetComponent<Rigidbody>();
        arrowRB.velocity = cm.transform.forward * shootForce;
    }
}
