using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class dedicated to search algorithms for finding patterns in the PuzzleGrid via ISearchables.
/// </summary>
public class PuzzleShapeSearch
{
    ISearchable currentSearchable;
    PuzzleTile currentPuzzleTile;
    PuzzleTile childTile;

    /// <summary>
    /// Finds sets of tiles arranged in a vertical or horizontal line pattern of 4, and marks tiles in those patterns as match ready.
    /// </summary>
    /// <param name="puzzleGrid"></param>
    public void LineSearch(PuzzleGrid puzzleGrid, bool vertical)
    {        
        Queue<ISearchable> searchables = PuzzleGridSearchSetup(puzzleGrid._gridPuzzleTiles);

        while(searchables.Count > 0)
        {
            currentSearchable = searchables.Dequeue();
            currentPuzzleTile = currentSearchable as PuzzleTile;

            if (vertical)
            {
                childTile = currentSearchable.children[(int)EChildGridDirection.North] as PuzzleTile;
            }
            else
            {
                childTile = currentSearchable.children[(int)EChildGridDirection.East] as PuzzleTile;
            }

            if (childTile != null)
            {
                if(currentPuzzleTile.tileMatchType == childTile.tileMatchType)
                {
                    //We don't want a chain longer than 4
                    if(SearchDepth(currentPuzzleTile) < 3)
                    {
                        childTile.parent = currentPuzzleTile;
                    }
                }
            }

            foreach(ISearchable child in currentSearchable.children)
            {
                if(child != null)
                {
                    if (!child.wasSearched && !searchables.Contains(child))
                    {
                        searchables.Enqueue(child);
                    }
                }
            }

            currentSearchable.wasSearched = true;
        }

        foreach(PuzzleTile puzzleTile in puzzleGrid._gridPuzzleTiles)
        {
            //If the tile has a parent (meaning tile match)
            if(puzzleTile.parent != null)
            {
                //Check dpeth to see if we have a line of four
                if (SearchDepth(puzzleTile) == 3)
                {
                    currentPuzzleTile = puzzleTile;
                    while(currentPuzzleTile.parent != null)
                    {
                        currentPuzzleTile.matchReady = true;
                        currentPuzzleTile = currentPuzzleTile.parent as PuzzleTile;
                    }
                    //make final parent match ready
                    currentPuzzleTile.matchReady = true;
                }

            }

        }

    }

    public void BoxSearch(PuzzleGrid puzzleGrid)
    {
        Queue<ISearchable> searchables = PuzzleGridSearchSetup(puzzleGrid._gridPuzzleTiles);
    }

    /// <summary>
    /// Takes the puzzle tile list of a puzzle grid and readies it for a search by cleaning the searchables and seeding a queue.
    /// </summary>
    /// <param name="puzzleTiles"></param>
    /// <returns></returns>
    Queue<ISearchable> PuzzleGridSearchSetup(List<PuzzleTile> puzzleTiles)
    {
        ResetPuzzleGridSearched(puzzleTiles);
        Queue<ISearchable> searchQueue = new Queue<ISearchable>();
        searchQueue.Enqueue(puzzleTiles[0]);

        return searchQueue;
    }

    /// <summary>
    /// Resets the searchables in a puzzleGrid.
    /// </summary>
    /// <param name="searchables"></param>
    public void ResetPuzzleGridSearched(List<PuzzleTile> searchables)
    {
        foreach(ISearchable searchable in searchables)
        {
            searchable.wasSearched = false;
            searchable.parent = null;
        }
    }

    /// <summary>
    /// Calculates the search depth in an active search of the current given ISearchable.
    /// </summary>
    /// <param name="searchable">The ISearchable to find the current search depth for.</param>
    /// <returns></returns>
    int SearchDepth(ISearchable searchable)
    {
        ISearchable currentSearchable = searchable;
        int depth = 0;
        while (currentSearchable.parent != null)
        {
            depth += 1;
            currentSearchable = currentSearchable.parent;
        }
        return depth;
    }

}
