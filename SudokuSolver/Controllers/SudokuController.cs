using Microsoft.AspNetCore.Mvc;

namespace SudokuSolver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SudokuController : ControllerBase
    {
        [HttpPost("solve")]
        public IActionResult Solve([FromBody] SudokuRequest request)
        {
            try
            {
                if (request?.Puzzle == null || request.Puzzle.Length != 81)
                    return BadRequest(new { error = "Incorret format" });
                // Проверка диапазона
                foreach (var n in request.Puzzle)
                {
                    if (n < 0 || n > 9)
                        return BadRequest(new { error = "Only allowed to enter digits from 1 to 9" });
                }
                if (!SudokuSolver.IsSolvable(request.Puzzle))
                    return BadRequest(new { error = "No solves for this board. Check the input." });
                var solution = SudokuSolver.Solve(request.Puzzle);
                return Ok(new { solution });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error: " + ex.Message });
            }
        }
    }

    public class SudokuRequest
    {
        public int[] Puzzle { get; set; }
    }

    public static class SudokuSolver
    {
        static bool[,] rowUsed = new bool[9, 9];
        static bool[,] colUsed = new bool[9, 9];
        static bool[,] blockUsed = new bool[9, 9];
        static List<(int r, int c)> empties;

        public static int[] Solve(int[] puzzle)
        {
            // Convert int[] to char[][]
            char[][] board = new char[9][];
            for (int i = 0; i < 9; i++)
            {
                board[i] = new char[9];
                for (int j = 0; j < 9; j++)
                {
                    int val = puzzle[i * 9 + j];
                    board[i][j] = val == 0 ? '.' : (char)('0' + val);
                }
            }

            if (!SolveSudoku(ref board))
                return null;

            // Convert back to int[]
            int[] solution = new int[81];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i * 9 + j] = board[i][j] == '.' ? 0 : board[i][j] - '0';
            return solution;
        }

        public static bool IsSolvable(int[] puzzle)
        {
            char[][] board = new char[9][];
            for (int i = 0; i < 9; i++)
            {
                board[i] = new char[9];
                for (int j = 0; j < 9; j++)
                {
                    int val = puzzle[i * 9 + j];
                    board[i][j] = val == 0 ? '.' : (char)('0' + val);
                }
            }
            return SolveSudoku(ref board);
        }

        public static bool SolveSudoku(ref char[][] board)
        {
            rowUsed = new bool[9, 9];
            colUsed = new bool[9, 9];
            blockUsed = new bool[9, 9];
            empties = new List<(int, int)>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j] == '.')
                    {
                        empties.Add((i, j));
                    }
                    else
                    {
                        int d = board[i][j] - '1';
                        rowUsed[i, d] = true;
                        colUsed[j, d] = true;
                        int b = (i / 3) * 3 + (j / 3);
                        blockUsed[b, d] = true;
                    }
                }
            }
            return Fill(0, board);
        }

        private static bool Fill(int idx, char[][] board)
        {
            if (idx == empties.Count)
                return true;
            var (r, c) = empties[idx];
            int b = (r / 3) * 3 + (c / 3);
            for (int d = 0; d < 9; d++)
            {
                if (rowUsed[r, d] || colUsed[c, d] || blockUsed[b, d])
                    continue;
                board[r][c] = (char)('1' + d);
                rowUsed[r, d] = colUsed[c, d] = blockUsed[b, d] = true;
                if (Fill(idx + 1, board))
                    return true;
                board[r][c] = '.';
                rowUsed[r, d] = colUsed[c, d] = blockUsed[b, d] = false;
            }
            return false;
        }
    }
}
