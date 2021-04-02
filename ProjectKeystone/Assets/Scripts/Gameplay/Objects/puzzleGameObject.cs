using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unity Game Object script that contains the objects and variables to initialize and run the puzzle.
/// </summary>

public class PuzzleGameObject : MonoBehaviour
{
    public PuzzleState puzzleState;
    public PuzzleGrid gameplayGrid;
    public PuzzleCursor puzzleCursor;
    public PuzzleShapeSearch searchSystem;
    UIPuzzleObject uiPuzzleObject;
    UIGameGeneral uiGameGeneral;
    public float cameraEdgeSpace = 2.5f;
    Camera mainCamera;

    [Range(1,20)] public int puzzleSizeX;
    [Range(1,20)] public int puzzleSizeY;
    public float TimeLimit;
    public float ComboTimer;
    public int ClearQuota;

    //Temp field, pre-gameManager implementation
    [Range(1,2)]public int NumberOfPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        gameplayGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
        puzzleCursor = new PuzzleCursor(puzzleSizeX, puzzleSizeY);
        searchSystem = new PuzzleShapeSearch();
        puzzleState = new PuzzleState(NumberOfPlayers, ClearQuota, TimeLimit, ComboTimer);
        mainCamera = FindObjectOfType<Camera>();
        uiPuzzleObject = FindObjectOfType<UIPuzzleObject>();
        uiGameGeneral = FindObjectOfType<UIGameGeneral>();
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
        if (puzzleState.IsInPlay)
        {
            puzzleState.AdjustTimeLeft(-Time.deltaTime);
            for(int x = 0; x < NumberOfPlayers; x++)
            {
                if (puzzleState.puzzlePlayers[x].IsComboActive)
                {
                    puzzleState.puzzlePlayers[x].AdjustComboTimer(-Time.deltaTime);
                    if (!puzzleState.puzzlePlayers[x].IsComboActive)
                    {
                        uiPuzzleObject.UpdatePlayerCombo(x);
                    }
                }
            }
        }
        else if(!uiGameGeneral.GeneralGameCanvas.enabled)
        {

            uiGameGeneral.ToggleCanvas();
            uiGameGeneral.SetupEndScreen(puzzleState.puzzlePlayers[0].PlayerMatchPoint, ClearQuota, puzzleState.puzzlePlayers[0].PlayerScorePoint);

        }
    }
}
