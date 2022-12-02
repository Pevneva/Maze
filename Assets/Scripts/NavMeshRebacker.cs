using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebacker : MonoBehaviour
{
    [SerializeField] private MazeSpawner _spawner;
    
    void Start()
    {
        _spawner.MazeSpawned += OnMazeSpawned;
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
