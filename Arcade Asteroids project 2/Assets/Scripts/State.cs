using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State : MonoBehaviour
{
    public void play_game(){
        SceneManager.LoadScene("Game");
    }
    
    public void play_menu(){
        SceneManager.LoadScene("Menu");
    }

    public void exit_game(){
        Application.Quit();
    }

}
