using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleTile : ISearchable
{
    public EPuzzleTileMatchType tileMatchType;
    public int _tileid;
    public int _gridPosX;
    public int _gridPosY;
    public bool matchReady;
    private List<ISearchable> _children;
    private ISearchable _parent;
    private bool _wasSearched;

    public bool wasSearched { get => _wasSearched; set => _wasSearched = value; }
    public List<ISearchable> children { get => _children; set => _children = value; }
    public ISearchable parent { get => _parent; set => _parent = value; }

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

    public void TileMatchTypeReroll()
    {
        //This version of Next(minVal, maxVal) does not include the maxValue as a possible returned value, so it has to be 1 higher than the max we want it to roll
        tileMatchType = (EPuzzleTileMatchType)GameManager.rand.Next((int)EPuzzleTileMatchType.error+1, (int)EPuzzleTileMatchType.special);
        matchReady = false;
        Debug.Log("Tile Rerolled! Tile is now:" + tileMatchType.ToString());
    }

    public void AssignChildDirection(EChildGridDirection gridDirection, ISearchable child)
    {
        children[(int)gridDirection] = child;
    }

}
