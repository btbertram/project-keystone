using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleTile : ISearchable
{
    EPuzzleTileMatchType _tileMatchType;
    public EPuzzleTileMatchType TileMatchType { get => _tileMatchType; set => _tileMatchType = value; }
    int _tileid;
    int _gridPosX;
    public int GridPosX { get => _gridPosX; }
    int _gridPosY;
    public int GridPosY { get => _gridPosY; }
    bool _matchReady;
    public bool MatchReady { get => _matchReady; set => _matchReady = value; }
    private List<ISearchable> _children;
    private ISearchable _parent;
    private bool _wasSearched;

    public bool wasSearched { get => _wasSearched; set => _wasSearched = value; }
    public List<ISearchable> children { get => _children; set => _children = value; }
    public ISearchable parent { get => _parent; set => _parent = value; }


    /// <summary>
    /// PuzzleTile Constructor.
    /// </summary>
    /// <param name="tileid">An identifier for the tile.</param>
    /// <param name="gridPosX">The PuzzleTile's X pos in the PuzzleGrid.</param>
    /// <param name="gridPosY">The PuzzleTile's Y pos in the PUzzleGrid.</param>
    public PuzzleTile(int tileid, int gridPosX, int gridPosY)
    {
        TileMatchTypeReroll();
        _tileid = tileid;
        _gridPosX = gridPosX;
        _gridPosY = gridPosY;
        children = new List<ISearchable>((int)EChildGridDirection.Error);
        for(int x = 0; x < (int)EChildGridDirection.Error; x++)
        {
            children.Add(null);
        }
    }

    /// <summary>
    /// Randomizes this PuzzleTile's PuzzleTileMatchType, then sets its matchReady to false.
    /// </summary>
    public void TileMatchTypeReroll()
    {
        //This version of Next(minVal, maxVal) does not include the maxValue as a possible returned value, so it has to be 1 higher than the max we want it to roll
        _tileMatchType = (EPuzzleTileMatchType)GameManager.rand.Next(0, (int)EPuzzleTileMatchType.special);
        _matchReady = false;
        Debug.Log("Tile Rerolled! Tile is now:" + _tileMatchType.ToString());
    }

    /// <summary>
    /// ISearchable utility function. Places children in the children list based on the EChildGridDirection given.
    /// </summary>
    /// <param name="gridDirection">The cardinal direction the child is in, in reference to the PuzzleGrid.</param>
    /// <param name="child">The child ISearchable to place in the children list.</param>
    public void AssignChildDirection(EChildGridDirection gridDirection, ISearchable child)
    {
        children[(int)gridDirection] = child;
    }

}
