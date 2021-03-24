using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    public float mouseSenstivity = 1f;

    public Camera cm;

    private bool greeting = false;

    private float timeBetweenCheck = 0.5f, timeSince = 0f;

    // Double Tap Running //
    private int _tapCount = 0, _tapsBetween = 2;
    private float _timeToWait = 0.3f, _speedChange = 0f;
    public float speedToChange = 2f;

    public GameObject arrow;
    public Transform arrowSpawn;
    public float shootForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
        anim = GetComponent<Animator>();
    }   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, Input.GetAxis("Mouse X"), 0) * mouseSenstivity);
        anim.SetBool("Greeting", Input.GetKey(KeyCode.G));
        anim.SetBool("Hit", Input.GetMouseButton(0));
        anim.SetBool("Shooting", Input.GetKey(KeyCode.F));
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetTrigger("Jump");
        if(Input.GetAxis("Vertical") > 0) {
            anim.SetFloat("Walking", Input.GetAxis("Vertical"));
            anim.SetFloat("Running", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime);
        } else if (Input.GetAxis("Run") > 0) {
            anim.SetFloat("Running", Input.GetAxis("Run"));
            anim.SetFloat("Walking", 0f);
            transform.Translate(Vector3.forward * Input.GetAxis("Run") * Time.deltaTime * speedToChange);
        } else {
            anim.SetFloat("Running", 0f);
            anim.SetFloat("Walking", 0f);
        }
    }

    public void Shoot()
    {
        print("shoot");
        GameObject arrowGO = Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        Rigidbody arrowRB = arrowGO.GetComponent<Rigidbody>();
        arrowRB.velocity = cm.transform.forward * shootForce;
    }
}
