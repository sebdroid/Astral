/*using UnityEngine;
using System.Collections;

public class HuntAndKillMazeAlgorithm : MazeAlgorithm {

	private int currentRow = 0;
	private int currentColumn = 0;

	private bool courseComplete = false;

	public HuntAndKillMazeAlgorithm(MazeCell[,] mazeCells) : base(mazeCells) {
	}

	public override void CreateMaze () {
		HuntAndKill ();
	}

	private void HuntAndKill() {
		mazeCells [currentRow, currentColumn].visited = true;

		while (! courseComplete) {
			Kill(); // Will run until it hits a dead end.
			Hunt(); // Finds the next unvisited cell with an adjacent visited cell. If it can't find any, it sets courseComplete to true.
		}
	}

	private void Kill() {
		while (RouteStillAvailable (currentRow, currentColumn)) {
			// int direction = Random.Range (1, 5);
			int direction = ProceduralNumberGenerator.GetNextNumber ();

			if (direction == 1 && CellIsAvailable (currentRow - 1, currentColumn)) {
				// North
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].northWall);
				DestroyWallIfItExists (mazeCells [currentRow - 1, currentColumn].southWall);
				currentRow--;
			} else if (direction == 2 && CellIsAvailable (currentRow + 1, currentColumn)) {
				// South
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].southWall);
				DestroyWallIfItExists (mazeCells [currentRow + 1, currentColumn].northWall);
				currentRow++;
			} else if (direction == 3 && CellIsAvailable (currentRow, currentColumn + 1)) {
				// east
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].eastWall);
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn + 1].westWall);
				currentColumn++;
			} else if (direction == 4 && CellIsAvailable (currentRow, currentColumn - 1)) {
				// west
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn].westWall);
				DestroyWallIfItExists (mazeCells [currentRow, currentColumn - 1].eastWall);
				currentColumn--;
			}

			mazeCells [currentRow, currentColumn].visited = true;
		}
	}

	private void Hunt() {
		courseComplete = true; // Set it to this, and see if we can prove otherwise below!

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				if (!mazeCells [r, c].visited && CellHasAnAdjacentVisitedCell(r,c)) {
					courseComplete = false; // Yep, we found something so definitely do another Kill cycle.
					currentRow = r;
					currentColumn = c;
					DestroyAdjacentWall (currentRow, currentColumn);
					mazeCells [currentRow, currentColumn].visited = true;
					return; // Exit the function
				}
			}
		}
	}


	

	

	private void DestroyAdjacentWall(int row, int column) {
		bool wallDestroyed = false;

		while (!wallDestroyed) {
			// int direction = Random.Range (1, 5);
			int direction = ProceduralNumberGenerator.GetNextNumber ();

			if (direction == 1 && row > 0 && mazeCells [row - 1, column].visited) {
				DestroyWallIfItExists (mazeCells [row, column].northWall);
				DestroyWallIfItExists (mazeCells [row - 1, column].southWall);
				wallDestroyed = true;
			} else if (direction == 2 && row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
				DestroyWallIfItExists (mazeCells [row, column].southWall);
				DestroyWallIfItExists (mazeCells [row + 1, column].northWall);
				wallDestroyed = true;
			} else if (direction == 3 && column > 0 && mazeCells [row, column-1].visited) {
				DestroyWallIfItExists (mazeCells [row, column].westWall);
				DestroyWallIfItExists (mazeCells [row, column-1].eastWall);
				wallDestroyed = true;
			} else if (direction == 4 && column < (mazeColumns-2) && mazeCells [row, column+1].visited) {
				DestroyWallIfItExists (mazeCells [row, column].eastWall);
				DestroyWallIfItExists (mazeCells [row, column+1].westWall);
				wallDestroyed = true;
			}
		}

	}

}
*/