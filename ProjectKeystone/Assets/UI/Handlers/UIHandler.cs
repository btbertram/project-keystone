using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A MonoBehaviour class for processing Unity Events in regards to UI elements.
/// </summary>
public class UIHandler : MonoBehaviour
{

    public void ToggleCanvasEnabled(Canvas canvas)
    {
        canvas.enabled = !canvas.enabled;
    }

    public void ResetPuzzle(PuzzleGameObject PuzzleObject)
    {
        PuzzleObject.PObjectPuzzleState.ResetPuzzleState(PuzzleObject.TimeLimit);
        for(int x = 0; x<PuzzleObject.NumberOfPlayers; x++)
        {
            PuzzleObject.UIPuzzleObjectRef.UpdatePlayerAllElements(x);
        }
        PuzzleObject.PObjectPuzzleGrid.ResetGrid();
        //This loop is from GridController ApparanceSyncAllTiles(). It is poor preformance code that should be refactored later.
        PuzzleTilePresenter[] test = FindObjectsOfType<PuzzleTilePresenter>();
        foreach (PuzzleTilePresenter presenter in test)
        {
            presenter.AppearanceSync();
        }

        PuzzleObject.PObjectPuzzleCursor.ResetCursorPos();
        FindObjectOfType<PuzzleCursorPresenter>().PositionSync();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
