using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Gameplay Class which represents the player solving the puzzle. 
/// </summary>
public class PuzzlePlayer
{
    int playerScore = 0;
    int matchesCleared = 0;
    int currentComboCount = 0;
    // var comboTimer;

    /// <summary>
    /// Alters a player's score by the amount given.
    /// </summary>
    /// <param name="amount">The amount of score to add or subtract.</param>
    public void AddScore(int amount)
    {
        playerScore += amount;
    }




}
