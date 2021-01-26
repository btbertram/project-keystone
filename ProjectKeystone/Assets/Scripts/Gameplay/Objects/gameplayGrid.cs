using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayGrid
{

    private int _gridsizex;
    private int _gridsizey;

    public List<List<gameplayTile>> gameplayTiles;


    public gameplayGrid(int xsize, int ysize)
    {
        int tilenumber = -1;
        gameplayTiles = new List<List<gameplayTile>>();
        for (int y = 0; y < ysize; y++)
        {
            List<gameplayTile> tileListConstruct = new List<gameplayTile>();
            for(int x = 0; x < xsize; x++)
            {
                tilenumber++;
                gameplayTile tile = new gameplayTile(tilenumber);
                tileListConstruct.Add(tile);
            }
            gameplayTiles.Add(tileListConstruct);
        }

    }
    /// <summary>
    /// Shifts gameplayTile objects in a gameplayTile List by the amount given, looping around to the other end if necessary.
    /// Positive amount implies movement to the right, Negitive amount implies movement to the left.
    /// </summary>
    /// <param name="row">The list to be manipulated. Note: Should be index of grid row instead?</param>
    /// <param name="amount">The number of indexes to move data by.</param>
    public void ShiftHorizontal(List<gameplayTile> row, int amount)
    {

        ///put index 0 into prev
        ///Start at index 0
        ///loop
        ///change index selector by amount
        ///Save value at selected index in next value
        ///Mutate selected index with prev value
        ///set prev value as next value 
        ///end loop after setting index 0

        gameplayTile nextTile;
        gameplayTile prevTile;
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

    public void ShiftVertical()
    {

    }

}
