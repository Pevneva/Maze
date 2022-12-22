using UnityEngine;
using UnityEngine.Events;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Transform _parent;

    public event UnityAction MazeSpawned;

    private int _width;
    private int _height;
    private Cell[,] _cells;
    private GameObject[,] _cellsView;
    private MazeGenerator _generator;

    public void Init()
    {
        InitializeMazeParameters();
        SpawnMaze(true);
    }

    private void InitializeMazeParameters()
    {
        _width = ParamsController.Maze.MAZE_WIDTH + 1;
        _height = ParamsController.Maze.MAZE_HEIGHT + 1;
        _cells = new Cell[_width, _height];
        _cellsView = new GameObject[_width, _height];
    }

    public void SpawnMaze(bool isFirstSpawn = false)
    {
        _generator = InitializeMaze();
        _generator.GenerateTraps(ParamsController.Maze.TRAPS_AMOUNT);
        for (int i = 0; i < _width; i++)
        for (int j = 0; j < _height; j++)
        {
            if (isFirstSpawn)
            {
                Cell cell = InstantiateCells(i, j);
                _cells[i, j] = cell;
                _cellsView[i, j] = cell.gameObject;
            }

            SetActivityCellItems(_cells[i, j], i, j);
        }

        MazeSpawned?.Invoke();
    }

    private MazeGenerator InitializeMaze()
    {
        MazeGenerator generator = new MazeGenerator();
        generator.GenerateCells();
        return generator;
    }

    private Cell InstantiateCells(int i, int j)
    {
        Vector3 startPosition =
            new Vector3(i * ParamsController.Maze.CELL_SIZE, 0, j * ParamsController.Maze.CELL_SIZE);
        Cell cell = Instantiate(_cellPrefab, startPosition, Quaternion.identity, _parent);
        return cell;
    }

    private void SetActivityCellItems(Cell cell, int i, int j)
    {
        SetActivityBottomWall(cell, _generator.GetBottomWallActivity(i, j));
        SetActivityLeftWall(cell, _generator.GetLeftWallActivity(i, j));
        SetActivityTrap(cell, _generator.GetTrapActivity(i, j));

        if (IsLastColumn(i))
        {
            SetActivityBottomWall(cell, false);
            DisableFloor(cell);
            DisableTrap(cell);
        }

        if (IsLastRow(j))
        {
            SetActivityLeftWall(cell, false);
            DisableFloor(cell);
            DisableTrap(cell);
        }

        if (IsFirstOrLastCell())
            DisableTrap(cell);

        bool IsLastColumn(int columnNumber)
        {
            return columnNumber == _width - 1;
        }

        bool IsLastRow(int rowNumber)
        {
            return rowNumber == _height - 1;
        }

        bool IsFirstOrLastCell()
        {
            return i == 0 && j == 0 || i == _width - 2 && j == _height - 2;
        }
    }

    private static void SetActivityLeftWall(Cell cell, bool isActive)
    {
        cell.WallLeft.gameObject.SetActive(isActive);
    }

    private static void SetActivityBottomWall(Cell cell, bool isActive)
    {
        cell.WallBottom.gameObject.SetActive(isActive);
    }

    private static void SetActivityTrap(Cell cell, bool isActive)
    {
        cell.Trap.gameObject.SetActive(isActive);
    }

    private static void DisableFloor(Cell cell)
    {
        cell.Floor.gameObject.SetActive(false);
    }

    private static void DisableTrap(Cell cell)
    {
        cell.Trap.gameObject.SetActive(false);
    }
}