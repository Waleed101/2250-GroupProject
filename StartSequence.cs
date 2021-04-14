using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the start up of the game
public class StartSequence : MonoBehaviour
{
    public Animator anim;

    // Refernecs to different portions of the animation/start sequence
    private bool _roarAnimFinished = false, _finishedMovement = false, _cameraFinished = false, _drawBridgeDown = false, _openDrawBridge, _renabled = true;
    public GameObject refToMovement, refToNewCameraLoc, sceneCamera, playerCamera, thirdPersonView, monsterDrawBridge, monsterDrawBridgeNew, monsterDrawBridgeOld;
    private Transform[] _finalFightLocations;

    // Position camera starts
    private Vector3 _cameraStartPosition = new Vector3(-161.0212f, -9.865371f, -148.4638f), _cameraStartRotation = new Vector3(-0.585f, -149.98f, 0f);
    private Vector3 _thirdPersonView = new Vector3(-152.5412f, -9.865371f, -148.4638f);
    // Start is called before the first frame update
    void Start()
    {
        // Disable player camera
        playerCamera.GetComponent<Camera>().enabled = false;

        // Get references to the movement of the final enemy
        _finalFightLocations = new Transform[refToMovement.transform.childCount];
        for (int i = 0; i < refToMovement.transform.childCount; i++)
            _finalFightLocations[i] = refToMovement.transform.GetChild(i);

        // Roar
        anim.SetBool("Roar", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Walk out of the gate after doing the first roar
        if (!_roarAnimFinished && Time.time > (anim.GetCurrentAnimatorStateInfo(0).length + 0.5f))
        {
            // Walk towards player, move player camera to appropriate position
            anim.SetBool("Roar", false);
            _roarAnimFinished = true;
            anim.SetBool("Walk", true);
            thirdPersonView.GetComponent<CinemachineFreeLook>().enabled = false;
            print("Start Sequence");
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>().DisableMovement();
            GameObject.FindGameObjectWithTag("Player").transform.eulerAngles = new Vector3(0f, 180f, 0f);
            thirdPersonView.GetComponent<CinemachineFreeLook>().PreviousStateIsValid = false;
            thirdPersonView.transform.position = _thirdPersonView;
            playerCamera.transform.position = _cameraStartPosition;
            playerCamera.transform.eulerAngles = _cameraStartRotation;
            thirdPersonView.GetComponent<CinemachineFreeLook>().m_YAxis.Value = 0.15f;
            thirdPersonView.GetComponent<CinemachineFreeLook>().m_XAxis.Value = 0;
        }

        // When movement is done roar again
        if(_roarAnimFinished && !_finishedMovement)
        {
            transform.position = Vector3.MoveTowards(transform.position, _finalFightLocations[0].position, Time.deltaTime);
            if(GetIfAtLocation(transform.position, _finalFightLocations[0].position)) {
                _finishedMovement = true;
                anim.SetBool("Walk", false);
                anim.SetBool("Roar", true);
            }
        }

        // Close the draw bridge
        if(_finishedMovement && !_drawBridgeDown && !_openDrawBridge)
        {
            monsterDrawBridge.transform.position = Vector3.MoveTowards(monsterDrawBridge.transform.position, monsterDrawBridgeNew.transform.position, 2f * Time.deltaTime);
            if(GetIfAtLocation(monsterDrawBridge.transform.position, monsterDrawBridgeNew.transform.position)) {
                _drawBridgeDown = true;
                anim.SetBool("Roar", false);
            }
        }
        // Move camera towards player
        if(_drawBridgeDown && !_cameraFinished)
        {
            sceneCamera.transform.position = Vector3.MoveTowards(sceneCamera.transform.position, playerCamera.transform.position, 10f * Time.deltaTime);
            sceneCamera.transform.rotation = Quaternion.Slerp(sceneCamera.transform.rotation, playerCamera.transform.rotation, 5f * Time.deltaTime);
            _cameraFinished = GetIfAtLocation(sceneCamera.transform.position, playerCamera.transform.position);
        }
        // Once camera is there, renable player camera/movement
        if(_cameraFinished && _renabled)
        {
            _renabled = false;
            sceneCamera.SetActive(false);
            playerCamera.GetComponent<Camera>().enabled = true;
            thirdPersonView.GetComponent<CinemachineFreeLook>().enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonMovement>().EnableMovement();
            StartCoroutine(EnableAttack());
        }

        // Open draw bridge if needed
        if (_openDrawBridge && _drawBridgeDown)
        {
            monsterDrawBridge.transform.position = Vector3.MoveTowards(monsterDrawBridge.transform.position, monsterDrawBridgeOld.transform.position, 2f * Time.deltaTime); ;
            _drawBridgeDown = !GetIfAtLocation(monsterDrawBridge.transform.position, monsterDrawBridgeOld.transform.position);
        }
        // Disable script to avoid bugs
        else if (_openDrawBridge && !_drawBridgeDown)
            this.enabled = false;
    }

    // Enable enemy attack
    IEnumerator EnableAttack()
    {
        yield return new WaitForSeconds(5f);    
        this.GetComponent<PigBoss>().enabled = true;
        this.enabled = false;
    }

    // Open draw bridge
    public void OpenDrawBridge() { _openDrawBridge = true; }

    // Quick method to get if at current locaiton
    public bool GetIfAtLocation(Vector3 curLoc, Vector3 targetLoc) => (Vector3.Distance(curLoc, targetLoc) < 0.5f);
}
