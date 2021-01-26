using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameplayTile
{

    public EGameplayTileMatchType tileMatchType;
    public bool isTileActive;
    public int tileid;

    public gameplayTile(int id)
    {
        isTileActive = true;
        TileMatchTypeReroll();
        tileid = id;
    }

    void TileMatchTypeReroll()
    {
        tileMatchType = (EGameplayTileMatchType)puzzleState.rand.Next((int)EGameplayTileMatchType.square, (int)EGameplayTileMatchType.special);
    }



}
