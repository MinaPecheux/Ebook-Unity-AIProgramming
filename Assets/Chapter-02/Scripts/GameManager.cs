using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _rewardPrefab;

    [Header("AI")]
    [SerializeField] private float _executionDelay = 0.5f;
    [SerializeField] private RobotAI _robotAI;
    [SerializeField] private Tilemap _wallsTilemap;

    [Header("UI")]
    [SerializeField] private GameObject _winPanel;

    private int rewardX_, rewardY_;

    void Start()
    {
        _CreateReward();
        StartCoroutine(_Running());

        _winPanel.SetActive(false);
    }

    private IEnumerator _Running()
    {
        WaitForSeconds w = new WaitForSeconds(_executionDelay);
        while (true)
        {
            yield return w;
            (int robotX, int robotY) = _robotAI.Execute();
            if (robotX == rewardX_ && robotY == rewardY_)
            {
                _Win();
                break;
            }
        }
    }

    private void _CreateReward()
    {
        int x = Random.Range(-8, 8);
        int y = Random.Range(-4, 4);
        while (
            _wallsTilemap.HasTile(new Vector3Int(x, y, 0)) || /* tile is a wall */
            (x == 0 && y == 0) /* tile is robot's starting point */
        )
        {
            x = Random.Range(-8, 8);
            y = Random.Range(-4, 4);
        }
        rewardX_ = x;
        rewardY_ = y;

        Vector3 p = new Vector3(rewardX_, rewardY_, -0.5f);
        Instantiate(_rewardPrefab, p, Quaternion.identity);
    }

    private void _Win()
    {
        _winPanel.SetActive(true);
    }
}
