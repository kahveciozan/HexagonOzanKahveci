using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation
{

    private Vector2 temp1, temp2, temp3;
    private GameObject[,]  hexagons;

    private int t1i, t1j, t2i, t2j, t3i, t3j;

    private int lastMarkerX; 
    private int lastMarkerY; 
    private bool lastMarkerRotation;

    public MoveAnimation(GameObject[,] hexagons, int lastMarkerX, int lastMarkerY, bool lastMarkerRotation)
    {
        this.hexagons = hexagons;
        this.lastMarkerX = lastMarkerX;
        this.lastMarkerY = lastMarkerY;
        this.lastMarkerRotation = lastMarkerRotation;

        FindAroundMarker();

        temp1 = hexagons[t1i, t1j].transform.position;
        temp2 = hexagons[t2i, t2j].transform.position;
        temp3 = hexagons[t3i, t3j].transform.position;

    }


    public bool startAnimation(float deltaTime)
    {

        hexagons[t1i, t1j].transform.position = Vector2.Lerp(hexagons[t1i, t1j].transform.position, temp2, deltaTime);
        hexagons[t2i, t2j].transform.position = Vector2.Lerp(hexagons[t2i, t2j].transform.position, temp3, deltaTime);
        hexagons[t3i, t3j].transform.position = Vector2.Lerp(hexagons[t3i, t3j].transform.position, temp1, deltaTime);

        bool isFinish = true;

        if (Mathf.Approximately(hexagons[t1i, t1j].transform.position.x, temp2.x) && Mathf.Approximately(hexagons[t1i, t1j].transform.position.y, temp2.y))
        {
            isFinish = false;

            hexagons[t1i, t1j].transform.position = temp1;
            hexagons[t2i, t2j].transform.position = temp2;
            hexagons[t3i, t3j].transform.position = temp3;

        }
            


        return isFinish;
    }

    public void FindAroundMarker()
    {

        

        if (lastMarkerX % 2 == 0)
        {
            if (lastMarkerRotation)
            {
                t1i = lastMarkerX; t1j = lastMarkerY;
                t2i = lastMarkerX; t2j = lastMarkerY + 1;
                t3i = lastMarkerX + 1; t3j = lastMarkerY;

            }
            else
            {
                t1i = lastMarkerX; t1j = lastMarkerY + 1;
                t2i = lastMarkerX + 1; t2j = lastMarkerY;
                t3i = lastMarkerX + 1; t3j = lastMarkerY + 1;

            }

        }
        else
        {
            if (lastMarkerRotation)
            {
                t1i = lastMarkerX; t1j = lastMarkerY;
                t2i = lastMarkerX; t2j = lastMarkerY + 1;
                t3i = lastMarkerX + 1; t3j = lastMarkerY + 1;


            }
            else
            {
                t1i = lastMarkerX; t1j = lastMarkerY;
                t2i = lastMarkerX + 1; t2j = lastMarkerY;
                t3i = lastMarkerX + 1; t3j = lastMarkerY + 1;

            }
        }

    }







} //Class
