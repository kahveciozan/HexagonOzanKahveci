using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private GameObject[,] hexagons;
    [SerializeField]private GameObject exampleHexagon;
    private int rowCount, columnCount;

    private Vector2 hexagonSpawnPoint;
    private float firstSpawnX = -2.3f, firstSwapwnY = -4f;

    private GameObject parentObject;

    public GridCreator(GameObject[,] hexagons, GameObject exampleHexagon ,int rowCount,int columnCount,GameObject parentObject)
    {
        this.hexagons = hexagons;
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        this.exampleHexagon = exampleHexagon;
        this.parentObject = parentObject;
    }

    public GameObject[,] CreateHexagons()
    {
        hexagons = new GameObject[rowCount, columnCount];

        float hexagonWeight = exampleHexagon.transform.localScale.x;
        float hexagonHeight = exampleHexagon.transform.localScale.y;
        hexagonSpawnPoint = new Vector2(firstSpawnX, firstSwapwnY);

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                hexagons[i, j] = Instantiate(exampleHexagon, hexagonSpawnPoint, Quaternion.identity, parentObject.transform);

                hexagonSpawnPoint.y += hexagonHeight;

            }

            if (i % 2 == 0)
                hexagonSpawnPoint.y = firstSwapwnY + hexagonHeight / 2;

            else
                hexagonSpawnPoint.y = firstSwapwnY;

            hexagonSpawnPoint.x += hexagonWeight / 2 + hexagonWeight / 4 + 0.05f;
        }

        return hexagons;
    }

}
