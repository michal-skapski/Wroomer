using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private List<Transform> _startingPoints;
    private int _starterPoint;
    [SerializeField] private float _timeToStart = 3f;
    private int _zeroVal = 0;
    private void Start()
    {
        _starterPoint = Random.Range(_zeroVal, _startingPoints.Count);
        //StartCoroutine("Go"); //deactivated
    }
    IEnumerator Go()
    {
        yield return new WaitForSeconds(_timeToStart);
        //AIEnemy.player = _player.transform;
        //AIEnemy.target = _startingPoints[_starterPoint]; //discovered different and more efficient method,
                                                           //but still not 100% it will not be needed in future, so leaving it right here deactivated for now
    }
}
