using UnityEngine;

public class Maze : MonoBehaviour
{

    [SerializeField]
    int mazeRows;
    [SerializeField]
    int mazeColumns;
    [SerializeField]
    GameObject wall;
    MazeCell[,] mazeCells;
    Vector3 size;

    [SerializeField]
    Color StartColour;
    [SerializeField]
    Color EndColour;

    int cRow, cColumn;
    bool courseComplete = false;

    // Use this for initialization
    void Start()
    {
        size = wall.transform.localScale;
        InitMaze();
        HAK();
    }

    void InitMaze()
    {
        mazeCells = new MazeCell[mazeRows, mazeColumns];
        for (int r = 0; r < mazeRows; r++) //for how many rows there are
        {
            for (int c = 0; c < mazeColumns; c++) //for how many columns there are
            {
                mazeCells[r, c] = new MazeCell();

                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size.x, -(size.z) / 2, c * size.y), Quaternion.identity, transform.parent) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + ", " + c;
                mazeCells[r, c].floor.transform.Rotate(90, 0, 0);
                if ((r == 0) && (c == 0))
                {
                    mazeCells[r, c].floor.GetComponent<Renderer>().material.color = StartColour;
                }
                else if ((r == mazeRows - 1) && (c == mazeColumns - 1))
                {
                    mazeCells[r, c].floor.GetComponent<Renderer>().material.color = EndColour;
                    mazeCells[r, c].floor.tag = "DCP1";
                }

                if (c == 0)
                {
                    mazeCells[r, c].westW = Instantiate(wall, new Vector3(r * size.x, (size.y) / 2, (c * size.y) - ((size.y) / 2)), Quaternion.identity, transform.parent) as GameObject;
                    mazeCells[r, c].westW.name = "West Wall " + r + ", " + c;
                }

                mazeCells[r, c].eastW = Instantiate(wall, new Vector3(r * size.x, (size.y) / 2, (c * size.y) + ((size.y) / 2)), Quaternion.identity, transform.parent) as GameObject;
                mazeCells[r, c].eastW.name = "East Wall " + r + ", " + c;

                if (r == 0)
                {
                    mazeCells[r, c].northW = Instantiate(wall, new Vector3((r * (size.x)) - ((size.y) / 2), (size.y) / 2, c * (size.y)), Quaternion.identity, transform.parent) as GameObject;
                    mazeCells[r, c].northW.name = "North Wall " + r + ", " + c;
                    mazeCells[r, c].northW.transform.Rotate(0, 90, 0);
                }

                mazeCells[r, c].southW = Instantiate(wall, new Vector3((r * (size.x)) + ((size.y) / 2), (size.y) / 2, c * (size.y)), Quaternion.identity, transform.parent) as GameObject;
                mazeCells[r, c].southW.name = "South Wall " + r + ", " + c;
                mazeCells[r, c].southW.transform.Rotate(0, 90, 0);
            }
        }
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
            }
            else if ((direction == 2) && (CellAvailable(cRow + 1, cColumn)))
            {
                DestroyWall(mazeCells[cRow, cColumn].southW);
                DestroyWall(mazeCells[cRow + 1, cColumn].northW);
                cRow++;
            }
            else if ((direction == 3) && (CellAvailable(cRow, cColumn + 1)))
            {
                DestroyWall(mazeCells[cRow, cColumn].eastW);
                DestroyWall(mazeCells[cRow, cColumn + 1].westW);
                cColumn++;
            }
            else if ((direction == 4) && (CellAvailable(cRow, cColumn - 1)))
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

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
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

        if ((row > 0) && (!mazeCells[row - 1, column].visited)) //if cell to the left is unvisited
        {
            paths++;
        }

        if ((row < mazeRows - 1) && (!mazeCells[row + 1, column].visited)) //right
        {
            paths++;
        }

        if ((column > 0) && (!mazeCells[row, column - 1].visited)) //if cell above is unvisited
        {
            paths++;
        }

        if ((column < mazeColumns - 1) && (!mazeCells[row, column + 1].visited)) //below
        {
            paths++;
        }

        return (paths > 0);
    }

    bool CellAvailable(int row, int column)
    {
        if ((((row >= 0) && (row < mazeRows)) && ((column >= 0) && (column < mazeColumns))) && !mazeCells[row, column].visited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DestroyWall(GameObject obj)
    {
        if (obj != null)
        {
            Object.Destroy(obj);
        }
    }

    bool NextToCell(int row, int column)
    {
        int visited = 0;

        if ((row > 0) && (mazeCells[row - 1, column].visited)) //if cell above? is visited
        {
            visited++;
        }

        if ((row < mazeRows - 1) && (mazeCells[row + 1, column].visited))
        {
            visited++;
        }

        if ((column > 0) && (mazeCells[row, column - 1].visited)) //if cell to the right is unvisited
        {
            visited++;
        }

        if ((column < mazeColumns - 1) && (mazeCells[row, column + 1].visited))
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
            }
            else if (((direction == 2) && (row < (mazeRows - 2))) && (mazeCells[row + 1, column].visited))
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
            else if (((direction == 4) && (column < (mazeColumns - 2))) && (mazeCells[row, column + 1].visited))
            {
                DestroyWall(mazeCells[row, column].eastW);
                DestroyWall(mazeCells[row, column + 1].westW);
                destroyed = true;
            }
        }
    }

}