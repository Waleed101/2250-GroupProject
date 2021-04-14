using System.Collections;
using UnityEngine;

public class ActionAfterDelay : MonoBehaviour //Script that allows for a custom time to wait for a function to be called
{
    float delay; //delay variable that is number of seconds that are waited
    System.Action action;
    
    public static ActionAfterDelay Create (float delay, System.Action action)
    {
        ActionAfterDelay aad = new GameObject("ActionAfterDelay").AddComponent<ActionAfterDelay>();
        aad.delay = delay; //delay that is passed over
        aad.action = action; //the action after the delay
        return aad;
    }
     IEnumerator Start()
    {
        yield return new WaitForSeconds(delay); //waits for the number of seconds
        action(); //calls action 
        Destroy(gameObject); 

    }
}
