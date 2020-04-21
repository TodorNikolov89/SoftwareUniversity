function trainTheTrainers(input) {
    let n = Number(input.shift());
    let averagePresentationGrade = 0;
    let totalGrades = 0;
    let averageGrade = 0;
    let presentationName = input.shift();

    while (presentationName !=='Finish') {

        for (let j = 0; j < n; j++) {
            averagePresentationGrade += Number(input.shift());
        }
        totalGrades++;

        averagePresentationGrade /= n;
        averageGrade += averagePresentationGrade;
        console.log(`${presentationName} - ${averagePresentationGrade.toFixed(2)}.`);
        averagePresentationGrade = 0;

        presentationName = input.shift();
    }

    console.log(`Student's final assessment is ${(averageGrade / totalGrades).toFixed(2)}.`);
}


// trainTheTrainers([
//     2,
//     "While-Loop",
//     6.00,
//     5.50,
//     "For-Loop",
//     5.84,
//     5.66,
//     "Finish"
// ])


// trainTheTrainers([
//     2,
//     'Objects and Classes',
//     5.77,
//     4.23,
//     'Dictionaries',
//     4.62,
//     5.02,
//     'RegEx',
//     2.88,
//     3.42,
//     'Finish'
// ])