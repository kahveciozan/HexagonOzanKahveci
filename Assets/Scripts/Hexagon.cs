using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    [SerializeField] private int colorCount = 5;

    private void Awake()
    {
        SetColorCount();

        transform.Rotate(new Vector3(0, 0, 90));


        RandomColor(colorCount);

    }


    private void SetColorCount()
    {
         colorCount =  PlayerPrefs.GetInt("ColorCount");

         float scale = PlayerPrefs.GetFloat("HexagonScale");

         gameObject.transform.localScale = new Vector3(scale, scale, 1);
    }

    public void RandomColor(int colorCount)
    {
        int randomnumber = Random.Range(0, colorCount);

        switch (randomnumber)
        {
            case 1:
                transform.GetComponent<SpriteRenderer>().color = Color.red;

                transform.gameObject.tag = "Red";
                break;
            case 2:
                transform.GetComponent<SpriteRenderer>().color = Color.yellow;
                transform.gameObject.tag = "Yellow";
                break;
            case 3:
                transform.GetComponent<SpriteRenderer>().color = Color.blue;
                transform.gameObject.tag = "Blue";
                break;
            case 4:
                transform.GetComponent<SpriteRenderer>().color = Color.cyan;
                transform.gameObject.tag = "Cyan";
                break;
            case 5:
                transform.GetComponent<SpriteRenderer>().color = Color.green;
                transform.gameObject.tag = "Green";
                break;
            case 6:
                transform.GetComponent<SpriteRenderer>().color = Color.magenta;
                transform.gameObject.tag = "Magenta";
                break;
            case 7:
                transform.GetComponent<SpriteRenderer>().color = Color.grey;
                transform.gameObject.tag = "Grey";
                break;
            case 0:
                transform.GetComponent<SpriteRenderer>().color = new Color32(255,127,0,255);    //Orange
                transform.gameObject.tag = "Orange";
                break;
        }
       
    }


    private void Update()
    {
        string myTag = transform.tag;

        switch (myTag)
        {
            case "Red":
                transform.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case "Yellow":
                transform.GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case "Blue":
                transform.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case "Cyan":
                transform.GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
            case "Green":
                transform.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case "Magenta":
                transform.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case "Grey":
                transform.GetComponent<SpriteRenderer>().color = Color.grey;
                break;
            case "Orange":
                transform.GetComponent<SpriteRenderer>().color = new Color32(255, 127, 0, 255);    //Orange
                break;
        }

    }


}
