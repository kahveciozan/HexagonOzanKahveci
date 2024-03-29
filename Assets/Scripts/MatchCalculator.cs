﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCalculator : MonoBehaviour
{
    private int rowCount, columnCount;
    private GameObject[,] hexagons;
    private GameObject exampleHexagon;
    private GameObject parentObject;

    private bool checkAgain;
    private bool eslestiMi = false;
    private int score = 0;

    
    public MatchCalculator(int rowCount, int columnCount, GameObject[,] hexagons, GameObject exampleHexagon, GameObject parentObject)
    {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        this.hexagons = hexagons;
        this.exampleHexagon = exampleHexagon;
        this.parentObject = parentObject;

        
    }

    // Chaeck Matcing Hexagon, If tree hexs check,  Destroy them 
    public GameObject[,] CheckMatchHexagons()
    {

        do
        {
            checkAgain = false;
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


                    //If  There is any match on Grid, They will explode
                    if (isMatch)
                    {
                        score = score + 15;
                        DestroyAndCreate(d1i, d1j, d2i, d2j, d3i, d3j);

                        checkAgain = true;
                        eslestiMi = true;
                    }

                }
            }
        } while (checkAgain);



            return hexagons;
    }

    public bool isMatch()
    {
        return eslestiMi;
    }


    private void DestroyAndCreate(int d1i, int d1j, int d2i, int d2j, int d3i, int d3j)
    {
        Vector2 tempSpawnPoint;

        tempSpawnPoint = hexagons[d1i, d1j].transform.position;
        Destroy(hexagons[d1i, d1j]);

        hexagons[d1i, d1j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);


        /*----------------------------------------------------------------------------------------*/

        tempSpawnPoint = hexagons[d2i, d2j].transform.position;
        Destroy(hexagons[d2i, d2j]);

        hexagons[d2i, d2j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);

        /*----------------------------------------------------------------------------------------*/

        tempSpawnPoint = hexagons[d3i, d3j].transform.position;
        Destroy(hexagons[d3i, d3j]);

        hexagons[d3i, d3j] = Instantiate(exampleHexagon, tempSpawnPoint, Quaternion.identity, parentObject.transform);

    }


    /*------------------------------------------------------------------------------------------------------------------------------------------------*/

    // Find hexagons around the marker
    public GameObject[,] FindAroundMarker(int lastMarkerX, int lastMarkerY, bool lastMarkerRotation)
    {
        eslestiMi = false;
        int t1i, t1j, t2i, t2j, t3i, t3j;

        if (lastMarkerX % 2 == 0)
        {
            if (lastMarkerRotation)
            {
                t1i = lastMarkerX;      t1j = lastMarkerY;
                t2i = lastMarkerX;      t2j = lastMarkerY + 1;
                t3i = lastMarkerX + 1;  t3j = lastMarkerY;

                ExchangeHexsTag(t1i, t1j, t2i, t2j, t3i, t3j);
 
                hexagons = CheckMatchHexagons();
            }
            else
            {
                t1i = lastMarkerX;       t1j = lastMarkerY +1 ;
                t2i = lastMarkerX +1;    t2j = lastMarkerY;
                t3i = lastMarkerX + 1;   t3j = lastMarkerY + 1;

                ExchangeHexsTag(t1i, t1j, t2i, t2j, t3i, t3j);
                
                hexagons = CheckMatchHexagons();
            }

        }
        else
        {
            if (lastMarkerRotation)
            {
                t1i = lastMarkerX;      t1j = lastMarkerY;
                t2i = lastMarkerX;      t2j = lastMarkerY +1;
                t3i = lastMarkerX + 1;  t3j = lastMarkerY + 1;

                ExchangeHexsTag(t1i, t1j, t2i, t2j, t3i, t3j);

                hexagons = CheckMatchHexagons();

            }
            else
            {
                t1i = lastMarkerX;     t1j = lastMarkerY;
                t2i = lastMarkerX + 1; t2j = lastMarkerY;
                t3i = lastMarkerX + 1; t3j = lastMarkerY + 1;

                ExchangeHexsTag(t1i, t1j, t2i, t2j, t3i, t3j);
                
                hexagons = CheckMatchHexagons();
            }
        }

        return hexagons;
    }

    // Change Hexs tag and Bomb Image each other
    private void ExchangeHexsTag(int t1i,int t1j, int t2i, int t2j,int t3i,int t3j)
    {

        /* -----  Change Bobm Image --------*/
        if (hexagons[t1i, t1j].transform.GetChild(1).gameObject.activeSelf)
        {
            hexagons[t1i, t1j].transform.GetChild(1).gameObject.SetActive(false);
            hexagons[t2i, t2j].transform.GetChild(1).gameObject.SetActive(true);
        }

        else if (hexagons[t2i, t2j].transform.GetChild(1).gameObject.activeSelf )
        {
            hexagons[t2i, t2j].transform.GetChild(1).gameObject.SetActive(false);
            hexagons[t3i, t3j].transform.GetChild(1).gameObject.SetActive(true);
        }

        else if (hexagons[t3i, t3j].transform.GetChild(1).gameObject.activeSelf )
        {
            hexagons[t3i, t3j].transform.GetChild(1).gameObject.SetActive(false);
            hexagons[t1i, t1j].transform.GetChild(1).gameObject.SetActive(true);
        }



        /* -------  Change Hexagon tag -------*/

        string g1, g2, g3;

        g1 = hexagons[t1i, t1j].tag;
        g2 = hexagons[t2i, t2j].tag;
        g3 = hexagons[t3i, t3j].tag;

        hexagons[t1i, t1j].tag = g3;
        hexagons[t2i, t2j].tag = g1;
        hexagons[t3i, t3j].tag = g2;

    }

} // Class
