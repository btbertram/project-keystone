using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Gameplay Object Class that tracks the current state of puzzle in play.
/// </summary>
public class PuzzleState
{
    int _matchPointGoalQuota;
    float _timeLeft;
    int _tilePointScoreValue;

    public List<PuzzlePlayer> puzzlePlayers;

    public PuzzleState(int numberOfPlayers, int matchClearScore, float timeLimit)
    {
        _matchPointGoalQuota = matchClearScore;
        _timeLeft = timeLimit;
        //This will later be modified by difficulty
        _tilePointScoreValue = 100;
        puzzlePlayers = new List<PuzzlePlayer>(1);
        for (int x = 0; x < numberOfPlayers; x++)
        {
            PuzzlePlayer newPlayer = new PuzzlePlayer();
            puzzlePlayers.Add(newPlayer);
        }
    }

    public int CalculateScorePoints(int tilesCleared, int comboCount, int keysUsedCount)
    {
        int result = 0;
        //100t * n * m
        result = ((_tilePointScoreValue * tilesCleared) * comboCount) * keysUsedCount;
        return result;
    }

    public void AddScorePointsToPlayer(int player, int amount)
    {
        puzzlePlayers[player].AddScorePoint(amount);
    }

    //
    //Difficulty modifiers:
    //Allowed MatchTypes
    //Allowed "Keys"
    ///"Key" Appearance weighting
    //Puzzle size? (Move from Puzzle Game Object?)

    //Holds the "shape queue" and "Next Shape"
    

}
