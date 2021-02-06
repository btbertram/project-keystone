using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unity Game Object script that contains the objects to initialize the puzzle.
/// </summary>

public class PuzzleGameObject : MonoBehaviour
{
    public PuzzleState currentPuzzleState;
    public PuzzleGrid currentGameplayGrid;

    public int puzzleSizeX;
    public int puzzleSizeY;

    // Start is called before the first frame update
    void Awake()
    {
        currentGameplayGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int number = 0;
            int tilenumber = 0;
            foreach (List<PuzzleTile> row in currentGameplayGrid.gameplayTiles)
            {
                number++;
                Debug.Log(row + " Number:" + number.ToString());
                foreach (PuzzleTile tile in row)
                {
                    tilenumber++;
                    Debug.Log(tile + " TileID:" + tile.tileid.ToString());
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(FindObjectOfType<PuzzleCursor>().gridPosX.ToString() + "" + FindObjectOfType<PuzzleCursor>().gridPosY.ToString());
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    currentGameplayGrid.ShiftHorizontal(0, 1);
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    currentGameplayGrid.ShiftVertical(0, -1);
        //}

    }
}
