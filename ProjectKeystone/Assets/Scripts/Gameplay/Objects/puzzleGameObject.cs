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
    public float cameraEdgeSpace = 2.5f;
    Camera mainCamera;

    [Range(1,20)] public int puzzleSizeX;
    [Range(1,20)] public int puzzleSizeY;

    // Start is called before the first frame update
    void Awake()
    {
        currentGameplayGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
        currentPuzzleCursor = new PuzzleCursor(puzzleSizeX, puzzleSizeY);
        mainCamera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        float gridspacing = FindObjectOfType<PuzzleGridPresenter>().gridSpacing;
        //center the camera on the puzzle grid, keep same layer for camera
        mainCamera.transform.position = new Vector3(((float)puzzleSizeX/2) * gridspacing, ((float)puzzleSizeY/2) * gridspacing, mainCamera.transform.position.z);
        int newSizeCamera = Mathf.Max(puzzleSizeX, puzzleSizeY);
        mainCamera.orthographicSize = (((float)newSizeCamera / 2) * gridspacing) + cameraEdgeSpace;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            int number = 0;
            int tilenumber = 0;

            for(int y = 0; y < puzzleSizeY; y++)
            {
                Debug.Log("Grid Row Number: " + number.ToString());
                number++;

                for(int x = 0; x < puzzleSizeX; x++)
                {
                    PuzzleTile tile = currentGameplayGrid._gridPuzzleTiles[tilenumber];
                    Debug.Log(tile + " TileID:" + tile._tileid.ToString());
                    tilenumber++;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(currentPuzzleCursor.gridPosX.ToString() + "" + currentPuzzleCursor.gridPosY.ToString());
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            int number = 0;
            int tilenumber = 0;

            for (int y = 0; y < puzzleSizeY; y++)
            {
                Debug.Log("Grid Row Number: " + number.ToString());
                number++;

                for (int x = 0; x < puzzleSizeX; x++)
                {
                    PuzzleTile tile = currentGameplayGrid._gridPuzzleTiles[tilenumber];
                    Debug.Log(tile + " TileID:" + tile._tileid.ToString());
                    foreach (PuzzleTile searchable in tile.children)
                    {
                        if(searchable != null)
                        {
                            Debug.Log("Child:" + searchable._tileid);
                        }
                        else
                        {
                            Debug.Log("Child: null");
                        }
                    }

                    tilenumber++;
                }
            }
        }

    }
}
