using UnityEngine;

public class GenMaze : MazeAlgorithm
{

    private int cRow = 0;
    private int cColumn = 0;

    private bool courseComplete = false;

    public GenMaze(MazeCell[,] mazeCells) : base(mazeCells) { }

    // Use this for initialization
    public override void StartGen()
    {
        HAK();
    }

    void HAK()
    {
        mazeCells[cRow, cColumn].visited = true;

        while (!courseComplete)
        {
            Kill();
            Hunt();
        }
    }

    void Kill()
    {
        while (PathAvailable(cRow, cColumn))
        {
            int direction = Random.Range(1, 5);

            if ((direction == 1) && (CellAvailable(cRow - 1, cColumn)))
            {
                DestroyWall(mazeCells[cRow, cColumn].northW);
                DestroyWall(mazeCells[cRow - 1, cColumn].southW);
                cRow--;
            }else if ((direction == 2) && (CellAvailable(cRow + 1, cColumn)))
            {
                DestroyWall(mazeCells[cRow, cColumn].southW);
                DestroyWall(mazeCells[cRow + 1, cColumn].northW);
                cRow++;
            }else if ((direction == 3) && (CellAvailable(cRow, cColumn + 1)))
            {
                DestroyWall(mazeCells[cRow, cColumn].eastW);
                DestroyWall(mazeCells[cRow, cColumn + 1].westW);
                cColumn++;
            }else if ((direction  == 4) && (CellAvailable(cRow, cColumn - 1)))
            {
                DestroyWall(mazeCells[cRow, cColumn - 1].eastW);
                DestroyWall(mazeCells[cRow, cColumn].westW);
                cColumn--;
            }

            mazeCells[cRow, cColumn].visited = true; //without this, there's a memory leak

        }

    }

    void Hunt()
    {
        courseComplete = true;

        for (int r = 0; r < mRows; r++)
        {
           for (int c = 0; c < mColumns; c++)
            {
                if ((!mazeCells[r, c].visited) && (NextToCell(r, c)))
                {
                    courseComplete = false;
                    cRow = r;
                    cColumn = c;
                    DestroyCellWall(cRow, cColumn);
                    mazeCells[cRow, cColumn].visited = true;
                    return;
                }
            }
        }
    }

    bool PathAvailable(int row, int column)
    {
        int paths = 0;

        if ((row > 0) && (!mazeCells[row-1, column].visited)) //if cell above? is unvisited
        {
            paths++;
        }

        if ((row < mRows - 1) && (!mazeCells[row+1, column].visited))
        {
            paths++;
        }

        if ((column > 0) && (!mazeCells[row, column - 1].visited)) //if cell to the right is unvisited
        {
            paths++;
        }

        if ((column < mColumns - 1) && (!mazeCells[row, column + 1].visited))
        {
            paths++;
        }

        return (paths > 0);
    }

    bool CellAvailable(int row, int column)
    {
        if ((((row >= 0) && (row < mRows)) && ((column >= 0) && (column < mColumns))) && !mazeCells[row, column].visited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DestroyWall(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }

    bool NextToCell(int row, int column)
    {
        int visited = 0;

        if ((row > 0) && (mazeCells[row - 1, column].visited)) //if cell above? is unvisited
        {
            visited++;
        }

        if ((row < mRows - 1) && (mazeCells[row + 1, column].visited))
        {
            visited++;
        }

        if ((column > 0) && (mazeCells[row, column - 1].visited)) //if cell to the right is unvisited
        {
            visited++;
        }

        if ((column < mColumns - 1) && (mazeCells[row, column + 1].visited))
        {
            visited++;
        }

        return (visited > 0);
    }

    void DestroyCellWall(int row, int column)
    {
        bool destroyed = false;

        while (!destroyed)
        {
            int direction = Random.Range(1, 5);

            if (((direction == 1) && (row > 0)) && (mazeCells[row - 1, column].visited))
            {
                DestroyWall(mazeCells[row, column].northW);
                DestroyWall(mazeCells[row - 1, column].southW);
                destroyed = true;
            }else if (((direction == 2) && (row < (mRows - 2))) && (mazeCells[row + 1, column].visited))
            {
                DestroyWall(mazeCells[row, column].southW);
                DestroyWall(mazeCells[row + 1, column].northW);
                destroyed = true;
            }
            else if (((direction == 3) && (column > 0)) && (mazeCells[row, column - 1].visited))
            {
                DestroyWall(mazeCells[row, column].westW);
                DestroyWall(mazeCells[row, column - 1].eastW);
                destroyed = true;
            }
            else if (((direction == 4) && (column < (mColumns - 2))) && (mazeCells[row, column + 1].visited))
            {
                DestroyWall(mazeCells[row, column].eastW);
                DestroyWall(mazeCells[row, column + 1].westW);
                destroyed = true;
            }
        }
    }

}