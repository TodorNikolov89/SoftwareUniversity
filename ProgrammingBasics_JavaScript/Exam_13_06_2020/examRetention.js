function solve(students, seasons) {
    students = Number(students);
    seasons = Number(seasons);
    let firstExamStudnets = 0;
    let secondExamStudents = 0;
    let passed = 0;

    for (let i = 1; i <= seasons; i++) {
        firstExamStudnets = Math.ceil(students * 0.90)
        secondExamStudents = Math.ceil(firstExamStudnets * 0.90)

        if (i % 3 === 0) {
            passed = Math.ceil(secondExamStudents * 0.80)
            passed += Math.ceil(passed * 0.15);
        } else {
            passed = Math.ceil(secondExamStudents * 0.80)
            passed += Math.ceil(passed * 0.05);
        }

        students = passed;
    }

    console.log(`Students: ${students}`)
}

solve(100, 6)
solve(2000,5)