using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator
{
    private MazeGeneratorCell[,] _maze;

    public void GenerateCells()
    {
        _maze = new MazeGeneratorCell[ParamsController.Maze.MAZE_WIDTH + 1, ParamsController.Maze.MAZE_HEIGHT + 1];

        for (int i = 0; i < _maze.GetLength(0); i++)
        for (int j = 0; j < _maze.GetLength(1); j++)
            _maze[i, j] = new MazeGeneratorCell()
                {X = i, Y = j, IsTrap = false};

        RemoveRandomWallsWithBacktracker();
    }

    public bool GetLeftWallActivity(int i, int j)
    {
        return _maze[i, j].IsLeft;
    }

    public bool GetBottomWallActivity(int i, int j)
    {
        return _maze[i, j].IsBottom;
    }

    public bool GetTrapActivity(int i, int j)
    {
        return _maze[i, j].IsTrap;
    }

    public void GenerateTraps(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GenerateRandomTrap();
        }
    }

    private void RemoveRandomWallsWithBacktracker()
    {
        MazeGeneratorCell currentCell = _maze[0, 0];
        currentCell.IsVisited = true;

        Stack<MazeGeneratorCell> visitableCells = new Stack<MazeGeneratorCell>();

        do
        {
            List<MazeGeneratorCell> unvisitableNeighbours = new List<MazeGeneratorCell>();

            int x = currentCell.X;
            int y = currentCell.Y;
            if (x > 0 && _maze[x - 1, y].IsVisited == false) unvisitableNeighbours.Add(_maze[x - 1, y]);
            if (y > 0 && _maze[x, y - 1].IsVisited == false) unvisitableNeighbours.Add(_maze[x, y - 1]);
            if (x < ParamsController.Maze.MAZE_WIDTH - 1 && _maze[x + 1, y].IsVisited == false)
                unvisitableNeighbours.Add(_maze[x + 1, y]);
            if (y < ParamsController.Maze.MAZE_HEIGHT - 1 && _maze[x, y + 1].IsVisited == false)
                unvisitableNeighbours.Add(_maze[x, y + 1]);

            if (unvisitableNeighbours.Count > 0)
            {
                MazeGeneratorCell nextCell = unvisitableNeighbours[Random.Range(0, unvisitableNeighbours.Count)];
                RemoveWall(currentCell, nextCell);
                nextCell.IsVisited = true;
                currentCell = nextCell;
                visitableCells.Push(nextCell);
            }
            else
            {
                currentCell = visitableCells.Pop();
            }
        } while (visitableCells.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell currentCell, MazeGeneratorCell nextCell)
    {
        if (IsVerticalCellsLocation())
        {
            if (nextCell.Y > currentCell.Y)
                nextCell.IsBottom = false;
            else
                currentCell.IsBottom = false;
        }

        if (IsHorizontalCellsLocation())
        {
            if (nextCell.X > currentCell.X)
                nextCell.IsLeft = false;
            else
                currentCell.IsLeft = false;
        }

        bool IsVerticalCellsLocation()
        {
            return currentCell.X == nextCell.X;
        }

        bool IsHorizontalCellsLocation()
        {
            return currentCell.Y == nextCell.Y;
        }
    }

    private void GenerateRandomTrap()
    {
        MazeGeneratorCell randomCell;
        int randomX;
        int randomY;
        do
        {
            randomX = Random.Range(0, _maze.GetLength(0) - 1);
            randomY = Random.Range(0, _maze.GetLength(1) - 1);
            randomCell = _maze[randomX, randomY];
        } while (randomCell.IsTrap != false || IsStartCell(randomX, randomY) || IsFinishCell(randomX, randomY));

        randomCell.IsTrap = true;
    }

    private bool IsStartCell( int x, int y)
    {
        return x == 0 && y == 0;
    }
    private bool IsFinishCell( int x, int y)
    {
        return x == _maze.GetLength(0) - 2 && y == _maze.GetLength(1) -2;
    }
}

public class MazeGeneratorCell
{
    public int X;
    public int Y;
    public bool IsLeft = true;
    public bool IsBottom = true;
    public bool IsVisited;
    public bool IsTrap;
}