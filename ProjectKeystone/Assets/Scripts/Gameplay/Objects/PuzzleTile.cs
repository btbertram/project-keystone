using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleTile
{

    public EPuzzleTileMatchType tileMatchType;
    public bool isTileActive;
    public int tileid;

    public PuzzleTile(int id)
    {
        isTileActive = true;
        TileMatchTypeReroll();
        tileid = id;
    }

    void TileMatchTypeReroll()
    {
        tileMatchType = (EPuzzleTileMatchType)GameManager.rand.Next((int)EPuzzleTileMatchType.square, (int)EPuzzleTileMatchType.diamond+1);
    }



}
