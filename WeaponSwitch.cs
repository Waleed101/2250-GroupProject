using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
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
        _anim = GetComponent<Animator>();
        bowIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 1, false);
        bowHand.SetActive(false);
        bowBack.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("WeaponOut", Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2));
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bowHand.SetActive(false);
            bowBack.SetActive(true);
            swordHand.SetActive(true);
            swordBack.SetActive(false);
            swordIcon.GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
            bowIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bowHand.SetActive(true);
            bowBack.SetActive(false);
            swordHand.SetActive(false);
            swordBack.SetActive(true);
            swordIcon.GetComponent<Image>().CrossFadeAlpha(0.2f, 0.5f, false);
            bowIcon.GetComponent<Image>().CrossFadeAlpha(1f, 0.5f, false);
        }
    }
}
