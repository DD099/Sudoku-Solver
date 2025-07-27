document.addEventListener('DOMContentLoaded', () => {
    const board = document.getElementById('sudoku-board');
    for (let i = 0; i < 81; i++) {
        const input = document.createElement('input');
        input.type = 'number';
        input.min = 1;
        input.max = 9;
        input.value = '';
        board.appendChild(input);
    }

document.getElementById('solve-btn').addEventListener('click', async () => {
    const cells = Array.from(board.querySelectorAll('input'));
    // Validate input: only numbers 1-9 or empty
    for (const cell of cells) {
        if (cell.value && (isNaN(cell.value) || cell.value < 1 || cell.value > 9)) {
            document.getElementById('solution').textContent = 'Error: Only allowed to enter numbers from 1 to 9';
            return;
        }
    }
    const puzzle = cells.map(cell => cell.value ? parseInt(cell.value) : 0);
    try {
        const response = await fetch('/api/sudoku/solve', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ puzzle })
        });
        const text = await response.text();
        let result;
        try {
            result = JSON.parse(text);
        } catch (e) {
            throw new Error('Invalid JSON response');
        }
        if (!response.ok) {
            document.getElementById('solution').textContent = result.error || 'Error occurred.';
        } else if (result.solution) {
            result.solution.forEach((num, idx) => {
                cells[idx].value = num;
            });
            document.getElementById('solution').textContent = 'Solved!';
        } else {
            document.getElementById('solution').textContent = 'No solves for this board';
        }
    } catch (error) {
        document.getElementById('solution').textContent = 'Error: ' + error.message;
    }
});

// Erase button logic
document.getElementById('erase-btn').addEventListener('click', () => {
    const cells = Array.from(board.querySelectorAll('input'));
    cells.forEach(cell => cell.value = '');
    document.getElementById('solution').textContent = '';
});
});
