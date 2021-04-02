using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A MonoBehaviour Script for the General UI, such as an end results screen, pause menu, etc for the in game scene.
/// Attach to a relevent GameObject with a Canvas component.
/// </summary>
public class UIGameGeneral : MonoBehaviour
{

    Canvas _canvas;
    public Canvas GeneralGameCanvas { get => _canvas; }
    public GameObject EndScreenPanelGameObject;

    void Awake()
    {
        _canvas = this.gameObject.GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupEndScreen(int matchPoints, int matchQuota, int score)
    {
        var _endPannelRefs = EndScreenPanelGameObject.GetComponent<UIEndScreenReferences>();

        var scoreText = _endPannelRefs.FinalScoreTextGameObject.GetComponent<Text>();
        scoreText.text = "Final Score: " + score.ToString();

        var matchPointsText = _endPannelRefs.MatchQuotaResultTextGameObject.GetComponent<Text>();
        matchPointsText.text = "Puzzle Match Quota: " + matchPoints.ToString() + "/" + matchQuota.ToString();

        var resultsText = _endPannelRefs.ResultTextGameObject.GetComponent<Text>();

        if(matchPoints >= matchQuota)
        {
            resultsText.text = "Win!";
        }
        else
        {
            resultsText.text = "Lose";
        }
    }
    

    public void ToggleCanvas()
    {
        _canvas.enabled = !_canvas.enabled;
    }

}
