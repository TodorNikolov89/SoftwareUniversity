function examPreparatoin(input) {
    let badGrades = Number(input.shift());
    let outputMessage = '';
    let problemName = input.shift();
    let grade = Number(input.shift());
    let allGrades = 0;
    let sumGrades = 0;
    let lastProblem = '';
    let badGradesCount = 0;

    while (problemName !== 'Enough' && badGradesCount < badGrades) {
        allGrades++;
        if (grade <= 4) {
            badGradesCount++
        }
        sumGrades += grade;
        lastProblem = problemName;

        problemName = input.shift();
        grade = Number(input.shift());
        
    }

    if (problemName === 'Enough') {
        let avGrade = sumGrades / allGrades;
        outputMessage = `Average score: ${avGrade.toFixed(2)}\nNumber of problems: ${allGrades}\nLast problem: ${lastProblem}`;
    } else {
        outputMessage = `You need a break, ${badGradesCount} poor grades.`;
    }

    console.log(outputMessage)
}

// examPreparatoin([3, "Money", 6, "Story", 4, "Spring Time", 5, "Bus", 6, "Enough"])
// examPreparatoin([2, "Income", 3, "Game Info", 6, "Best Player", 4])