using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A MonoBehaviour Class that handles and displays the visual components of the PuzzleTile data class.
/// </summary>
public class PuzzleTilePresenter : MonoBehaviour
{
    PuzzleTile assocatedTile;
    PuzzleTilePresenterManager puzzleTilePresenterManager;
    SpriteRenderer spriteRenderer;
    PuzzleGameObject puzzle;

    void Awake()
    {
        puzzle = GameObject.FindObjectOfType<PuzzleGameObject>();
        PuzzleGridPresenter gridPresenter = FindObjectOfType<PuzzleGridPresenter>();
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        puzzleTilePresenterManager = FindObjectOfType<PuzzleTilePresenterManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Assigns an existing PuzzleTile reference to this TilePresenter.
    /// </summary>
    public void AssignTile(int rowIndex, int columnIndex)
    {
        Debug.Log(puzzle);
        assocatedTile = puzzle.currentGameplayGrid.gameplayTiles[rowIndex][columnIndex];
        //We'll want to call this on a confirmed move. Right now, that's after every move.
    }

    /// <summary>
    /// Moves this TilePresenter's GameObject in world space.
    /// </summary>
    public void Move()
    {
        
    }

    /// <summary>
    /// Has this TilePresenter check its tile reference and update its visuals accordingly.
    /// </summary>
    public void AppearanceSync()
    {
        switch (assocatedTile.tileMatchType)
        {
            case EPuzzleTileMatchType.square:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.square];
                break;
            case EPuzzleTileMatchType.circle:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.circle];
                break;
            case EPuzzleTileMatchType.tri:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.tri];
                break;
            case EPuzzleTileMatchType.hex:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.hex];
                break;
            case EPuzzleTileMatchType.star:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.star];
                break;
            case EPuzzleTileMatchType.diamond:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.diamond];
                break;
            default:
                spriteRenderer.sprite = puzzleTilePresenterManager.sprites[(int)EPuzzleTileMatchType.error];
                break;

        }
    }
}
