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
    public PuzzleCursor currentPuzzleCursor;
    public Camera mainCamera;

    int defaultCameraSize = 5;

    [Range(1,20)] public int puzzleSizeX;
    [Range(1,20)] public int puzzleSizeY;

    // Start is called before the first frame update
    void Awake()
    {
        currentGameplayGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
        mainCamera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        float gridspacing = FindObjectOfType<PuzzleGridPresenter>().gridSpacing;
        //center the camera on the puzzle grid, keep same layer for camera
        mainCamera.transform.position = new Vector3(((float)puzzleSizeX/2) * gridspacing, ((float)puzzleSizeY/2) * gridspacing, mainCamera.transform.position.z);
        int newSizeCamera = Mathf.Max(puzzleSizeX, puzzleSizeY);
        mainCamera.orthographicSize = (((float)newSizeCamera / 2) * gridspacing) + 2.5f;
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
            Debug.Log(currentPuzzleCursor.gridPosX.ToString() + "" + currentPuzzleCursor.gridPosY.ToString());
        }

    }
}
