using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that handles and displays the visual components of the puzzleCursor data class.
/// </summary>
public class PuzzleCursorPresenter : MonoBehaviour
{

    public GameObject PuzzleCursorSpritePrefab;
    private GameObject _currentPuzzleCursorSpite;
    private PuzzleCursor _puzzleCursor;
    private float _currentGridSpacing;
    // Start is called before the first frame update
    void Start()
    {
        _puzzleCursor = GameObject.FindObjectOfType<PuzzleGameObject>().PObjectPuzzleCursor;
        _currentGridSpacing = FindObjectOfType<PuzzleGridPresenter>().gridSpacing;
        GenerateCursorSprite(_currentGridSpacing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generates a sprite for the Cursor using the prefab given in editor.
    /// </summary>
    /// <param name="gridSpacing">The size of the space between the visual lines of the grid.</param>
    public void GenerateCursorSprite(float gridSpacing)
    {
        Vector3 pos = Vector3.zero;
        Quaternion quaternion = Quaternion.identity;

        _currentPuzzleCursorSpite = Instantiate(PuzzleCursorSpritePrefab, pos, quaternion, this.transform);
        _currentPuzzleCursorSpite.transform.localScale = new Vector3(gridSpacing, gridSpacing);
        //.5 added to center the cursor in a grid space
        _currentPuzzleCursorSpite.transform.position = new Vector3((float)_puzzleCursor.GridPosX * gridSpacing + .5f, (float)_puzzleCursor.GridPosY * gridSpacing + .5f, 5);

    }

    /// <summary>
    /// Updates/Changes position of the CursorPresenter GameObject in worldspace based on PuzzleCursor data.
    /// </summary>
    public void PositionSync()
    {
        Vector3 newPos = new Vector3(_puzzleCursor.GridPosX * _currentGridSpacing + .5f, _puzzleCursor.GridPosY * _currentGridSpacing + .5f, 5);
         _currentPuzzleCursorSpite.transform.position = newPos;
    }

    public void Move()
    {
        //Move from currentpos to new pos
    }

    public void ToggleLockAppearance()
    {

    }

}
