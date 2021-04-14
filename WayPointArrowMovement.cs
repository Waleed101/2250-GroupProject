using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Class for managing movement of the waypoint arrow
public class WayPointArrowMovement : MonoBehaviour
{
    // Reference to the arrow and current locaiton
    public GameObject waypointArrow;
    public GameObject wayPointPopUp;
    private Vector3 _waypoint;

    // Turn off waypoint
    private void Start() => waypointArrow.SetActive(false);

    // Update is called once per frame
    void Update()
    {
        // Point arrow, check if reached location
        waypointArrow.transform.LookAt(_waypoint);
        if (Mathf.Abs(gameObject.transform.position.x - _waypoint.x) <= 2 && Mathf.Abs(gameObject.transform.position.z - _waypoint.z) <= 2) {
            waypointArrow.SetActive(false);
        }
    }

    // Set location, turn on arrow and UI
    public void SetWaypoint(Vector3 newWaypoint, string message)
    {
        waypointArrow.SetActive(true);
        wayPointPopUp.SetActive(true);
        wayPointPopUp.transform.GetComponentInChildren<TextMeshProUGUI>().text = message;
        _waypoint = newWaypoint;

        // Close popup after 5 seconds
        StartCoroutine(ExecuteAfterTime(5));
    }

    // Close popup after x time
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        wayPointPopUp.SetActive(false);
    }

}
