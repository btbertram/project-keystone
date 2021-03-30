using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A MonoBehaviour class which controls, formats, and manipulates the In-Game UI and player display.
/// </summary>
public class UIPuzzleObject : MonoBehaviour
{
    public Canvas PuzzleUICanvas;
    public GameObject PlayerHudPrefab;
    public string PlayerHudScorePanelName;
    public string PlayerHudScoreTextName;
    public string PlayerHudComboPanelName;
    public string PlayerHudComboCountTextName;
    public string PlayerHudComboMultiplierTextName;
    public GameObject TextPrefab;
    GameObject _timerUI;
    Text _timerText;
    List<GameObject> _playerHudList;
    List<Text> _playerScoreTextList;
    List<Text> _playerComboCountTextList;
    List<Text> _playerComboMultiplierTextList;
    List<Slider> _playerQuotaMeterSliderList;
    PuzzleGameObject _puzzleReference;
    Camera _cameraReference;
    UIManipulationUtility _UIManipulation;

    void Awake()
    {
        _puzzleReference = FindObjectOfType<PuzzleGameObject>();
        _cameraReference = FindObjectOfType<Camera>();
        _UIManipulation = new UIManipulationUtility();
        _playerHudList = new List<GameObject>(2);
        _playerScoreTextList = new List<Text>(2);
        _playerComboCountTextList = new List<Text>(2);
        _playerComboMultiplierTextList = new List<Text>(2);
        _playerQuotaMeterSliderList = new List<Slider>(2);
    }

    // Start is called before the first frame update
    void Start()
    {
        TimerSetup();
        PlayerPuzzleHudSetup(_puzzleReference.NumberOfPlayers);
    }

    // Update is called once per frame
    void Update()
    {
        if (_puzzleReference.puzzleState.IsInPlay)
        {
            UpdateTimer();
        }
    }

    void TimerSetup()
    {
        _timerUI = Instantiate(TextPrefab, PuzzleUICanvas.transform);
        var _timerTransform = _timerUI.GetComponent<RectTransform>();
        _UIManipulation.SetRectTransformSizeToScreenScale(_timerTransform, .2f, .1f);
        _UIManipulation.SetAnchors(_timerTransform, EAnchorPos.Top, EAnchorPos.Center);
        _timerTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, _timerTransform.rect.height);
        
        _timerText = _timerUI.GetComponentInChildren<Text>();
        _timerText.text = _puzzleReference.puzzleState.TimeLeft.ToString();
    }

    void UpdateTimer()
    {
        _timerText.text = _puzzleReference.puzzleState.TimeLeft.ToString();       
    }

    void PlayerPuzzleHudSetup(int numberOfPlayers)
    {
        for(int x = 0; x < numberOfPlayers; x++)
        {
            _playerHudList.Add(Instantiate(PlayerHudPrefab, PuzzleUICanvas.transform));
            var _hudTransform = _playerHudList[x].GetComponent<RectTransform>();
            _UIManipulation.SetRectTransformSizeToScreenScale(_hudTransform, .3f, .4f);
            
            var _playerScoreText = _playerHudList[x].transform.Find(PlayerHudScorePanelName+"/"+PlayerHudScoreTextName).GetComponent<Text>();
            if(_playerScoreText == null)
            {
                Debug.LogError("Check UI Puzzle Object Names for misspellings");
            }
            _playerScoreTextList.Add(_playerScoreText);
            UpdatePlayerScore(x);

            var _playerComboText = _playerHudList[x].transform.Find(PlayerHudComboPanelName+"/"+PlayerHudComboCountTextName).GetComponent<Text>();
            if(_playerComboText == null)
            {
                Debug.LogError("Check UI Puzzle Object Names for misspellings");
            }
            _playerComboCountTextList.Add(_playerComboText);
            UpdatePlayerCombo(x);

            var _playerComboMultiplierText = _playerHudList[x].transform.Find(PlayerHudComboPanelName + "/" + PlayerHudComboMultiplierTextName).GetComponent<Text>();
            if(_playerComboMultiplierTextList == null)
            {
                Debug.LogError("Check UI Puzzle Object Names for misspellings");
            }
            _playerComboMultiplierTextList.Add(_playerComboMultiplierText);
            UpdatePlayerComboMultiplier(x);

            var _playerQuotaMeterSlider = _playerHudList[x].GetComponentInChildren<Slider>();
            _playerQuotaMeterSliderList.Add(_playerQuotaMeterSlider);
            UpdatePlayerQuotaSliderMeter(x);


            switch (x)
            {
                //P1
                case 0:
                    _UIManipulation.SetAnchors(_hudTransform, EAnchorPos.Top, EAnchorPos.Left);
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, _hudTransform.rect.height);
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, _hudTransform.rect.width);
                    break;
                //P2
                case 1:
                    _UIManipulation.SetAnchors(_hudTransform, EAnchorPos.Bottom, EAnchorPos.Left);
                    _hudTransform.GetComponent<VerticalLayoutGroup>().reverseArrangement = true;
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, _hudTransform.rect.height);
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, _hudTransform.rect.width);
                    break;

            }

        }

    }

    public void UpdatePlayerScore(int playerIndexNumber)
    {
        _playerScoreTextList[playerIndexNumber].text = _puzzleReference.puzzleState.puzzlePlayers[playerIndexNumber].PlayerScorePoint.ToString();        
    }

    public void UpdatePlayerCombo(int playerIndexNumber)
    {
        _playerComboCountTextList[playerIndexNumber].text = _puzzleReference.puzzleState.puzzlePlayers[playerIndexNumber].CurrentComboCount.ToString();
    }

    public void UpdatePlayerComboMultiplier(int playerIndexNumber)
    {
        _playerComboMultiplierTextList[playerIndexNumber].text = _puzzleReference.puzzleState.puzzlePlayers[playerIndexNumber].NoMovementClearCount.ToString();
    }

    public void UpdatePlayerQuotaSliderMeter(int playerIndexNumber)
    {
        _playerQuotaMeterSliderList[playerIndexNumber].value = (float)_puzzleReference.puzzleState.puzzlePlayers[playerIndexNumber].PlayerMatchPoint / (float)_puzzleReference.puzzleState.MatchPointGoalQuota;
    }

}
