using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A gameplay object that represents which index/list of the grid the player will interact with when controlling it.
/// TODO: Change any getkeys to getbuttons when controls are setup
/// </summary>
public class PuzzleCursor : MonoBehaviour
{

    public int gridPosX { get; set; }
    public int gridPosY { get; set; }
    int puzzleSizeX;
    int puzzleSizeY;
    public bool isLockedIn = false;
    bool isFocused = false;


    // Start is called before the first frame update
    void Start()
    {
        PuzzleGameObject puzzle = FindObjectOfType<PuzzleGameObject>();

        puzzleSizeX = puzzle.puzzleSizeX;
        puzzleSizeY = puzzle.puzzleSizeY;
        gridPosX = 0;
        gridPosY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calls functions which check for cursor movement input if the PuzzleCursor is the current mode of control.
    /// </summary>
    //public void CursorControlCatch()
    //{
    //    if (isFocused)
    //    {
    //        MoveHorizontal();
    //        MoveVertical();
    //    }
    //}



    /// <summary>
    /// Toggles if the cursor is moving itself, or the tiles.
    /// </summary>
    public void CursorSelectToggle()
    {
         isLockedIn = !isLockedIn;        
    }

    /// <summary>
    /// Moves the grid index marker between indexes in a list in the grid.
    /// </summary>
    public void MoveHorizontal(float amount)
    {
        Debug.Log(Input.GetAxis(EInputAxis.Horizontal.ToString()) + "Cursor Moved Horizontally");
        if (amount > 0)
        {
            gridPosX += 1;
            //loop back to start if needed
            if(gridPosX >= puzzleSizeX)
            {
                gridPosX = 0;
            }
        }
        if (amount < 0)
        {
            gridPosX -= 1;
            if(gridPosX < 0)
            {
                //loop to end if needed
                gridPosX = puzzleSizeX - 1;
            }
        }
    }

    /// <summary>
    /// Moves the grid index marker between lists in the grid.
    /// </summary>
    public void MoveVertical(float amount)
    {
        Debug.Log(Input.GetAxis(EInputAxis.Vertical.ToString()) + " Cursor Moved Vertically");
        //Recall that origin is top left, positive means moving a row down.
        //To Move down in the grid, we must add to y.
        if (amount < 0)
        {
            gridPosY += 1;
            if(gridPosY >= puzzleSizeY)
            {
                gridPosY = 0;
            }

        }
        if (amount > 0)
        {
            gridPosY -= 1;
            if(gridPosY < 0)
            {
                gridPosY = puzzleSizeY - 1;
            }
        }
    }



}
