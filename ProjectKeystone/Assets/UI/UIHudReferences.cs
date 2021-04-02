using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script for the "PlayerPuzzleHud" Prefab object. UI objects/Components that change dynamically during gameplay
/// are exposed and set in the inspector to be able to easily change them at runtime.
/// </summary>
public class UIHudReferences : MonoBehaviour
{
    public GameObject PlayerNumberImageGameObject;
    public GameObject QuotaMeterGameObject;
    public GameObject ScoreTextGameObject;
    public GameObject ComboCountTextGameObject;
    public GameObject ComboMultiplierTextGameObject;
}
