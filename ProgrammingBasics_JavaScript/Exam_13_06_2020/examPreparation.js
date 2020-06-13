function solve(students, problems, energy) {
    students = Number(students);
    problems = Number(problems);
    energy = Number(energy);
    let questions = 0;
    let solvedProblems = 0;
    let totalQuestions = 0;
    let message = ``;

    while (energy > 0) {
        solvedProblems += problems;
        energy += problems * 2;
        students -= problems;
        questions = students * 2;
        totalQuestions += questions;
        energy -= 3 * questions;

        if (students > 10) {
            students += Math.floor(students / 10)
        } else {
            message = `The students are too few!\nProblems solved: ${solvedProblems}`
            break;
        }
    }
    if (energy <= 0) {
        message = `The trainer is too tired!\nQuestions asked: ${totalQuestions}`
    }
    console.log(message)
}


solve(20, 5, 500)
solve(20, 1, 100)