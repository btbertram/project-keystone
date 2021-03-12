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
    
    List<PuzzleTile> _gridPuzzleTiles;
    public List<PuzzleTile> GridPuzzleTiles { get => _gridPuzzleTiles; }

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

        SetTileChildren(_gridPuzzleTiles);

    }

    /// <summary>
    /// Interface utility function. Populates the children list in PuzzleTile objects from the ISearchable interface, in reference to their positioning in the PuzzleGrid. 
    /// </summary>
    /// <param name="puzzleTiles"></param>
    void SetTileChildren(List<PuzzleTile> puzzleTiles)
    {
        int count = puzzleTiles.Count - 1;
        int rowOffset;
        int currentTileIndex;

        for(int row = 0; row < _gridsizey; row++)
        {
            rowOffset = _gridsizex * row;

            for(int index = 0; index < _gridsizex; index++)
            {
                currentTileIndex = index + rowOffset;

                if (currentTileIndex + _gridsizex < count)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.North, puzzleTiles[currentTileIndex + _gridsizex]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.North, null);
                }

                if(index + 1 < _gridsizex && currentTileIndex + _gridsizex < count)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.NorthEast, puzzleTiles[currentTileIndex + _gridsizex + 1]);

                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.NorthEast, null);
                }

                if (index + 1 < _gridsizex)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.East, puzzleTiles[currentTileIndex + 1]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.East, null);
                }

                if (index + 1 < _gridsizex && currentTileIndex - _gridsizex >= 0)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.SouthEast, puzzleTiles[currentTileIndex - _gridsizex + 1]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.SouthEast, null);
                }

                if (currentTileIndex - _gridsizex >= 0)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.South, puzzleTiles[currentTileIndex - _gridsizex]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.South, null);
                }

                if (index - 1 >= 0 && currentTileIndex - _gridsizex >= 0)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.SouthWest, puzzleTiles[currentTileIndex - _gridsizex - 1]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.SouthWest, null);
                }

                if (index - 1 >= 0)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.West, puzzleTiles[currentTileIndex - 1]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.West, null);
                }
            
                if (index - 1 >= 0 && currentTileIndex + _gridsizex < count)
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.NorthWest, puzzleTiles[currentTileIndex + _gridsizex - 1]);
                }
                else
                {
                    puzzleTiles[currentTileIndex].AssignChildDirection(EChildGridDirection.NorthWest, null);
                }

            }

        }


    }

    /// <summary>
    /// Shifts PuzzleTile data in a PuzzleTile List "grid" by the amount given, looping around to the "other end" if necessary.
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
            _gridPuzzleTiles[spareIndex].TileMatchType = _gridPuzzleTiles[indexRowOffset + x].TileMatchType;
            //First to target
            _gridPuzzleTiles[indexRowOffset + x].TileMatchType = _gridPuzzleTiles[indexRowOffset].TileMatchType;
            //_gridPuzzleTiles[indexRowOffset + x]._gridPosX = x;
            //spare to first
            _gridPuzzleTiles[indexRowOffset].TileMatchType = _gridPuzzleTiles[spareIndex].TileMatchType;

        } while (x != 0);

    }

    /// <summary>
    /// Shifts PuzzleTile data in a PuzzleTile List to another index in the "grid," based on the amount given, looping around if necessary.
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
            _gridPuzzleTiles[spareIndex].TileMatchType = _gridPuzzleTiles[targetIndex].TileMatchType;
            //First to Target
            _gridPuzzleTiles[targetIndex].TileMatchType = _gridPuzzleTiles[columnIndex].TileMatchType;
            //_gridPuzzleTiles[targetIndex]._gridPosY = y;
            //Spare to First
            _gridPuzzleTiles[columnIndex].TileMatchType = _gridPuzzleTiles[spareIndex].TileMatchType;

        } while (y != 0);
    }

    /// <summary>
    /// Checks the grid for MatchReady PuzzleTiles, randomizing their match type and counting how many PuzzleTiles have been operated on.
    /// </summary>
    /// <returns>An int denoting the number of tiles "cleared."</returns>
    public int ClearMatchReadyTiles()
    {
        int tilesCleared = 0;
        foreach (PuzzleTile tile in _gridPuzzleTiles)
        {
            if (tile.MatchReady)
            {
                tile.TileMatchTypeReroll();
                tilesCleared++;
            }
        }
        return tilesCleared;
    }
}
