using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Gameplay Class which represents the player solving the puzzle. 
/// </summary>
public class PuzzlePlayer
{
    int _playerScorePoint = 0;
    public int PlayerScorePoint { get => _playerScorePoint; }
    int _playerMatchPoint = 0;
    public int PlayerMatchPoint { get => _playerMatchPoint; }
    int _currentComboCount = 1;
    public int CurrentComboCount { get => _currentComboCount; }
    int _currentKeyComboCount = 1;
    public int CurrentKeyComboCount { get => _currentKeyComboCount; }    
    // var comboTimer;

    /// <summary>
    /// Alters a player's score by the amount given.
    /// </summary>
    /// <param name="amount">The amount of score to add or subtract.</param>
    public void AddScorePoint(int amount)
    {
        _playerScorePoint += amount;
    }




}
