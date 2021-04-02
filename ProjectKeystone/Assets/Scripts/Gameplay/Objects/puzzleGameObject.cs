using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A unity Game Object script that contains the objects and variables to initialize and run the puzzle.
/// Acts as a Scene Manager.
/// </summary>

public class PuzzleGameObject : MonoBehaviour
{
    PuzzleState _puzzleState;
    public PuzzleState PObjectPuzzleState { get => _puzzleState; }
    PuzzleGrid _puzzleGrid;
    public PuzzleGrid PObjectPuzzleGrid { get => _puzzleGrid; }
    PuzzleCursor _puzzleCursor;
    public PuzzleCursor PObjectPuzzleCursor { get => _puzzleCursor; }
    PuzzleShapeSearch _puzzleShapeSearch;
    public PuzzleShapeSearch PObjectPuzzleSearchSystem { get => _puzzleShapeSearch; }

    [Range(1,20)] public int puzzleSizeX;
    [Range(1,20)] public int puzzleSizeY;
    public float TimeLimit;
    public float ComboTimer;
    public int ClearQuota;
    //Temp field, pre-gameManager implementation
    [Range(1,2)]public int NumberOfPlayers;
    public float cameraEdgeSpace = 2.5f;

    UIPuzzleObject _uiPuzzleObject;
    public UIPuzzleObject UIPuzzleObjectRef { get => _uiPuzzleObject; }
    UIGameGeneral _uiGameGeneral;
    UIHandler _uiHandler;
    Camera _mainCamera;

    // Start is called before the first frame update
    void Awake()
    {
        _puzzleGrid = new PuzzleGrid(puzzleSizeX, puzzleSizeY);
        _puzzleCursor = new PuzzleCursor(puzzleSizeX, puzzleSizeY);
        _puzzleShapeSearch = new PuzzleShapeSearch();
        _puzzleState = new PuzzleState(NumberOfPlayers, ClearQuota, TimeLimit, ComboTimer);
        _mainCamera = FindObjectOfType<Camera>();
        _uiPuzzleObject = FindObjectOfType<UIPuzzleObject>();
        _uiGameGeneral = FindObjectOfType<UIGameGeneral>();
        _uiHandler = FindObjectOfType<UIHandler>();
    }

    void Start()
    {
        float gridspacing = FindObjectOfType<PuzzleGridPresenter>().gridSpacing;
        //center the camera on the puzzle grid, keep same layer for camera
        _mainCamera.transform.position = new Vector3(((float)puzzleSizeX/2) * gridspacing, ((float)puzzleSizeY/2) * gridspacing, _mainCamera.transform.position.z);
        int newSizeCamera = Mathf.Max(puzzleSizeX, puzzleSizeY);
        _mainCamera.orthographicSize = (((float)newSizeCamera / 2) * gridspacing) + cameraEdgeSpace;
    }

    // Update is called once per frame
    void Update()
    {
        if (PObjectPuzzleState.IsInPlay)
        {
            PObjectPuzzleState.AdjustTimeLeft(-Time.deltaTime);
            for(int x = 0; x < NumberOfPlayers; x++)
            {
                if (PObjectPuzzleState.puzzlePlayers[x].IsComboActive)
                {
                    PObjectPuzzleState.puzzlePlayers[x].AdjustComboTimer(-Time.deltaTime);
                    if (!PObjectPuzzleState.puzzlePlayers[x].IsComboActive)
                    {
                        _uiPuzzleObject.UpdatePlayerCombo(x);
                    }
                }
            }
        }
        else if(!_uiGameGeneral.GeneralGameCanvas.enabled)
        {
            _uiHandler.ToggleCanvasEnabled(_uiGameGeneral.GeneralGameCanvas);
            _uiGameGeneral.SetupEndScreen(PObjectPuzzleState.puzzlePlayers[0].PlayerMatchPoint, ClearQuota, PObjectPuzzleState.puzzlePlayers[0].PlayerScorePoint);
        }
    }

}
