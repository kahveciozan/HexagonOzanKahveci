using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCalculator : MonoBehaviour
{
    private Vector2[,] rightMarkers, leftMarkers;
    private GameObject[,] hexagons;
    private int rowCount, columnCount;


    public MarkerCalculator(Vector2[,] rightMarkers, Vector2[,] leftMarkers , GameObject[,] hexagons, int rowCount, int columnCount)
    {
        this.rightMarkers = rightMarkers;
        this.leftMarkers = leftMarkers;
        this.hexagons = hexagons;
        this.rowCount = rowCount;
        this.columnCount = columnCount;
    }



    public Vector2[,] LocationRightMarkers()
    {

        rightMarkers = new Vector2[rowCount - 1, columnCount - 1];

        for (int i = 0; i < rowCount - 1; i++)
        {
            for (int j = 0; j < columnCount - 1; j++)
            {
                float markerSpawnPointY;
                float markerSpawnPointX;

                markerSpawnPointX = (2 * hexagons[i, j].transform.position.x + hexagons[i + 1, j].transform.position.x) / 3;
                markerSpawnPointY = (hexagons[i, j].transform.position.y + hexagons[i, j + 1].transform.position.y) / 2;

                rightMarkers[i, j] = new Vector2(markerSpawnPointX, markerSpawnPointY);

            }
        }

        return rightMarkers;
    }

    public Vector2[,] LocationLeftMarkers()
    {

        leftMarkers = new Vector2[rowCount - 1, columnCount - 1];

        for (int i = 0; i < rowCount - 1; i++)
        {
            for (int j = 0; j < columnCount - 1; j++)
            {
                float markerSpawnPointY;
                float markerSpawnPointX;

                markerSpawnPointX = (hexagons[i, j].transform.position.x + 2 * hexagons[i + 1, j].transform.position.x) / 3;
                markerSpawnPointY = (hexagons[i + 1, j].transform.position.y + hexagons[i + 1, j + 1].transform.position.y) / 2;

                leftMarkers[i, j] = new Vector2(markerSpawnPointX, markerSpawnPointY);
            }
        }

        return leftMarkers;
    }

    

} //Class

