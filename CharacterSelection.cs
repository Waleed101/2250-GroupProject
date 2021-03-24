using System.Collections;//Libraries
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour //Iterate through different character and then choose a character
{
    private GameObject[] _characterList; //Declare game object array
    [SerializeField] private CharacterInfo _characterRef;//private class CharacterInfo 
    private int _index;//Declare the index variable of the array to allow iteration through the array
    

    private void Start()
    {
        _index = PlayerPrefs.GetInt("CharacterSelected");//index is initialized to the selected character in the character list
        _characterList = new GameObject[transform.childCount];//creates an array of the amount of childern
        
        for (int i = 0; i < transform.childCount; i++)//for each child object in the empty game object
        {
            _characterList[i] = transform.GetChild(i).gameObject;//fill the array
        }
            foreach (GameObject go in _characterList)
            {
                go.SetActive(false);// The characters besides the first in the array are set to false 
                

        }
        //toggle on the selected character
        if (_characterList[_index])
        {
           
            _characterList[_index].SetActive(true);

        }

    }
    public void ToggleLeft()
    {
      

        //Toggle off the current model
        _characterList[_index].SetActive(false);

        _index--;
        if (_index < 0)
        {
            _index = _characterList.Length - 1;//loop back to the max value once the min value is passed on the left
        }

        //Toggle on the new model
        _characterList[_index].SetActive(true);
        //translate the loaded character game object up in the y direction
        //appears as though the character is dropping in
        Vector3 tempPos = _characterList[_index].transform.position;
        tempPos.y=1.2f;
        _characterList[_index].transform.position = tempPos;


        
    }
    public void ToggleRight()
    {

        //Toggle off the current model
        _characterList[_index].SetActive(false);

        _index++;
        if (_index == _characterList.Length)//loop back to the min value once the max value is passed on the right
        {
            _index = 0;
            
        }
        //Toggle on the new model
        _characterList[_index].SetActive(true);
        //translate the loaded character game object up in the y direction
        //appears as though the character is dropping in
        Vector3 tempPos = _characterList[_index].transform.position;
        tempPos.y = 1.2f;
        _characterList[_index].transform.position = tempPos;

    }

    public void ChangeScene() //on click of the confirm button the main scene will be initiated
    {
        
        if (_index == 0)//if the character index is 0 then the character loaded will be the hero
            _characterRef.SetHero();
        else//if the character index is anything other than 0 then the character loaded will be the bandit
            _characterRef.SetBandit();
        PlayerPrefs.SetInt("CharacterSelected", _index);//sets the player that is selected
        SceneManager.LoadScene("Main");//loads the next scene
    }
       
    
        
}
