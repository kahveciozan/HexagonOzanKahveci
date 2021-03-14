using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public GameObject settingsPopUp;


    private void Start()
    {
        if (!PlayerPrefs.HasKey("ColorCount"))
        {
            PlayerPrefs.SetInt("ColorCount",5);
        }

        if (!PlayerPrefs.HasKey("RowCount"))
        {
            PlayerPrefs.SetInt("RowCount", 8);
        }

        if (!PlayerPrefs.HasKey("ColumnCount"))
        {
            PlayerPrefs.SetInt("ColumnCount", 9);
        }

        if (!PlayerPrefs.HasKey("HexagonScale"))
        {
            PlayerPrefs.SetFloat("HexagonScale", 0.8f);
        }

        if (!PlayerPrefs.HasKey("MarkerScale"))
        {
            PlayerPrefs.SetFloat("MarkerScale", 1f);
        }


    }




    // Button OnClick
    public void GoToGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Oen Pop Up Settings Menu
    public void OpenPopUp()
    {
        settingsPopUp.SetActive(true);
    }

    //Button OnClick
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("OYUNDAN CIKILDI");
    }



    public void ChooseColorCount(int colorCount)
    {
        PlayerPrefs.SetInt("ColorCount", colorCount);
    }

    public void ChooseGridSize(int rowCount)
    {
        PlayerPrefs.SetInt("RowCount", rowCount);
        PlayerPrefs.SetInt("ColumnCount", rowCount+1);



        switch (rowCount)
        {
            case 6:
                PlayerPrefs.SetFloat("HexagonScale", 1.1f);
                PlayerPrefs.SetFloat("MarkerScale", 1.3f);
                break;
            case 8:
                PlayerPrefs.SetFloat("HexagonScale", 0.8f);
                PlayerPrefs.SetFloat("MarkerScale", 1f);
                break;
            case 10:
                PlayerPrefs.SetFloat("HexagonScale", 0.6f);
                PlayerPrefs.SetFloat("MarkerScale", 0.74f);
                break;

        }





    }


}
