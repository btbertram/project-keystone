using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuzzleTile
{

    public EGameplayTileMatchType tileMatchType;
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
        tileMatchType = (EGameplayTileMatchType)PuzzleState.rand.Next((int)EGameplayTileMatchType.square, (int)EGameplayTileMatchType.special);
    }



}
