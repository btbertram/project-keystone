using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehaviour Class which keeps track of the overall game state.
/// </summary>

public sealed class GameManager : MonoBehaviour
{
    public static GameObject StaticGameStateInstance;
    public static System.Random rand = new System.Random();

    
    // Used as constructor here. Singleton design.
    public void Awake()
    {
        if (StaticGameStateInstance == null)
        {
            StaticGameStateInstance = this.gameObject;
            DontDestroyOnLoad(StaticGameStateInstance);
        }
        else if (StaticGameStateInstance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Function to end game execution, in editor or standalone.
    /// </summary>
    public void UnityApplicationQuit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
