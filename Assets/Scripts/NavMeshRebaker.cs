using System;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebaker : MonoBehaviour
{
    private MazeSpawner _spawner;

    public void Init(MazeSpawner spawner)
    {
        _spawner = spawner;
        _spawner.MazeSpawned += OnMazeSpawned;
    }

    private void OnDestroy()
    {
        _spawner.MazeSpawned -= OnMazeSpawned;
    }

    private void OnMazeSpawned()
    {
        Rebake();
    }

    private void Rebake()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
