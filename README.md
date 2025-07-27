Full-Stack Sudoku Solver

This is a full-stack Sudoku Solver application. The project consists of two main parts:

1. Back-End API: The solver logic is written in C# and exposed as a web API using ASP.NET Core. It handles the computation for solving the puzzles.

2. Front-End UI: The user interface is a visual grid built with HTML, CSS, and JavaScript, allowing users to input puzzles and see the results.

This project is a web API built with ASP.NET Core that provides an endpoint to solve 9x9 Sudoku puzzles.

Prerequisites

Before you begin, ensure you have the following software installed on your machine:

.NET 9 SDK: This is required to build and run the project.

1.Download .NET 9 SDK

Git: This is required to clone the project repository from GitHub.

2.Download Git

3. (Optional) A Code Editor: A code editor like Visual Studio Code is highly recommended for viewing and editing the code.

How to Run the Project
Follow these steps to get the application running on your local machine.

1. Clone the Repository
Open a terminal or command prompt and clone the project repository from GitHub:

git clone https://github.com/DD099/Sudoku-Solver.git

2. Navigate to the Project Directory
Change your directory to the project folder that was just created:

cd SudokuSolver.Api

3. Run the Application
Use the .NET CLI to run the project. This command will build the project and start the web server.

dotnet run

After running the command, you will see output in your terminal indicating that the server is running. It will be listening on a specific URL, typically something like http://localhost:5123 or https://localhost:7123. Enter the url and test it out!

Rules of Sudoku:
1. Each of the digits 1-9 must occur exactly once in each row.
2. Each of the digits 1-9 must occur exactly once in each column.
3. Each of the digits 1-9 must occur exactly once in each of the 9 3x3 sub-boxes of the grid.
