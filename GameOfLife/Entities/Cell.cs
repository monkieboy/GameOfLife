namespace GameOfLife.Entities
{
    public class Cell
    {
        public static Cell[][] CreateDefaultMatrix(int rows, int columns)
        {
            var matrix = new Cell[rows][];

            for (var x = 0; x < rows; x++)
            {
                matrix[x] = new Cell[columns];
                for (var y = 0; y < columns; y++)
                {
                    matrix[x][y] = new Cell
                        {
                            X = x, 
                            Y = y, 
                            State = CellState.Dead
                        };
                }
            }

            return matrix;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public CellState State { get; set; }

        public static Cell[][] CreateBlockMatrix()
        {
            var cells = CreateDefaultMatrix(4, 4);
            cells[1][1].State = CellState.Live;
            cells[1][2].State = CellState.Live;
            cells[2][1].State = CellState.Live;
            cells[2][2].State = CellState.Live;
            return cells;
        }
    }
}