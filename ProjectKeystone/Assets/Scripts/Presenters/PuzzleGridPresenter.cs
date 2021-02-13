using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A MonoBehaviour Class that handles and displays the visual components of the PuzzleGrid data class.
/// </summary>
public class PuzzleGridPresenter : MonoBehaviour
{
    public int gridSpacing;
    public float lineThickness;
    public GameObject GridLinePrefab;
    // Start is called before the first frame update
    void Start()
    {
        PuzzleGameObject puzzle = GameObject.FindObjectOfType<PuzzleGameObject>();

        GenerateGridSprites(puzzle.puzzleSizeX, puzzle.puzzleSizeY);
    }

    /// <summary>
    /// Creates a visual representation of the Grid.
    /// </summary>
    public void GenerateGridSprites(int puzzleSizeX, int puzzleSizeY)
    {
        Vector3 pos = Vector3.zero;
        Vector3 scale = Vector3.zero;
        Quaternion quaternion = Quaternion.identity;
        Quaternion rotation = Quaternion.Euler(0, 0, 90);

        Vector3 lineScaleY = new Vector3(lineThickness, puzzleSizeX * gridSpacing, 0);
        Vector3 lineScaleX = new Vector3(lineThickness, puzzleSizeY * gridSpacing, 0);


        for (int y = 0; y < puzzleSizeY+1; y++)
        {
            pos.y = (gridSpacing * y);
            GameObject gameObject = Instantiate(GridLinePrefab, pos, rotation);
            gameObject.transform.localScale = lineScaleY;
        }

        //Clear Vector
        pos = Vector3.zero;
        
        pos.y = (gridSpacing * puzzleSizeY);
        for (int x = 0; x < puzzleSizeX+1; x++)
        {
            pos.x = (gridSpacing * x);
            GameObject gameObject = Instantiate(GridLinePrefab, pos, quaternion);
            gameObject.transform.localScale = lineScaleX;
        }

    }
}
