using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class dedicated to search algorithms for finding patterns in the PuzzleGrid via ISearchables.
/// </summary>
public class PuzzleShapeSearch
{
    //Delarations so that we don't have to redeclare everytime a function is called. Searches are used often.
    ISearchable currentSearchable;
    PuzzleTile currentPuzzleTile;
    PuzzleTile childTile;

    /// <summary>
    /// Finds sets of tiles arranged in a vertical or horizontal line pattern of 4, and marks tiles in those patterns as match ready.
    /// </summary>
    /// <param name="puzzleGrid">The PuzzleGrid object to search.</param>
    /// <param name="vertical">True for a vertical line search, false for a horizontal line search.</param>
    public void LineSearch(PuzzleGrid puzzleGrid, bool vertical)
    {        
        Queue<ISearchable> searchables = PuzzleGridSearchSetup(puzzleGrid.GridPuzzleTiles);

        //Search through whole grid, create match type "chains" of up to 4.
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
                if(currentPuzzleTile.TileMatchType == childTile.TileMatchType)
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

        //Sets chains that match the "vertical line" pattern to be "MatchReady". This part could be own function, allowing line search to also function as a hint mechanism?
        foreach(PuzzleTile puzzleTile in puzzleGrid.GridPuzzleTiles)
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
                        currentPuzzleTile.MatchReady = true;
                        currentPuzzleTile = currentPuzzleTile.parent as PuzzleTile;
                    }
                    //make final parent match ready
                    currentPuzzleTile.MatchReady = true;
                }

            }

        }

    }

    /// <summary>
    /// Finds sets of 4 tiles arranged in a square pattern, and marks tiles in those patterns as match ready.
    /// </summary>
    /// <param name="puzzleGrid"></param>
    public void BoxSearch(PuzzleGrid puzzleGrid)
    {
        Queue<ISearchable> searchables = PuzzleGridSearchSetup(puzzleGrid.GridPuzzleTiles);
        throw new System.NotImplementedException("Box Search not yet implemented.");
    }

    /// <summary>
    /// Takes the puzzle tile list of a puzzle grid and readies it for a search by cleaning the searchables and seeding a queue.
    /// </summary>
    /// <param name="puzzleTiles">The list of PuzzleTiles that represents the PuzzleGrid to make ready for a search.</param>
    /// <returns>A queue of with an ISearchable PuzzleTile, ready for a PuzzleShapeSearch.</returns>
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
    /// <param name="searchables">The list of searchables to reset.</param>
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
    /// <returns>An int representing the depth of the current search.</returns>
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
