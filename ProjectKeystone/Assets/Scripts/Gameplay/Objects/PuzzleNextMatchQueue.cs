using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A gameplay object which contains data denoting which shape searches will be used for matching.
/// </summary>
public class PuzzleNextMatchQueue
{
    //Queue length can be tweaked as necessitated by design
    int _queueLengthSize = 5;
    //A normal queue is not used here because we need to be able to display information from all points of the collection, not just the front.
    List<EPuzzleSearchType> _puzzleSearchTypesMatchContainer;
    public List<EPuzzleSearchType> puzzleSearchTypesMatchContainer { get => _puzzleSearchTypesMatchContainer; }

    /// <summary>
    /// PuzzleNextMatchQueue Constructor.
    /// </summary>
    public PuzzleNextMatchQueue()
    {
        _puzzleSearchTypesMatchContainer = new List<EPuzzleSearchType>(_queueLengthSize);
        for(int x = 0; x < _queueLengthSize; x++)
        {
            _puzzleSearchTypesMatchContainer.Add((EPuzzleSearchType)GameManager.rand.Next(0, (int)EPuzzleSearchType.Error));
        }
    }

    /// <summary>
    /// Moves the PuzzleNextMatchQueue list container forward by one, discarding the data at the start of the list, and adding a random new entry at the end.
    /// </summary>
    public void AdvanceQueue()
    {
        _puzzleSearchTypesMatchContainer.RemoveAt(0);
        _puzzleSearchTypesMatchContainer.Add((EPuzzleSearchType)GameManager.rand.Next(0, (int)EPuzzleSearchType.Error));
    }

}
