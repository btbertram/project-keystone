using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A gameplay object that represents which index of the grid the player will interact with when controlling it.
/// </summary>
public class PuzzleCursor
{
    int _gridPosX;
    public int GridPosX { get => _gridPosX; }
    int _gridPosY;
    public int GridPosY { get => _gridPosY; }
     
    int _puzzleSizeX;
    int _puzzleSizeY;
    bool isLockedIn = false;

    /// <summary>
    /// PuzzleCursor Constructor.
    /// </summary>
    /// <param name="puzzleSizeX">The length of the puzzle size in the X Axis.</param>
    /// <param name="puzzleSizeY">The height of the puzzle sixe in the Y Axis.</param>
    public PuzzleCursor(int puzzleSizeX, int puzzleSizeY)
    {
        _puzzleSizeX = puzzleSizeX;
        _puzzleSizeY = puzzleSizeY;
        _gridPosX = 0;
        _gridPosY = 0;
    }

    /// <summary>
    /// Toggles if the cursor is moving itself, or the tiles.
    /// </summary>
    public void CursorSelectToggle()
    {
         isLockedIn = !isLockedIn;        
    }

    /// <summary>
    /// Moves the grid index marker "horizontally" between indexes in a list in the grid.
    /// </summary>
    public void MoveHorizontal(float amount)
    {
        Debug.Log(Input.GetAxis(EInputAxis.Horizontal.ToString()) + "Cursor Moved Horizontally");
        if (amount > 0)
        {
            _gridPosX += 1;
            //loop back to start if needed
            if(_gridPosX >= _puzzleSizeX)
            {
                _gridPosX = 0;
            }
        }
        else if (amount < 0)
        {
            _gridPosX -= 1;
            if(_gridPosX < 0)
            {
                //loop to end if needed
                _gridPosX = _puzzleSizeX - 1;
            }
        }
    }

    /// <summary>
    /// Moves the grid index marker "vertically" between indexes in the grid.
    /// </summary>
    public void MoveVertical(float amount)
    {
        Debug.Log(Input.GetAxis(EInputAxis.Vertical.ToString()) + " Cursor Moved Vertically");
        if (amount > 0)
        {
            _gridPosY += 1;
            //Loop back to start if needed
            if(_gridPosY >= _puzzleSizeY)
            {
                _gridPosY = 0;
            }

        }
        else if (amount < 0)
        {
            _gridPosY -= 1;
            //Loop to end if needed
            if(_gridPosY < 0)
            {
                _gridPosY = _puzzleSizeY - 1;
            }
        }
    }



}
