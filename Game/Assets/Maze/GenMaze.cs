using UnityEngine;

public class GenMaze : MazeAlgorithm {

    private int cRow = 0;
    private int cColumn = 0;

    private bool courseComplete = false;

    public GenMaze(MazeCell[,] mazeCells) : base(mazeCells) {}

    // Use this for initialization
    public override void StartGen()
    {
        //HAK();
    }



}
