using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    // Field variables used to represent rigit body of the arrow as well as data of the arrow events
    private Rigidbody _myBody;
    private bool _hitSomething;

    // Future varaibles that will be used to despawn arrows over time in phase 3
    // private float _lifeTimer;
    // private float _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Rigid body reference is assigned
        _myBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // While the arrow is in free trajectory, the arrow will rotate naturally towards its velocity (imitating a realistic projectile)
        if (!_hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(_myBody.velocity);
        }
        // This condition is used to destroy arrows that fell through terrain that may have been caused by unwanted glitches
        if (transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }

    // Once the arrow collides with another object, it calls the Stick() method to Stick() to that object
    private void OnCollisionEnter(Collision collision)
    {
        _hitSomething = true;
        Stick();
    }

    private void Stick()
    {
        // Once the arrow hits an object, it freezes its orientation so that it sticks to the object.
        _myBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
