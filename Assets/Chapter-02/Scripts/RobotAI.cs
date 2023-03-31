using System.Collections.Generic;
using UnityEngine;

public class RobotAI : MonoBehaviour
{
    private static Vector2[] _OFFSETS = new Vector2[]
    {
        new Vector2( 0,  1), // up
        new Vector2( 1,  0), // right
        new Vector2( 0, -1), // down
        new Vector2(-1,  0), // left
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
        Vector2 d = _OFFSETS[_direction];
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
        for (int i = 0; i < _OFFSETS.Length; i++)
        {
            if (Physics2D.Raycast(transform.position, _OFFSETS[i], 1f).collider != null) {
                possibleDirections.Remove(i);
            }
        }
        _SetDirection(possibleDirections[Random.Range(0, possibleDirections.Count)]);
    }

}
