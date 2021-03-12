using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum that denotes which cardinal direction an ISearchable child is in, in reference to a PuzzleTile placed in a PuzzleGrid.
/// </summary>
public enum EChildGridDirection
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    Error
}
