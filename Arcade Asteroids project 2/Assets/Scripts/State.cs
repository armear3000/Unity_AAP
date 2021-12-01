using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State : MonoBehaviour
{
    public void play_game(){
        SceneManager.LoadScene("Game");
    }
    

    public void exit_game(){
        Application.Quit();
    }

}
