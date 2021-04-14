using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that controls the camera wakeup
public class WakeupCamera : MonoBehaviour
{
    // Reference to different portions of the game
    private GameObject _cm;
    public GameObject logo;
    public Transform[] waypoints;

    // Scale logo as needed
    public bool logoScaled = false, wakeupEnabled = false;
    private Vector3 _logoScaleTo = new Vector3(4.9f, 1.25f, 1.25f);
    private int _currentWaypoint = 0;

    // Movement of the camera
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get camera if needed, get all waypoints
        _cm = GameObject.FindGameObjectWithTag("MainCamera");
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            waypoints[i] = transform.GetChild(i);
    }

    // Update is called once per frame
    void Update()
    {
        // Move camera through all waypoints
        if (_currentWaypoint != waypoints.Length && wakeupEnabled)
        {
            _cm.transform.position = Vector3.MoveTowards(_cm.transform.position, waypoints[_currentWaypoint].transform.position, movementSpeed * Time.deltaTime);
            _cm.transform.rotation = Quaternion.Slerp(_cm.transform.rotation, waypoints[_currentWaypoint].transform.rotation, float.Parse(waypoints[_currentWaypoint].name) * Time.deltaTime);
            if (GetIfAtLocation(_cm.transform.position, waypoints[_currentWaypoint].transform.position) && GetIfAtLocation(_cm.transform.eulerAngles, waypoints[_currentWaypoint].transform.eulerAngles))
                _currentWaypoint++;

        // Scale up logo
        } else if(!logoScaled && wakeupEnabled) {
            Vector3 newLogoSize = Vector3.MoveTowards(logo.GetComponent<RectTransform>().localScale, _logoScaleTo, Time.deltaTime);
            logo.GetComponent<RectTransform>().localScale = newLogoSize;
            logoScaled = GetIfAtLocation(logo.GetComponent<RectTransform>().localScale, _logoScaleTo);
        // Enable camera rotation with keyboard
        } else if(wakeupEnabled) {
            Vector3 rotation = _cm.transform.localEulerAngles;
            rotation.y += 12f * Time.deltaTime * Input.GetAxis("Horizontal");
            if (rotation.y < 320f && rotation.y > 315f)
                rotation.y = 321f;
            else if (rotation.y < 95f && rotation.y > 90f)
                rotation.y = 89f;
            _cm.transform.localEulerAngles = rotation;
        }
    }

    public bool GetIfAtLocation(Vector3 curLoc, Vector3 targetLoc) => (Vector3.Distance(curLoc, targetLoc) < 1.5f);
    public void Wakeup() { wakeupEnabled = true; }
}
