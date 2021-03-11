﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Gameplay GameObject class, used to capture player input and manipulate the Puzzle objects and their presenters.
/// </summary>
public class GridController : MonoBehaviour
{

    PuzzleGrid currentPuzzleGrid;
    PuzzleState currentPuzzleState;
    PuzzleCursor currentPuzzleCursor;
    PuzzleShapeSearch currentSearchSystem;
    PuzzleNextMatchQueue currentPuzzleNextMatchQueue;

    // Start is called before the first frame update
    void Start()
    {
        currentPuzzleGrid = FindObjectOfType<PuzzleGameObject>().gameplayGrid;
        currentPuzzleState = FindObjectOfType<PuzzleGameObject>().puzzleState;
        currentPuzzleCursor = FindObjectOfType<PuzzleGameObject>().puzzleCursor;
        currentSearchSystem = FindObjectOfType<PuzzleGameObject>().searchSystem;
        currentPuzzleNextMatchQueue = currentPuzzleState.PuzzleNextMatchQueue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPuzzleState.isInPlay)
        {
            if (Input.GetButton(EInputAxis.Fire1.ToString()))
            {
                ShiftGrid();
            }
            else
            {
                MoveCursor();
            }

            if (Input.GetButtonDown(EInputAxis.Submit.ToString()))
            {
                MatchTiles();
            }
        }
    }

    /// <summary>
    /// Captures Player input. Commands the PuzzleGrid object to PuzzleTile data around based on cursor position.
    /// </summary>
    private void ShiftGrid()
    {
        if (Input.GetButtonDown(EInputAxis.Horizontal.ToString()))
        {
            float axisVal = Input.GetAxis(EInputAxis.Horizontal.ToString());
            if (axisVal > 0)
            {
                currentPuzzleGrid.ShiftHorizontal(currentPuzzleCursor.gridPosY, 1);
            }
            else if (axisVal < 0)
            {
                currentPuzzleGrid.ShiftHorizontal(currentPuzzleCursor.gridPosY, -1);
            }

        }
        else if (Input.GetButtonDown(EInputAxis.Vertical.ToString()))
        {
            float axisVal = Input.GetAxis(EInputAxis.Vertical.ToString());
            if (axisVal > 0)
            {
                currentPuzzleGrid.ShiftVertical(currentPuzzleCursor.gridPosX, 1);
            }
            else if (axisVal < 0)
            {
                currentPuzzleGrid.ShiftVertical(currentPuzzleCursor.gridPosX, -1);

            }
        }
        AppearanceSyncAllTiles();
    }

    /// <summary>
    /// Captures player input. Commands the PuzzleCursor object to change its position.
    /// </summary>
    private void MoveCursor()
    {
        if (Input.GetButtonDown(EInputAxis.Horizontal.ToString()))
        {
            currentPuzzleCursor.MoveHorizontal(Input.GetAxis(EInputAxis.Horizontal.ToString()));

        }
        if (Input.GetButtonDown(EInputAxis.Vertical.ToString()))
        {
            currentPuzzleCursor.MoveVertical(Input.GetAxis(EInputAxis.Vertical.ToString()));
        }
        FindObjectOfType<PuzzleCursorPresenter>().PositionSync();

    }

    /// <summary>
    /// Captures Player Input. Commands the PuzzleShapeSearch system to search the grid and match tiles together, then clear them if a match has been found.
    /// </summary>
    private void MatchTiles()
    {
        Debug.Log("Submit Hit, Searching for Vertical Line Matches. REMEMBER TO ADD ENUM FOR PLAYERNUMBER");
        CommenceShapeSearch(currentPuzzleNextMatchQueue.puzzleSearchTypesMatchContainer[0]);
        ClearTiles(0);
        Debug.Log("Player Current Score is: " + currentPuzzleState.puzzlePlayers[0].playerScorePoint.ToString());

        string queueOrder = "Current ShapeQueue order is: ";
        foreach (EPuzzleSearchType ePuzzleSearchType in currentPuzzleNextMatchQueue.puzzleSearchTypesMatchContainer)
        {
            queueOrder += ePuzzleSearchType.ToString() + ", ";
        }
        Debug.Log(queueOrder);
    } 

    /// <summary>
    /// Commands the PuzzleShapeSearch system to search the current PuzzleGrid object based on the provided PuzzleSearchType.
    /// </summary>
    /// <param name="puzzleSearchType">An Enum that denotes which pattern in the grid to find.</param>
    private void CommenceShapeSearch(EPuzzleSearchType puzzleSearchType)
    {
        switch (puzzleSearchType)
        {
            case EPuzzleSearchType.VerticalLine:
                currentSearchSystem.LineSearch(currentPuzzleGrid, true);
                break;
            case EPuzzleSearchType.HorizontalLine:
                currentSearchSystem.LineSearch(currentPuzzleGrid, false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Runs matching clear logic on PuzzleTiles after a PuzzleShapeSearch, updating the PuzzleState and PuzzlePlayer information as needed.
    /// </summary>
    /// <param name="playerNumber">The player ID that input the submit command.</param>
    private void ClearTiles(int playerNumber)
    {
        int scorePointsEarned = currentPuzzleState.CalculateScorePoints(currentPuzzleGrid.ClearMatchReadyTiles(), 
            currentPuzzleState.puzzlePlayers[playerNumber].currentComboCount,
            currentPuzzleState.puzzlePlayers[playerNumber].currentKeyComboCount);

        //If points have been earned, a match has occured
        if(scorePointsEarned > 0)
        {
            currentPuzzleState.AddScorePointsToPlayer(playerNumber, scorePointsEarned);
            currentPuzzleNextMatchQueue.AdvanceQueue();
            AppearanceSyncAllTiles();
        }

    }


    /// <summary>
    /// Calls on all active PuzzleTilePresenters in scene to sync thier sprite with their associated tile.
    /// This can probably be optimized to only update PuzzleTilePresenters which need their appearance to be updated.
    /// </summary>
    private void AppearanceSyncAllTiles()
    {
        PuzzleTilePresenter[] test = FindObjectsOfType<PuzzleTilePresenter>();
        foreach (PuzzleTilePresenter presenter in test)
        {
            presenter.AppearanceSync();
        }
    }


}
