using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    private static Dictionary<int, Vector2> _DIRECTIONS = new Dictionary<int, Vector2>()
    {
        { 0, new Vector2( 0,  1) }, // up
        { 1, new Vector2( 1,  0) }, // right
        { 2, new Vector2( 0, -1) }, // down
        { 3, new Vector2(-1,  0) }, // left
    };

    private int _direction;
    private int _x, _y;

    private void Awake()
    {
        _SetCoordinates(0, 0);
        _SetDirection(0);
    }

    public (int, int) Execute()
    {
        Vector2 d = _DIRECTIONS[_direction];
        var hit = Physics2D.Raycast(transform.position, d, 1f);
        if (hit.collider == null)
        {
            _SetCoordinates((int) (_x + d.x), (int) (_y + d.y));
            if (Random.Range(0f, 1f) < 0.5f)
                _ChooseRandomDirection();
        }
        else
        {
            bool turnRight = Random.Range(0f, 1f) < 0.5f;
            _SetDirection(turnRight
                ? ((_direction + 1) % 4)
                : ((_direction - 1 + 4) % 4));
        }

        return (_x, _y);
    }

    private void _SetCoordinates(int x, int y)
    {
        _x = x; _y = y;
        transform.position = new Vector3(x, y, -1);
    }

    private void _SetDirection(int direction)
    {
        _direction = direction;
        transform.rotation = Quaternion.Euler(0, 0, -90 * _direction);
    }

    private void _ChooseRandomDirection()
    {
        List<int> possibleDirections = new List<int>() { 0, 1, 2, 3 };
        foreach (var p in _DIRECTIONS)
        {
            if (Physics2D.Raycast(transform.position, p.Value, 1f).collider != null) {
                possibleDirections.Remove(p.Key);
            }
        }
        _SetDirection(possibleDirections[Random.Range(0, possibleDirections.Count)]);
    }

}
