using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] characterList; //Declare game object array
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");
        characterList = new GameObject[transform.childCount];//creates an array of the amount of childern
        for (int i = 0; i < transform.childCount; i++)//for each child object in the empty game object
        {
            characterList[i] = transform.GetChild(i).gameObject;//fill the array
        }
            foreach (GameObject go in characterList)
            {
                go.SetActive(false);// The characters besides the first in the array are set to false 

            }
            //toggle on the selected character
        if (characterList[index])
        {
            characterList[index].SetActive(true);
        }
       
    }
    public void ToggleLeft()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index--;
        if (index < 0)
            index = characterList.Length - 1;

        //Toggle on the new model
        characterList[index].SetActive(true);
    }
    public void ToggleRight()
    {
        //Toggle off the current model
        characterList[index].SetActive(false);

        index++;
        if (index == characterList.Length)
            index = 0;

        //Toggle on the new model
        characterList[index].SetActive(true);
    }

    public void ChangeScene()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("Main");
    }
}
