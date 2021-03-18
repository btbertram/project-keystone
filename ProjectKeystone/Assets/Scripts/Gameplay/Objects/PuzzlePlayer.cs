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

    int _currentComboCount = 0;
    public int CurrentComboCount { get => _currentComboCount; }

    int _noMovementClearCount = 0;
    public int NoMovementClearCount { get => _noMovementClearCount; }    

    float _comboTimer = 0;
    public float ComboTimer { get => _comboTimer; }
    
    bool _isComboActive = false;
    public bool IsComboActive { get => _isComboActive; } 

    /// <summary>
    /// Alters a player's score by the amount given.
    /// </summary>
    /// <param name="amount">The amount of score to add or subtract.</param>
    public void AddScorePoint(int amount)
    {
        _playerScorePoint += amount;
    }

    /// <summary>
    /// Alters a player's match points by the amount given.
    /// </summary>
    /// <param name="amount">The number of matches to add or subtract.</param>
    public void AddMatchPoint(int amount)
    {
        _playerMatchPoint += amount;
    }

    /// <summary>
    /// Starts a combo for the player if one isn't active, and sets the combo timer to the currently allowed maximum.
    /// </summary>
    /// <param name="maxComboTimer">The max combo time, provided by the puzzle state.</param>
    public void SetMaxComboTime(float maxComboTimer)
    {
        if (!_isComboActive)
        {
            _isComboActive = true;
            _comboTimer = maxComboTimer;
        }
        else
        {
            _comboTimer = 1f + maxComboTimer / _currentComboCount;
        }
    }

    /// <summary>
    /// Adjusts the remaining time left for this player's combo timer.
    /// </summary>
    /// <param name="ammount">The amount to adjust the timer by.</param>
    public void AdjustComboTimer(float amount)
    {
        _comboTimer += amount;
        Debug.Log("Combo Time left: " + _comboTimer.ToString());
        CheckComboTimeOver();
    }

    /// <summary>
    /// Ends the player's current combo if their combo timer has run out.
    /// </summary>
    private void CheckComboTimeOver()
    {
        if(_comboTimer <= 0)
        {
            Debug.Log("Combo Over");
            ResetCombo();
        }
    }

    /// <summary>
    /// Reset's the player's current combo back to 1.
    /// </summary>
    public void ResetCombo()
    {
        _isComboActive = false;
        _currentComboCount = 0;
        _noMovementClearCount = 1;
    }

    /// <summary>
    /// Alters a player's current combo count by the amount given.
    /// </summary>
    /// <param name="amount">The amount of combo count to add or subtract.</param>
    public void AddComboCount(int amount)
    {
        _currentComboCount += amount;
    }

    /// <summary>
    /// Alters a player's current no movement clear count by the amount given.
    /// </summary>
    /// <param name="amount">The amount of no movement clear count to add or subtract.</param>
    public void AddNoMovementClearCount(int amount)
    {
        _noMovementClearCount += amount;
    }


}
