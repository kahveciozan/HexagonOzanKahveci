using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCalculator : MonoBehaviour
{
    private int rowCount, columnCount;
    private GameObject[,] hexagons;
    private GameObject exampleHexagon;

    private GameObject parentObject;

    private bool tekrarKontrolEt;
    public MatchCalculator(int rowCount, int columnCount, GameObject[,] hexagons, GameObject exampleHexagon, GameObject parentObject)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        this.hexagons = hexagons;
        this.exampleHexagon = exampleHexagon;
        this.parentObject = parentObject;

        
    }

    public GameObject[,] CheckMatchHexagons()
    {
        tekrarKontrolEt = true;

        int d1i = -1, d1j = -1, d2i = -1, d2j = -1, d3i = -1, d3j = -1;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount - 1; j++)
            {
                bool isMatch = false;

                if (i % 2 == 0)
                {
                    if (i == 0)
                    {
                        if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i + 1, j].tag)
                        {
                            //Debug.Log("0. Indexte Aynı Renk: " + hexagons[i, j].tag);
                            isMatch = true;

                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i + 1; d3j = j;
                        }
                    }
                    else
                    {
                        if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i + 1, j].tag)
                        {
                            //Debug.Log("Cift Indexte Aynı Renk ve Duz: " + hexagons[i, j].tag);
                            isMatch = true;
                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i + 1; d3j = j;

                        }
                        else if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i - 1, j].tag)
                        {
                            //Debug.Log("Cift Indexte Aynı Renk ve Ters: " + hexagons[i, j].tag);
                            isMatch = true;
                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i - 1; d3j = j;

                        }
                    }
                }
                else
                {
                    if (i == rowCount - 1)
                    {
                        if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i - 1, j + 1].tag)
                        {
                            //Debug.Log("Sonuncu Indexte Aynı Renk: " + hexagons[i, j].tag);
                            isMatch = true;
                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i - 1; d3j = j + 1;
                        }
                    }
                    else
                    {
                        if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i - 1, j + 1].tag)
                        {
                            //Debug.Log("Tek Indexte Aynı Renk ve Ters : " + hexagons[i, j].tag);
                            isMatch = true;
                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i - 1; d3j = j + 1;

                        }
                        else if (hexagons[i, j].tag == hexagons[i, j + 1].tag && hexagons[i, j].tag == hexagons[i + 1, j + 1].tag)
                        {
                            //Debug.Log("Tek Indexte Aynı Renk ve Duz: " + hexagons[i, j].tag);
                            isMatch = true;
                            d1i = i; d1j = j;
                            d2i = i; d2j = j + 1;
                            d3i = i + 1; d3j = j + 1;
                        }
                    }
                }

                if (isMatch)
                {
                    DestroyAndCreate(d1i, d1j, d2i, d2j, d3i, d3j);
                }
                else
                {
                    tekrarKontrolEt = false;
                }
            }
        }

        return hexagons;
    }

    private void DestroyAndCreate(int d1i, int d1j, int d2i, int d2j, int d3i, int d3j)
    {
        Vector2 tempSpawnPoint;

        tempSpawnPoint = hexagons[d1i, d1j].transform.position;
        Destroy(hexagons[d1i, d1j]);
        hexagons[d1i, d1j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);

        tempSpawnPoint = hexagons[d2i, d2j].transform.position;
        Destroy(hexagons[d2i, d2j]);
        hexagons[d2i, d2j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);

        tempSpawnPoint = hexagons[d3i, d3j].transform.position;
        Destroy(hexagons[d3i, d3j]);
        hexagons[d3i, d3j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);
    }

    /*------------------------------------------------------------------------------------------------------------------------------------------------*/

    public GameObject[,] FindAroundMarker(int lastMarkerX, int lastMarkerY, bool lastMarkerRotation)
    {

        string g1, g2, g3;

        if (lastMarkerX % 2 == 0)
        {
            if (lastMarkerRotation)
            {
                //Debug.Log("TRUE LastX: " + lastMarkerX + "LastY: " + lastMarkerY);

                g1 = hexagons[lastMarkerX, lastMarkerY].tag;
                g2 = hexagons[lastMarkerX, lastMarkerY + 1].tag;
                g3 = hexagons[lastMarkerX + 1, lastMarkerY].tag;


                hexagons[lastMarkerX, lastMarkerY].tag = g3;
                hexagons[lastMarkerX, lastMarkerY + 1].tag = g1;
                hexagons[lastMarkerX + 1, lastMarkerY].tag = g2;

                Debug.Log(g1);
                Debug.Log(g2);
                Debug.Log(g3);

                hexagons = CheckMatchHexagons();
            }
            else
            {
                g1 = hexagons[lastMarkerX, lastMarkerY + 1].tag;
                g2 = hexagons[lastMarkerX + 1, lastMarkerY].tag;
                g3 = hexagons[lastMarkerX + 1, lastMarkerY + 1].tag;

                hexagons[lastMarkerX, lastMarkerY + 1].tag = g3;
                hexagons[lastMarkerX + 1, lastMarkerY].tag = g1;
                hexagons[lastMarkerX + 1, lastMarkerY + 1].tag = g2;

                //Debug.Log("FALSE LastX: " + lastMarkerX + "LastY: " + lastMarkerY);

                hexagons = CheckMatchHexagons();
            }

        }
        else
        {
            if (lastMarkerRotation)
            {
                //Debug.Log("TRUE LastX: " + lastMarkerX + "LastY: " + lastMarkerY);
                g1 = hexagons[lastMarkerX, lastMarkerY].tag;
                g2 = hexagons[lastMarkerX, lastMarkerY + 1].tag;
                g3 = hexagons[lastMarkerX + 1, lastMarkerY + 1].tag;

                hexagons[lastMarkerX, lastMarkerY].tag = g3;
                hexagons[lastMarkerX, lastMarkerY + 1].tag = g1;
                hexagons[lastMarkerX + 1, lastMarkerY + 1].tag = g2;

                hexagons = CheckMatchHexagons();

            }
            else
            {
                g1 = hexagons[lastMarkerX, lastMarkerY].tag;
                g2 = hexagons[lastMarkerX + 1, lastMarkerY].tag;
                g3 = hexagons[lastMarkerX + 1, lastMarkerY + 1].tag;

                hexagons[lastMarkerX, lastMarkerY].tag = g3;
                hexagons[lastMarkerX + 1, lastMarkerY].tag = g1;
                hexagons[lastMarkerX + 1, lastMarkerY + 1].tag = g2;

                //Debug.Log("FALSE LastX: " + lastMarkerX + "LastY: " + lastMarkerY);

                hexagons = CheckMatchHexagons();
            }
        }

        return hexagons;
    }

} // Class
