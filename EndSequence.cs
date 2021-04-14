using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;


// Manages the final end sequence of the game
public class EndSequence : MonoBehaviour
{
    // References to needed info for movement
    private bool _endSequenceStarted = false, _fTime = true, _triggerBed = false;
    public GameObject thirdPersonView, eyeBlinker;

    private GameObject _player;
    private Animator _anim;

    // Get references 
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _anim = _player.GetComponent<Animator>(); 
    }

    void Update()
    {
        // Start sequence when player gets close to the wife after rescuing
        if(Vector3.Distance(_player.transform.position, this.transform.position) <= 3f)
        {
            if(_fTime)
            {
                _player.GetComponent<ThirdPersonMovement>().DisableMovement();
                _anim.SetFloat("Running", 0f);
                _anim.SetFloat("Walking", 0f);
                StartCoroutine(CollapseSequence());
                _fTime = false;
            }
        }
    }

    // Method to run die sequence animation and then destroy enemy
    IEnumerator CollapseSequence()
    {
        // Run death animation until its done
        _anim.SetBool("Collapse", true);
        yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length - 1f);
        eyeBlinker.SetActive(true);
    }
}
