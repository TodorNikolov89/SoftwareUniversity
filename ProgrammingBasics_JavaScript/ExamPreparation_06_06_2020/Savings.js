function solve(input) {
    monthIncome = Number(input.shift());
    months = Number(input.shift());
    neededMoney = Number(input.shift());

    let personalSpendings = monthIncome * 0.30;
    let savingsPerMonth = monthIncome - (neededMoney + personalSpendings);

    let savedMoney = months * savingsPerMonth;
    let result = savingsPerMonth / monthIncome * 100;

    let message = `She can save ${Number.parseFloat(result).toFixed(2)}%\n${Number.parseFloat(savedMoney).toFixed(2)}`
    console.log(message);
}

solve(['1500', '3', '800'])