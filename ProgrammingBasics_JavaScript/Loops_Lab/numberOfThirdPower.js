function numberOfThirdPower(n) {
    n = Number(n);
    if (n % 2 === 0) {
        solve(2, n)
    } else {
        solve(1, n)
    }

    function solve(start, n) {
        for (let i = start; i <= n; i += 2) {
            console.log(`Current number: ${i}. Cube: ${Math.pow(i, 3)}`)
        }
    }
}

// numberOfThirdPower(6)