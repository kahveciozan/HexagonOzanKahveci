using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayScript : MonoBehaviour
{
    /*  --- UI variables --- */
    public Text scoreText;
    private int score;
    
    /* --- Bomb Variables ---*/
    public GameObject bombImage;
    private int bombControl;
    private int bombCountDown = 10;                                 // < ---
    public GameObject gameOverPopUp;
    private int bombScorNumber = 100;                               // < --- 

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
    private MoveAnimation moveAnimation;

    private bool animationControlStart = false;
    private bool animationControlMoving = false;
    bool isTouchEnd = false;

    private int work3Times = 0;


    private void Awake()
    {
        Hexagon.isBomb = false;
    }
    void Start()
    {
        SetGridSize();

        gridGenarator = new GridCreator(hexagons,exampleHexagon, rowCount,columnCount,parentObject);
        hexagons =  gridGenarator.CreateHexagons();


        markerCalculator = new MarkerCalculator(rightMarkers, leftMarkers, hexagons, rowCount, columnCount);
        rightMarkers = markerCalculator.LocationRightMarkers();
        leftMarkers  =  markerCalculator.LocationLeftMarkers();

        matchCalculator = new MatchCalculator(rowCount, columnCount, hexagons,  exampleHexagon,parentObject);
        hexagons = matchCalculator.CheckMatchHexagons();

        score = 0;
        

    }

    // Set Grid Size and Marker Scale
    private void SetGridSize()
    {
        rowCount = PlayerPrefs.GetInt("RowCount");
        columnCount = PlayerPrefs.GetInt("ColumnCount");

        float markerScale = PlayerPrefs.GetFloat("MarkerScale");
        marker.transform.localScale = new Vector3(markerScale, markerScale, 1f);
    }

 

    // Update is called once per frame
    void Update()
    {
        TouchControl();

        MoveAnimation();
        RealMove();

        if (bombControl > bombScorNumber)
        {
            Hexagon.isBomb = true;
            bombControl = 0;
        }

    }

    // Touch and Swipe Input System
    private void TouchControl()
    {

        if (Input.touchCount > 0 && !animationControlMoving)
        {
            
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                //touchStartPosition = theTouch.position;
                touchStartPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }

            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
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
                    playerDirectionX = x > 0 ? 1 : -1;                                                      // TO DO  (Open Here to Chance the Rotation)
                    playerDirectionY = 0;

                    if (theTouch.phase == TouchPhase.Ended)                                                 // --------------------------------- Kontrol Noktasi
                    {
                        animationControlStart = true;
                        isTouchEnd = true;
                        animationControlMoving = true;
                        work3Times = 0;

                    }
                }
                else
                {
                    playerDirectionY = y > 0 ? 1 : -1;
                    playerDirectionX = 0;

                    // *** TO DO ***  
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


    private void MoveAnimation()
    {
        if (animationControlStart)
        {
            moveAnimation = new MoveAnimation(hexagons, lastMarkerX, lastMarkerY, lastMarkerRotation);
            animationControlStart = false;
        }



        if (animationControlMoving)
        {
            bool a = moveAnimation.startAnimation(Time.deltaTime * 20f);

            if (!a)
                animationControlMoving = false;

        }
    }

    private void RealMove()
    {
        if (isTouchEnd && !animationControlMoving)
        {
            isTouchEnd = false;

            hexagons = matchCalculator.FindAroundMarker(lastMarkerX, lastMarkerY, lastMarkerRotation);

            //score = matchCalculator.Score();

            

            if (work3Times < 2 && !matchCalculator.isMatch())
            {
                work3Times++;
                animationControlStart = true;
                isTouchEnd = true;
                animationControlMoving = true;
            }

            if (matchCalculator.isMatch())
            {

                if (bombCountDown == 0)
                {
                    gameOverPopUp.SetActive(true);
                }

                score = score + 15;
                bombControl = bombControl + 15;

                if (IsThereBomb())
                {
                    bombImage.SetActive(true);
                    bombImage.transform.GetChild(0).gameObject.GetComponent<Text>().text= "" + bombCountDown;
                    bombCountDown--;
                }
                else
                {
                    bombImage.SetActive(false);
                    bombCountDown = 10;
                }


            }

            scoreText.text = "SKOR: " + score;

        }
    }

    // If there is any bonb on Screen method returns true
    private bool IsThereBomb()
    {
        for(int i=0; i<rowCount; i++)
        {
            for(int j =0; j < columnCount; j++)
            {
                if (hexagons[i, j].transform.GetChild(1).gameObject.activeSelf)
                {
                    return true;
                }


            }
        }
        return false;
    }

   

} // Class
