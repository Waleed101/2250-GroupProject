using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
  public void PlayGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //go to the character customizatiton scene
    }
public void QuitGame()//game quits
    {
        Debug.Log("Quit");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}

