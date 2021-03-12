using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaxagonScript : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    /* --- Hexagon Variables --- */
    [SerializeField] private int rowCount = 8;
    [SerializeField] private int columnCount = 9;
    [SerializeField] private GameObject exampleHexagon;
    private GameObject[,] hexagons;

    /* --- Variable for Markers --- */
    private Vector2[,] rightMarkers, leftMarkers;
    [SerializeField] private GameObject marker;
    private int lastMarkerX, lastMarkerY;
    private bool lastMarkerRotation = true;

    /* --- Touch Control Variables --- */
    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;
    private float playerDirectionX, playerDirectionY;
    private Vector2 touchPoint;

    /* --- Create Helper Class Object Referans --- */
    private GridCreator gridGenarator;
    private MarkerCalculator markerCalculator;
    private MatchCalculator matchCalculator;

    // Start is called before the first frame update
    void Start()
    {
        gridGenarator = new GridCreator(hexagons,exampleHexagon, rowCount,columnCount,parentObject);
        hexagons =  gridGenarator.CreateHexagons();

        markerCalculator = new MarkerCalculator(rightMarkers, leftMarkers, hexagons, rowCount, columnCount);
        rightMarkers = markerCalculator.LocationRightMarkers();
        leftMarkers  =  markerCalculator.LocationLeftMarkers();


        matchCalculator = new MatchCalculator(rowCount, columnCount, hexagons,  exampleHexagon,parentObject);
        hexagons = matchCalculator.CheckMatchHexagons();

    }



    // Update is called once per frame
    void Update()
    {
        TouchControl();
    }

    private void TouchControl()
    {
        bool isTouch = false;

        if (Input.touchCount > 0)
        {
            
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                //touchStartPosition = theTouch.position;
                touchStartPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                if (theTouch.phase == TouchPhase.Ended)
                    isTouch = true;

                touchEndPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                float x = touchEndPosition.x - touchStartPosition.x;
                float y = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
                {
                    touchPoint = touchStartPosition;

                    //If Touch Marker goes that point
                    MarkerMovement();

                }
                else if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    playerDirectionX = x > 0 ? 1 : -1;
                    playerDirectionY = 0;

                    

                    // If Swipe The hexs around marker revolves

                    
                    if (isTouch)
                    {
                        Debug.Log("KAYDIRMA OLDU");
                        isTouch = false;
                        //for (int i = 0; i < 3; i++)
                        //{


                        hexagons = matchCalculator.FindAroundMarker( lastMarkerX,  lastMarkerY,  lastMarkerRotation);
                           

                        //}
                            
                    }
                        
                }
                else
                {
                    playerDirectionY = y > 0 ? 1 : -1;
                    playerDirectionX = 0;
                }
            }
        }
    }

    // Hold Markers Coordinate and Angle
    private void MarkerMovement()
    {
        for(int i = 0; i < rowCount - 1; i++)
        {
            for(int j = 0; j < columnCount - 1; j++)
            {
                float offset = exampleHexagon.transform.localScale.x / 3;

                if(touchPoint.x  < rightMarkers[i, j].x + offset && touchPoint.x > rightMarkers[i,j].x - offset)
                {
                    
                    if (touchPoint.y < rightMarkers[i, j].y + offset && touchPoint.y > rightMarkers[i, j].y - offset)
                    {
                        marker.transform.eulerAngles = new Vector3(0, 0, 0);
                        marker.transform.position = rightMarkers[i, j];

                        lastMarkerX = i; lastMarkerY = j; lastMarkerRotation = true;
                    }
                        
                }
                else if (touchPoint.x < leftMarkers[i, j].x + offset && touchPoint.x > leftMarkers[i, j].x - offset)
                {
                    
                    if (touchPoint.y < leftMarkers[i, j].y + offset && touchPoint.y > leftMarkers[i, j].y - offset)
                    {
                        marker.transform.eulerAngles = new Vector3(0, 0, 180);
                        marker.transform.position = leftMarkers[i, j];

                        lastMarkerX = i; lastMarkerY = j; lastMarkerRotation = false;
                    }
                        
                }




            }
        } 
    }

   

} // Class
