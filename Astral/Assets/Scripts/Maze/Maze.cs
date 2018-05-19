using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private int mazeRows;
    [SerializeField]
    private int mazeColumns;
    [SerializeField]
    private GameObject wall;
    private MazeCell[,] mazeCells;
    private Vector3 size;

    [SerializeField]
    private Color StartColour;
    [SerializeField]
    private Color EndColour;

    [SerializeField]
    Transform parent;

    // Use this for initialization
    void Start()
    {
        size = wall.transform.localScale;
        InitMaze();
        MazeAlgorithm mA = new GenMaze(mazeCells);
        mA.StartGen();
    }

    void InitMaze()
    {
        mazeCells = new MazeCell[mazeRows, mazeColumns];
        for (int r = 0; r < mazeRows; r++) //for how many rows there are
        {
            for (int c = 0; c < mazeColumns; c++) //for how many columns there are
            {
#pragma warning disable IDE0017 // Simplify object initialization
                mazeCells[r, c] = new MazeCell();
#pragma warning restore IDE0017 // Simplify object initialization

                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size.x, -(size.z) / 2, c * size.y), Quaternion.identity, parent) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + ", " + c;
                mazeCells[r, c].floor.transform.Rotate(90, 0, 0);
                if ((r == 0) && (c == 0))
                {
                    mazeCells[r, c].floor.GetComponent<Renderer>().material.color = StartColour;
                }else if ((r == mazeRows-1) && (c == mazeColumns - 1))
                {
                    mazeCells[r, c].floor.GetComponent<Renderer>().material.color = EndColour;
                }

                if (c == 0)
                {
                    mazeCells[r, c].westW = Instantiate(wall, new Vector3(r * size.x, (size.y) / 2, (c * size.y) - ((size.y) / 2)), Quaternion.identity, parent) as GameObject;
                    mazeCells[r, c].westW.name = "West Wall " + r + ", " + c;
                }

                mazeCells[r, c].eastW = Instantiate(wall, new Vector3(r * size.x, (size.y) / 2, (c * size.y) + ((size.y) / 2)), Quaternion.identity, parent) as GameObject;
                mazeCells[r, c].eastW.name = "East Wall " + r + ", " + c;

                if (r == 0)
                {
                    mazeCells[r, c].northW = Instantiate(wall, new Vector3((r * (size.x)) - ((size.y) / 2), (size.y) / 2, c * (size.y)), Quaternion.identity, parent) as GameObject;
                    mazeCells[r, c].northW.name = "North Wall " + r + ", " + c;
                    mazeCells[r, c].northW.transform.Rotate(0, 90, 0);
                }

                mazeCells[r, c].southW = Instantiate(wall, new Vector3((r * (size.x)) + ((size.y) / 2), (size.y) / 2, c * (size.y)), Quaternion.identity, parent) as GameObject;
                mazeCells[r, c].southW.name = "South Wall " + r + ", " + c;
                mazeCells[r, c].southW.transform.Rotate(0, 90, 0);
            }
        }
    }
}
