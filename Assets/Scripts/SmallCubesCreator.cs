using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class SmallCubesCreator : MonoBehaviour
{
    [SerializeField] private int _amountCubesPerEdge;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Transform _parent;
    
    private Player _player;
    private float _bigCubeScale;
    private readonly Dictionary<GameObject, Vector3> _startPosition = new Dictionary<GameObject, Vector3>();

    public void Init(Player player)
    {
        _player = player;
        _player.Setup += CreateSmallCubes;
        _player.Revived += SetupSmallCubes;
    }

    private void OnDestroy()
    {
        _player.Setup -= CreateSmallCubes;
        _player.Revived -= SetupSmallCubes;
    }

    private void CreateSmallCubes()
    {
        _bigCubeScale = transform.localScale.x;

        for (float i = 0; i < _bigCubeScale; i += _bigCubeScale / _amountCubesPerEdge)
        for (float j = 0; j < _bigCubeScale; j += _bigCubeScale / _amountCubesPerEdge)
        for (float k = 0; k < _bigCubeScale; k += _bigCubeScale / _amountCubesPerEdge)

        {
            GameObject smallCube = Instantiate(_cubePrefab, new Vector3(i, j, k), Quaternion.identity, _parent);
            smallCube.transform.localScale /= _amountCubesPerEdge;
            smallCube.transform.localPosition = new Vector3(i, j, k) / _bigCubeScale;
            smallCube.transform.position += GetShiftOnOwnLeftBottomBeforeAngle(smallCube) - GetLeftBottomBeforeAngle();

            SaveStartPosition(smallCube);
        }

        Vector3 GetLeftBottomBeforeAngle()
        {
            return transform.localScale / 2;
        }

        Vector3 GetShiftOnOwnLeftBottomBeforeAngle(GameObject cubic)
        {
            return cubic.transform.localScale / 2 * _bigCubeScale;
        }

        _player.Setup -= CreateSmallCubes;
    }

    private void SaveStartPosition(GameObject cubic)
    {
        _startPosition.Add(cubic, cubic.transform.localPosition);
    }

    private void SetupSmallCubes()
    {
        foreach (var pair in _startPosition)
        {
            pair.Key.transform.localPosition = pair.Value;
            pair.Key.transform.rotation = Quaternion.identity;
        }
    }
}