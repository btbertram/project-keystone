using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unity Game Object script that contains the objects to initialize the puzzle.
/// </summary>

public class puzzleGameObject : MonoBehaviour
{
    public puzzleState currentPuzzleState;
    public gameplayGrid currentGameplayGrid;

    public int puzzleSizeX;
    public int puzzleSizeY;

    // Start is called before the first frame update
    void Start()
    {
        currentGameplayGrid = new gameplayGrid(puzzleSizeX, puzzleSizeY);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            int number = 0;
            int tilenumber = 0;
            foreach (List<gameplayTile> row in currentGameplayGrid.gameplayTiles)
            {
                number++;
                Debug.Log(row + " Number:" + number.ToString());
                foreach (gameplayTile tile in row)
                {
                    tilenumber++;
                    Debug.Log(tile + " TileNumber:" + tilenumber.ToString());
                }

            }
        }
        
    }
}
