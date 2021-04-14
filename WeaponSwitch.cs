using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    // Field variables that descrie the current state of the player and where its weapons are currently held
    private Animator _anim;
    public GameObject bowHand;
    public GameObject bowBack;
    public GameObject swordHand;
    public GameObject swordBack;
    public GameObject swordIcon;
    public GameObject bowIcon;


    // Start is called before the first frame update
    void Start()
    {
        // At the start of the game, the player, by default, has its bow on its back and a sword in its hand
        _anim = GetComponent<Animator>();
        bowIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 1, false);
        bowHand.SetActive(false);
        bowBack.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Keys "1" and "2" enables the user to switch between the sword and bow
        _anim.SetBool("WeaponOut", Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2));
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // If the sword is selected, the bow is moved from the player's hand to its back and brings its sword from its back to tis hand
            bowHand.SetActive(false);
            bowBack.SetActive(true);
            swordHand.SetActive(true);
            swordBack.SetActive(false);
            swordIcon.GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
            bowIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // If the bow is selected, the sword is moved from the player's hand to its back and brings its bow from its back to tis hand
            bowHand.SetActive(true);
            bowBack.SetActive(false);
            swordHand.SetActive(false);
            swordBack.SetActive(true);
            swordIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, false);
            bowIcon.GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
        }
    }
}
