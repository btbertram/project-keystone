using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGrid
{

    private int _gridsizex;
    private int _gridsizey;

    public List<List<PuzzleTile>> gameplayTiles;


    public PuzzleGrid(int xsize, int ysize)
    {
        _gridsizex = xsize;
        _gridsizey = ysize;
        int tilenumber = -1;
        gameplayTiles = new List<List<PuzzleTile>>();
        for (int y = 0; y < ysize; y++)
        {
            List<PuzzleTile> tileListConstruct = new List<PuzzleTile>();
            for(int x = 0; x < xsize; x++)
            {
                tilenumber++;
                PuzzleTile tile = new PuzzleTile(tilenumber);
                tileListConstruct.Add(tile);
            }
            gameplayTiles.Add(tileListConstruct);
        }

    }
    /// <summary>
    /// Shifts gameplayTile objects in a gameplayTile List by the amount given, looping around to the other end if necessary.
    /// Positive amount implies movement to the right, Negitive amount implies movement to the left.
    /// </summary>
    /// <param name="rowIndex">The index corresponding to the row in the grid to have data moved.</param>
    /// <param name="amount">The number of indexes to move data by.</param>
    public void ShiftHorizontal(int rowIndex, int amount)
    {

        List<PuzzleTile> row = gameplayTiles[rowIndex];

        PuzzleTile nextTile;
        PuzzleTile prevTile;
        prevTile = row[0];
        int x = 0;
        do
        {
            x += amount;
            if (x >= row.Count)
            {
                x -= row.Count;
            }
            else if (x < 0)
            {
                x += row.Count;
            }
            nextTile = row[x];
            row[x] = prevTile;
            prevTile = nextTile;

        } while (x != 0);       

    }

    /// <summary>
    /// Shifts PuzzleTile objects in a PuzzleTile List to another list in the "grid," based on the amount given, looping around if necessary.
    /// Positive amount implies movement "up", while negitive amount implies movement "down".
    /// </summary>
    /// <param name="columnIndex">The index to be used to access tiles in each of the rows.</param>
    /// <param name="amount">The number of indexes to move data by.</param>
    public void ShiftVertical(int columnIndex, int amount)
    {
        ///put index 0 into prev
        ///Start at index 0
        ///loop
        ///change index selector by amount
        ///Save value at selected index in next value
        ///Mutate selected index with prev value
        ///set prev value as next value 
        ///end loop after setting index 0

        PuzzleTile nextTile;
        PuzzleTile prevTile;
        //First row, selected column
        prevTile = gameplayTiles[0][columnIndex];
        int y = 0;

        do
        {
            // -= is used due to the grid being starting/reading left/right top/bottom instead of left/right bottom/top (think coorinate systems)
            y -= amount;
            if (y >= gameplayTiles.Count)
            {
                y -= gameplayTiles.Count;
            }
            else if (y < 0)
            {
                y += gameplayTiles.Count;
            }
            nextTile = gameplayTiles[y][columnIndex];
            gameplayTiles[y][columnIndex] = prevTile;
            prevTile = nextTile;

        } while (y != 0);


    }

}
