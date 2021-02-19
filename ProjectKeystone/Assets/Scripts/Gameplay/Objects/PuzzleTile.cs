using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleTile
{

    public EPuzzleTileMatchType tileMatchType;
    public bool isTileActive;
    public int _tileid;
    public int _gridPosX;
    public int _gridPosY;

    public PuzzleTile(int tileid, int gridPosX, int gridPosY)
    {
        isTileActive = true;
        TileMatchTypeReroll();
        _tileid = tileid;
        _gridPosX = gridPosX;
        _gridPosY = gridPosY;
    }

    void TileMatchTypeReroll()
    {
        tileMatchType = (EPuzzleTileMatchType)GameManager.rand.Next((int)EPuzzleTileMatchType.square, (int)EPuzzleTileMatchType.diamond+1);
    }



}
