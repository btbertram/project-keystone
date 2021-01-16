using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gameplayTile : MonoBehaviour
{

    public EGameplayTileMatchType tileMatchType;
    public bool isTileActive;

    // Start is called before the first frame update
    void Start()
    {
        isTileActive = true;
        TileMatchTypeReroll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TileMatchTypeReroll()
    {
        tileMatchType = (EGameplayTileMatchType)puzzleState.rand.Next((int)EGameplayTileMatchType.square, (int)EGameplayTileMatchType.special);
    }



}
