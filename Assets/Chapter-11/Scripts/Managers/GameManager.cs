using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SOMD;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Managers")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyManager _enemyManager;

    [Header("UI")]
    [SerializeField] private CanvasGroup _myUI;
    [SerializeField] private CanvasGroup _enemyUI;
    [SerializeField] private Button _turnButton;
    [SerializeField] private Transform _mySpellButtonsParent;
    [SerializeField] private TMPro.TextMeshProUGUI _turnLabel;
    [SerializeField] private GameObject _panelWin;
    [SerializeField] private GameObject _panelGameOver;
    private TMPro.TextMeshProUGUI _winTurnsLabel;
    private TMPro.TextMeshProUGUI _gameOverTurnsLabel;

    [Header("Data")]
    [SerializeField] private IntegerVariable _turnData;

    private int _turn;
    private int _turnPlayerIndex;
    public bool IAmPlaying => _turnPlayerIndex == 0;

    private bool _playing;

    private void Awake()
    {
        instance = this;

        _winTurnsLabel = _panelWin.transform.Find("Turns").GetComponent<TMPro.TextMeshProUGUI>();
        _gameOverTurnsLabel = _panelGameOver.transform.Find("Turns").GetComponent<TMPro.TextMeshProUGUI>();
        _panelWin.SetActive(false);
        _panelGameOver.SetActive(false);

        Spell.LoadAll();

        _playing = true;

        _turn = 0;
        _turnPlayerIndex = 1;
        _turnData.value = _turn;
        SetupTurn();
    }

    #region Game Loop
    public void SetupTurn()
    {
        if (!_playing)
        {
            _myUI.alpha = 0.5f;
            _enemyUI.alpha = 0.5f;
            return;
        }

        _turn++;
        _turnPlayerIndex = 1 - _turnPlayerIndex; // switch turn player
        bool iAmPlaying = _turnPlayerIndex == 0;
        _turnData.value = _turn;

        _enemyManager.StartTurn();
        _playerManager.StartTurn();

        _turnLabel.text = $"Turn {_turn} - {(iAmPlaying ? "Player" : "Enemy")}";

        if (iAmPlaying)
        {
            _myUI.alpha = 1;
            _enemyUI.alpha = 0.5f;
            _turnButton.interactable = true;
            _mySpellButtonsParent.Find("Fireball/Btn").GetComponent<Button>()
                .interactable = _playerManager.CanCast(Spell.LIB["Fireball"]);
            _mySpellButtonsParent.Find("IceShard/Btn").GetComponent<Button>()
                .interactable = _playerManager.CanCast(Spell.LIB["IceShard"]);
            _mySpellButtonsParent.Find("Heal/Btn").GetComponent<Button>()
                .interactable = _playerManager.CanCast(Spell.LIB["Heal"]);
        }
        else
        {
            _myUI.alpha = 0.5f;
            _enemyUI.alpha = 1;
            _turnButton.interactable = false;
            foreach (Transform button in _mySpellButtonsParent)
                button.Find("Btn").GetComponent<Button>().interactable = false;

            // run enemy AI after a few seconds
            StartCoroutine(_RunningEnemyAI());
        }
    }

    public void EndTurn(bool fromUI = false)
    {
        if (fromUI)
        {
            _playerManager.RestoreMana();
            SetupTurn();
        }
        else StartCoroutine(_EndingTurn());
    }

    private IEnumerator _RunningEnemyAI()
    {
        yield return new WaitForSeconds(1f);
        bool passed = _enemyManager.Execute();
        if (passed)
            _enemyManager.RestoreMana();

        EndTurn();
    }

    private IEnumerator _EndingTurn()
    {
        yield return new WaitForSeconds(1.5f);
        SetupTurn();
    }

    public void StopGame()
    {
        _playing = false;
    }

    public void Win()
    {
        _winTurnsLabel.text = $"you won in: <b>{_turn}</b> turn(s)";
        _panelWin.SetActive(true);
    }

    public void GameOver()
    {
        _gameOverTurnsLabel.text = $"rudy won in: <b>{_turn}</b> turn(s)";
        _panelGameOver.SetActive(true);
    }
    #endregion
}
