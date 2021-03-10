using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unity Game Object script that contains the objects to initialize the puzzle.
/// </summary>

public class PuzzleGameObject : MonoBehaviour
{
    public PuzzleState puzzleState;
    public PuzzleGrid gameplayGrid;
    public PuzzleCursor puzzleCursor;
    public PuzzleShapeSearch searchSystem;
    public PuzzleNextMatchQueue puzzleNextMatchQueue;
    public float cameraEdgeSpace = 2.5f;
    Camera mainCamera;

    [Range(1,20)] public int puzzleSizeX;
    [Range(1,20)] public int puzzleSizeY;
    public float TimeLimit;
    public int ClearQuota;

    //Temp field, pre-gameManager implementation
    [Range(1,2)]public int NumberOfPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        gameplayGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
        puzzleCursor = new PuzzleCursor(puzzleSizeX, puzzleSizeY);
        searchSystem = new PuzzleShapeSearch();
        puzzleState = new PuzzleState(NumberOfPlayers, ClearQuota, TimeLimit);
        puzzleNextMatchQueue = new PuzzleNextMatchQueue();
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
                
    }
}
