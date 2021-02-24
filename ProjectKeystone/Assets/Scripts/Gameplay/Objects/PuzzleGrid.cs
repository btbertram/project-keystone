using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A data-based gameplay object which represents a grid. Contains a list of PuzzleTiles used for gameplay logic.
/// </summary>
public class PuzzleGrid
{

    private int _gridsizex;
    private int _gridsizey;

    public List<PuzzleTile> _gridPuzzleTiles;

    /// <summary>
    /// PuzzleGrid Constructor.
    /// </summary>
    /// <param name="xsize">Length of the grid row. Any int greater than 0.</param>
    /// <param name="ysize">Hieght of the grid column. Any int greater than 0.</param>
    public PuzzleGrid(int xsize, int ysize)
    {
        if (xsize > 0)
        {
            _gridsizex = xsize;
        }
        else
        {
            _gridsizex = 1;
        }
        if(ysize > 0)
        {
            _gridsizey = ysize;
        }
        else
        {
            _gridsizey = 1;
        }

        int tilenumber = 0;
        _gridPuzzleTiles = new List<PuzzleTile>(xsize * ysize + 1);

        for (int y = 0; y < ysize; y++)
        {
            for (int x = 0; x < xsize; x++)
            {
                PuzzleTile tile = new PuzzleTile(tilenumber,x,y);
                _gridPuzzleTiles.Add(tile);
                tilenumber++;
            }
        }

        //Additional Entry for data manipulation/movement
        PuzzleTile workTile = new PuzzleTile(-1, -1, -1);
        _gridPuzzleTiles.Add(workTile);

    }
    /// <summary>
    /// Shifts gameplayTile objects in a gameplayTile List "grid" by the amount given, looping around to the "other end" if necessary.
    /// Positive amount implies movement to the right, Negitive amount implies movement to the left.
    /// </summary>
    /// <param name="rowIndex">The index corresponding to the row in the grid to have data moved.</param>
    /// <param name="amount">The number of indexes to move data by.</param>
    public void ShiftHorizontal(int rowIndex, int amount)
    {
        Debug.Log(Input.GetAxis(EInputAxis.Horizontal.ToString()) + " Grid Moved Horizontally");

        int spareIndex = _gridPuzzleTiles.Count - 1;
        int indexRowOffset = rowIndex * _gridsizex;
        int x = 0;
        do
        {
            x += amount;
            if (x >= _gridsizex)
            {
                x -= _gridsizex;
            }
            else if (x < 0)
            {
                x += _gridsizex;
            }

            //Target to spare
            _gridPuzzleTiles[spareIndex] = _gridPuzzleTiles[indexRowOffset + x];
            //First to target
            _gridPuzzleTiles[indexRowOffset + x] = _gridPuzzleTiles[indexRowOffset];
            _gridPuzzleTiles[indexRowOffset + x]._gridPosX = x;
            //spare to first
            _gridPuzzleTiles[indexRowOffset] = _gridPuzzleTiles[spareIndex];

        } while (x != 0);

    }

    /// <summary>
    /// Shifts PuzzleTile objects in a PuzzleTile List to another index in the "grid," based on the amount given, looping around if necessary.
    /// Positive amount implies movement "up", while negitive amount implies movement "down".
    /// </summary>
    /// <param name="columnIndex">The index to be used to access tiles in each of the rows.</param>
    /// <param name="amount">The number of indexes to move data by.</param>
    public void ShiftVertical(int columnIndex, int amount)
    {

        Debug.Log(Input.GetAxis(EInputAxis.Vertical.ToString()) + " Grid Moved Vertically");

        int spareIndex = _gridPuzzleTiles.Count - 1;
        int targetIndex = 0;
        int y = 0;
        do
        {
            y += amount;
            if (y >= _gridsizey)
            {
                y -= _gridsizey;
            }
            else if (y < 0)
            {
                y += _gridsizey;
            }

            targetIndex = columnIndex + y * _gridsizex;

            //Target to Spare
            _gridPuzzleTiles[spareIndex] = _gridPuzzleTiles[targetIndex];
            //First to Target
            _gridPuzzleTiles[targetIndex] = _gridPuzzleTiles[columnIndex];
            _gridPuzzleTiles[targetIndex]._gridPosY = y;
            //Spare to First
            _gridPuzzleTiles[columnIndex] = _gridPuzzleTiles[spareIndex];

        } while (y != 0);
    }

}
