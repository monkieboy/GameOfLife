using System.Collections.Generic;
using GameOfLife.Entities;
using System.Linq;

namespace GameOfLife
{
    public class Simulator
    {
        private readonly Cell[][] startingState;
        private readonly int maxCycles;

        private Cell[][] currentState;

        public Simulator(Cell[][] startingState, int maxCycles)
        {
            this.startingState = startingState;
            this.maxCycles = maxCycles;

            Simulate();
        }

        private void Simulate()
        {
            for (var i = 0; i < startingState.Length; i++)
            {
                for (var j = 0; j < startingState[0].Length; j++)
                {
                    if (CanReproduce(startingState[i][j]))
                    {
                        Simulation[i][j].State = CellState.Live;
                    }
                }
            }

            if (Simulation == null)
            {
                var state = new Cell[1][];
                state[0] = new[] {new Cell {State = CellState.Dead, X = 0, Y = 0}};
                currentState = state;
            }
        }

        public Cell[][] Simulation
        {
            get
            {
                return currentState ?? (currentState = Cell.CreateDefaultMatrix(startingState.Length, startingState[0].Length));
            }
        }

        private bool CanReproduce(Cell cell)
        {
            return CheckAdjacentCellsForSignsOfLife(cell);
        }

        private bool CheckAdjacentCellsForSignsOfLife(Cell cell)
        {
            var cells = LoadAdjacentCells(cell);

            return cells.Count(x => x.State == CellState.Live) > 2;
        }

        private IEnumerable<Cell> LoadAdjacentCells(Cell cell)
        {
            var cells = new List<Cell>();

            LoadRightCells(cell, cells);

            LoadLeftCells(cell, cells);

            return cells;
        }

        private void LoadLeftCells(Cell cell, List<Cell> cells)
        {
            if (NotAtTheLeftEdgeAlready(cell))
            {
                LoadBottomLeftCell(cell, cells);

                LoadLeftCell(cell, cells);

                LoadTopLeftCell(cell, cells);
            }
        }

        private void LoadTopLeftCell(Cell cell, List<Cell> cells)
        {
            if (NotAtTheTopAlready(cell))
            {
                cells.Add(startingState[cell.X - 1][cell.Y - 1]);
            }
        }

        private void LoadLeftCell(Cell cell, List<Cell> cells)
        {
            cells.Add(startingState[cell.X][cell.Y - 1]);
        }

        private void LoadBottomLeftCell(Cell cell, List<Cell> cells)
        {
            if (NotAtTheBottomAlready(cell))
            {
                cells.Add(startingState[cell.X + 1][cell.Y - 1]);
            }
        }

        private void LoadRightCells(Cell cell, List<Cell> cells)
        {
            if (NotAtTheRightEdgeAlready(cell))
            {
                LoadCellsToTheTopAndTopRight(cell, cells);

                LoadRightCell(cell, cells);

                LoadCellsToTheBottomAndBottomRight(cell, cells);
            }
        }

        private void LoadCellsToTheBottomAndBottomRight(Cell cell, List<Cell> cells)
        {
            if (NotAtTheBottomAlready(cell))
            {
                cells.Add(startingState[cell.X + 1][cell.Y + 1]);
                cells.Add(startingState[cell.X + 1][cell.Y]);
            }
        }

        private void LoadRightCell(Cell cell, List<Cell> cells)
        {
            cells.Add(startingState[cell.X][cell.Y + 1]);
        }

        private void LoadCellsToTheTopAndTopRight(Cell cell, List<Cell> cells)
        {
            if (NotAtTheTopAlready(cell))
            {
                cells.Add(startingState[cell.X - 1][cell.Y]);
                cells.Add(startingState[cell.X - 1][cell.Y + 1]);
            }
        }

        private static bool NotAtTheTopAlready(Cell cell)
        {
            return cell.X - 1 > -1;
        }

        private bool NotAtTheRightEdgeAlready(Cell cell)
        {
            return cell.Y + 1 < startingState[0].Length;
        }

        private bool NotAtTheBottomAlready(Cell cell)
        {
            return cell.X + 1 < startingState.Length;
        }

        private static bool NotAtTheLeftEdgeAlready(Cell cell)
        {
            return cell.Y - 1 > -1;
        }
    }
}
