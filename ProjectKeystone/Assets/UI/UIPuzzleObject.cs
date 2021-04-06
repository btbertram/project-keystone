using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A MonoBehaviour class which controls, formats, and manipulates the In-Game UI and player display.
/// Attach as component to a GameObject with a Canvas component.
/// </summary>
public class UIPuzzleObject : MonoBehaviour
{
    Canvas PuzzleUICanvas;
    public GameObject PlayerHudPrefab;
    public GameObject NextMatchQueuePrefab;
    public GameObject TextPrefab;
    GameObject _timerUI;
    Text _timerText;
    List<GameObject> _playerHudList;
    List<Text> _playerScoreTextList;
    List<Text> _playerComboCountTextList;
    List<Text> _playerComboMultiplierTextList;
    List<Slider> _playerQuotaMeterSliderList;
    List<GameObject> _playerMatchQueueList;
    List<Image[]> _matchQueueImageArraysList;

    PuzzleGameObject _puzzleReference;
    UIManipulationUtility _UIManipulation;
    public Sprite[] MatchShapeSpritesPrefabs;

    void Awake()
    {
        PuzzleUICanvas = this.transform.GetComponent<Canvas>();
        _puzzleReference = FindObjectOfType<PuzzleGameObject>();
        _UIManipulation = new UIManipulationUtility();
        _playerHudList = new List<GameObject>(2);
        _playerScoreTextList = new List<Text>(2);
        _playerComboCountTextList = new List<Text>(2);
        _playerComboMultiplierTextList = new List<Text>(2);
        _playerQuotaMeterSliderList = new List<Slider>(2);
        _playerMatchQueueList = new List<GameObject>(2);
        _matchQueueImageArraysList = new List<Image[]>(2);
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
        if (_puzzleReference.PObjectPuzzleState.IsInPlay)
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
        _timerText.text = _puzzleReference.PObjectPuzzleState.TimeLeft.ToString();
    }

    void UpdateTimer()
    {
        _timerText.text = _puzzleReference.PObjectPuzzleState.TimeLeft.ToString();       
    }

    void PlayerPuzzleHudSetup(int numberOfPlayers)
    {
        for(int x = 0; x < numberOfPlayers; x++)
        {
            _playerHudList.Add(Instantiate(PlayerHudPrefab, PuzzleUICanvas.transform));
            var _hudTransform = _playerHudList[x].GetComponent<RectTransform>();
            _UIManipulation.SetRectTransformSizeToScreenScale(_hudTransform, .15f, .4f);

            var _playerScoreText = _playerHudList[x].GetComponent<UIHudReferences>().ScoreTextGameObject.GetComponent<Text>();
            _playerScoreTextList.Add(_playerScoreText);
            UpdatePlayerScore(x);

            var _playerComboText = _playerHudList[x].GetComponent<UIHudReferences>().ComboCountTextGameObject.GetComponent<Text>();
            _playerComboCountTextList.Add(_playerComboText);
            UpdatePlayerCombo(x);

            var _playerComboMultiplierText = _playerHudList[x].GetComponent<UIHudReferences>().ComboMultiplierTextGameObject.GetComponent<Text>();
            _playerComboMultiplierTextList.Add(_playerComboMultiplierText);
            UpdatePlayerComboMultiplier(x);

            var _playerQuotaMeterSlider = _playerHudList[x].GetComponentInChildren<UIHudReferences>().QuotaMeterGameObject.GetComponent<Slider>();
            _playerQuotaMeterSlider.interactable = false;
            _playerQuotaMeterSliderList.Add(_playerQuotaMeterSlider);
            UpdatePlayerQuotaSliderMeter(x);

            _playerMatchQueueList.Add(Instantiate(NextMatchQueuePrefab, PuzzleUICanvas.transform));
            var _queueTransform = _playerMatchQueueList[x].GetComponent<RectTransform>();
            var _queueVericalLayoutTransform = _playerMatchQueueList[x].GetComponentInChildren<VerticalLayoutGroup>().gameObject.GetComponent<RectTransform>();
            var _queueImages = _playerMatchQueueList[x].GetComponent<UIMatchQueueReferences>().QueueImages;
            _matchQueueImageArraysList.Add(_queueImages);
            _UIManipulation.SetRectTransformSizeToScreenScale(_queueTransform, .1f, .5f);
            //Transforms are applied to each object indiviusally, we must also transform the children of the parent
            _UIManipulation.SetRectTransformSizeToScreenScale(_queueVericalLayoutTransform, .1f, .5f);
            UpdatePlayerMatchQueue(x);

            //Position HUD instances based on player number
            switch (x)
            {
                //P1
                case 0:
                    _UIManipulation.SetAnchors(_hudTransform, EAnchorPos.Top, EAnchorPos.Left);
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, _hudTransform.rect.height);
                    _hudTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, _hudTransform.rect.width);

                    _UIManipulation.SetAnchors(_queueTransform, EAnchorPos.Top, EAnchorPos.Left);
                    _queueTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, _queueTransform.rect.height);
                    _queueTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _hudTransform.rect.width, _queueTransform.rect.width);
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
        _playerScoreTextList[playerIndexNumber].text = _puzzleReference.PObjectPuzzleState.puzzlePlayers[playerIndexNumber].PlayerScorePoint.ToString();        
    }

    public void UpdatePlayerCombo(int playerIndexNumber)
    {
        _playerComboCountTextList[playerIndexNumber].text = _puzzleReference.PObjectPuzzleState.puzzlePlayers[playerIndexNumber].CurrentComboCount.ToString();
    }

    public void UpdatePlayerComboMultiplier(int playerIndexNumber)
    {
        _playerComboMultiplierTextList[playerIndexNumber].text = _puzzleReference.PObjectPuzzleState.puzzlePlayers[playerIndexNumber].NoMovementClearCount.ToString();
    }

    public void UpdatePlayerQuotaSliderMeter(int playerIndexNumber)
    {
        _playerQuotaMeterSliderList[playerIndexNumber].value = (float)_puzzleReference.PObjectPuzzleState.puzzlePlayers[playerIndexNumber].PlayerMatchPoint / (float)_puzzleReference.PObjectPuzzleState.MatchPointGoalQuota;
    }

    public void UpdatePlayerMatchQueue(int playerIndexNumber)
    {
        //Temp assignment to make work, since we don't yet have player based match queues (just a single puzzle based one)
        var test = _puzzleReference.PObjectPuzzleState.PuzzleNextMatchQueue.puzzleSearchTypesMatchContainer;

        for (int x = 0; x < _matchQueueImageArraysList[playerIndexNumber].Length; x++)
        {
            //Set the UI images of the match queue in the same order of the data match queue
            _matchQueueImageArraysList[playerIndexNumber][x].sprite = MatchShapeSpritesPrefabs[(int)test[x]];
        }

    }

    /// <summary>
    /// Calls all the UpdatePlayer UI Element functions to refresh the entire player HUD.
    /// </summary>
    /// <param name="playerIndexNumber"></param>
    public void UpdatePlayerAllElements(int playerIndexNumber)
    {
        UpdatePlayerScore(playerIndexNumber);
        UpdatePlayerCombo(playerIndexNumber);
        UpdatePlayerComboMultiplier(playerIndexNumber);
        UpdatePlayerQuotaSliderMeter(playerIndexNumber);
        UpdatePlayerMatchQueue(playerIndexNumber);
    }

}
