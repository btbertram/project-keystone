﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject Script to mass handle PuzzleTile presenters in the scene.
/// </summary>
public class PuzzleTilePresenterManager : MonoBehaviour
{
    public GameObject puzzleTilePrefab;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleGameObject puzzle = GameObject.FindObjectOfType<PuzzleGameObject>();
        PuzzleGridPresenter gridPresenter = FindObjectOfType<PuzzleGridPresenter>();

        GeneratePuzzleTilePresenters(puzzle.puzzleSizeX, puzzle.puzzleSizeY, gridPresenter.gridSpacing);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Creates a visual representation of all the puzzleTiles in the PuzzleGrid.
    /// </summary>
    /// <param name="puzzleSizeX"></param>
    /// <param name="puzzleSizeY"></param>
    /// <param name="gridSpacing"></param>
    public void GeneratePuzzleTilePresenters(int puzzleSizeX, int puzzleSizeY, float gridSpacing)
    {
        Vector3 pos = Vector3.zero;
        float xyscaling = gridSpacing * .5f;
        //scale spites to half the grid spacing so they fit neatly in a grid space
        Vector3 scale = new Vector3(xyscaling, xyscaling, 0);
        Quaternion quaternion = Quaternion.identity;
        Quaternion rotation = Quaternion.Euler(0, 0, 90);

        for (int y = 0; y < puzzleSizeY; y++)
        {
            pos.y = (gridSpacing * y + xyscaling);

            for (int x = 0; x < puzzleSizeX; x++)
            {
                pos.x = (gridSpacing * x + xyscaling);
                GameObject gameObject = Instantiate(puzzleTilePrefab, pos, quaternion, this.transform);
                gameObject.transform.localScale = scale;
                gameObject.GetComponentInChildren<PuzzleTilePresenter>().AssignTile(x, y);
                gameObject.GetComponentInChildren<PuzzleTilePresenter>().AppearanceSync();
            }
        }

    }

}