using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSetup : MonoBehaviour
{
    [SerializeField] private MazeSpawner _maze;
    
    private void Start()
    {
        _maze.MazeSpawned += OnMazeSpawned;
    }

    private void OnMazeSpawned()
    {
        LocateInTopRightPlace();
    }

    private void LocateInTopRightPlace()
    {
        int cellSize = ParamsController.Maze.CELL_SIZE;
        transform.localPosition =
            new Vector3(cellSize * ParamsController.Maze.MAZE_WIDTH - cellSize / 2, transform.localPosition.y, cellSize * ParamsController.Maze.MAZE_HEIGHT - cellSize );
    }
}
