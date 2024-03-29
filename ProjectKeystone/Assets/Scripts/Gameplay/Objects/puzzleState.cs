﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Gameplay Object Class that tracks the current state of puzzle in play.
/// </summary>
public class PuzzleState
{
    bool _isInPlay;
    public bool IsInPlay { get => _isInPlay; }
    
    int _matchPointGoalQuota;
    public int MatchPointGoalQuota { get => _matchPointGoalQuota; }

    float _timeLeft;
    public float TimeLeft { get => _timeLeft; }

    float _maxComboTime;
    public float MaxComboTime { get => _maxComboTime; }

    bool _puzzleMovedSinceLastClear;
    public bool PuzzleMovedSinceLastClear { get => _puzzleMovedSinceLastClear; }

    int _tilePointScoreValue;
    PuzzleNextMatchQueue _puzzleNextMatchQueue;
    public PuzzleNextMatchQueue PuzzleNextMatchQueue { get => _puzzleNextMatchQueue; } 

    public List<PuzzlePlayer> puzzlePlayers;

    /// <summary>
    /// PuzzleState Constructor.
    /// </summary>
    /// <param name="numberOfPlayers">The number of active players for this puzzle.</param>
    /// <param name="matchClearQuota">The number of matches required to clear this puzzle. Argument of 0 or less denotes Endless mode.</param>
    /// <param name="timeLimit">The initial time limit for this puzzle.</param>
    public PuzzleState(int numberOfPlayers, int matchClearQuota, float timeLimit, float startingComboTimer)
    {
        _isInPlay = true;
        _matchPointGoalQuota = matchClearQuota;
        _timeLeft = timeLimit;
        //This will later be modified by difficulty
        _tilePointScoreValue = 100;
        _maxComboTime = startingComboTimer;
        _puzzleMovedSinceLastClear = true;
        puzzlePlayers = new List<PuzzlePlayer>(numberOfPlayers);
        for (int x = 0; x < numberOfPlayers; x++)
        {
            PuzzlePlayer newPlayer = new PuzzlePlayer();
            puzzlePlayers.Add(newPlayer);
        }
        _puzzleNextMatchQueue = new PuzzleNextMatchQueue();
    }

    /// <summary>
    /// Calculates the amount of score points generated by a successful match and subsequent clear.
    /// </summary>
    /// <param name="tilesCleared">The number of matchReady PuzzleTile objects to "clear."</param>
    /// <param name="comboCount">The player's current combo count.</param>
    /// <param name="noMovementCount">The player's number of combo clears without additonal shifiting of the grid..</param>
    /// <returns></returns>
    public int CalculateScorePoints(int tilesCleared, int comboCount, int noMovementCount)
    {
        //Basic score formula: 100t * n * m

        int scoreEarned = tilesCleared * _tilePointScoreValue;

        if(comboCount > 1)
        {
            scoreEarned *= comboCount;

            if(noMovementCount > 0)
            {
                scoreEarned *= (noMovementCount + 1); 
            }
        }

        return scoreEarned;

    }

    /// <summary>
    /// Calculates the amount of match points generated by a successful match and subsequent clear.
    /// </summary>
    /// <param name="matchesCleared"></param>
    /// <param name="comboCount"></param>
    /// <param name="noMovementCount"></param>
    /// <returns></returns>
    public int CalculateMatchPoints(int matchesCleared, int comboCount, int noMovementCount)
    {
        int matchPoints = matchesCleared;

        if (comboCount > 1)
        {
            matchPoints += (comboCount - 1);

            if(noMovementCount > 0)
            {
                matchPoints *= (noMovementCount + 1);
            }
        }

        return matchPoints;
    }

    /// <summary>
    /// Calculates the number of sets of tiles that match the current clear keystone done in a single clear.
    /// </summary>
    /// <param name="tilesCleared">The total number of tiles cleared.</param>
    /// <param name="puzzleSearchType">The shape the search system was looking for.</param>
    /// <returns></returns>
    public int CalculateMatchesCleared(int tilesCleared, EPuzzleSearchType puzzleSearchType)
    {
        switch (puzzleSearchType)
        {
            case EPuzzleSearchType.HorizontalLine:
                return tilesCleared / 4;

            case EPuzzleSearchType.VerticalLine:
                return tilesCleared / 4;

            default:
                return 0;
                
        }
    }

    /// <summary>
    /// Adjusts the score points of the specified player.
    /// </summary>
    /// <param name="player">The player's points to be adjusted.</param>
    /// <param name="amount">The amount of points (positive or negitive) to adjust a player's score points by.</param>
    public void AddScorePointsToPlayer(int player, int amount)
    {
        puzzlePlayers[player].AddScorePoint(amount);
    }

    public void AddMatchPointsToPlayer(int player, int amount)
    {
        puzzlePlayers[player].AddMatchPoint(amount);
    }

    /// <summary>
    /// Sets the puzzle state to say the puzzle has been moved since the last clear.
    /// </summary>
    public void SetPuzzleMoved()
    {
        if (!_puzzleMovedSinceLastClear)
        {
            _puzzleMovedSinceLastClear = true;
        }
    }

    /// <summary>
    /// Sets the puzzle state to say the puzzle has not been moved since the last clear.
    /// </summary>
    public void UnsetPuzzleMoved()
    {
        _puzzleMovedSinceLastClear = false;
    }

    /// <summary>
    /// Adjusts the remaining time limit for the current puzzle.
    /// </summary>
    /// <param name="amount"></param>
    public void AdjustTimeLeft(float amount)
    {
        _timeLeft += amount;
        CheckTimeOver();
    }

    /// <summary>
    /// Checks if the puzzle has run out of time, ending the game.
    /// </summary>
    private void CheckTimeOver()
    {
        if(_timeLeft <= 0)
        {
            //Time Up!
            _isInPlay = false;
            Debug.Log("Game Over! P1 Final Socre: " + puzzlePlayers[0].PlayerScorePoint.ToString() + " :: Final Match Points: " + puzzlePlayers[0].PlayerMatchPoint.ToString());
            if(puzzlePlayers[0].PlayerMatchPoint >= _matchPointGoalQuota)
            {
                Debug.Log(puzzlePlayers[0].PlayerMatchPoint.ToString() + "/" + _matchPointGoalQuota.ToString() + " YOU WIN");
            }
            else
            {
                Debug.Log(puzzlePlayers[0].PlayerMatchPoint.ToString() + "/" + _matchPointGoalQuota.ToString() + " YOU LOSE");
            }


        }
    }


    public void ResetPuzzleState(float timeLimit)
    {
        _timeLeft = timeLimit;
        _puzzleMovedSinceLastClear = true;

        //Flush the match queue
        for (int x = 0; x < _puzzleNextMatchQueue.puzzleSearchTypesMatchContainer.Count; x++)
        {
            _puzzleNextMatchQueue.AdvanceQueue();
        }

        foreach (PuzzlePlayer puzzlePlayer in puzzlePlayers)
        {
            puzzlePlayer.ResetPlayer();
        }

        _isInPlay = true;
    }

    //
    //Difficulty modifiers:
    //Allowed MatchTypes
    //Allowed "Keys"
    ///"Key" Appearance weighting

}
