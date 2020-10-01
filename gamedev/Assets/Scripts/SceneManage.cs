using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
   public void gotogame()
    {
        SceneManager.LoadScene("Firstgame");
    }

    public void gotohome()
    {
        SceneManager.LoadScene("Home");
    }
   
}
