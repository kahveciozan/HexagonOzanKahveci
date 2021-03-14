using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
    //Button OnClick
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }



}
