using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A gameplay object which contains data denoting which shape searches will be used for matching.
/// </summary>
public class PuzzleNextMatchQueue
{
    int _queueLengthSize = 5;
    //A normal queue is not used here because we need to be able to display information from all points of the collection, not just the front.
    List<EPuzzleSearchType> _puzzleSearchTypesMatchContainer;
    public List<EPuzzleSearchType> puzzleSearchTypesMatchContainer { get => _puzzleSearchTypesMatchContainer; }

    public PuzzleNextMatchQueue()
    {
        _puzzleSearchTypesMatchContainer = new List<EPuzzleSearchType>(_queueLengthSize);
        for(int x = 0; x < _queueLengthSize; x++)
        {
            _puzzleSearchTypesMatchContainer.Add((EPuzzleSearchType)GameManager.rand.Next(0, (int)EPuzzleSearchType.Error));
        }
    }

    public void AdvanceQueue()
    {
        _puzzleSearchTypesMatchContainer.RemoveAt(0);
        _puzzleSearchTypesMatchContainer.Add((EPuzzleSearchType)GameManager.rand.Next(0, (int)EPuzzleSearchType.Error));
    }

}
