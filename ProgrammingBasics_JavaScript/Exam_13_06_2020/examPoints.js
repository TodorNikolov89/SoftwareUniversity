function solve(problem, points, course) {
    problem = problem.toString();
    points = Number(points);
    course = course;

    let totalPoints = 0;
    let coeff = 0;

    switch (course) {
        case 'Basics':
            if (problem == "1") {
                coeff = 8;
            } else if (problem == "2") {
                coeff = 9;
            } else if (problem == "3") {
                coeff = 9;
            } else if (problem == "4") {
                coeff = 10;
            }
            break;
        case 'Fundamentals':
            if (problem == "1") {
                coeff = 11;
            } else if (problem == "2") {
                coeff = 11;
            } else if (problem == "3") {
                coeff = 12;
            } else if (problem == "4") {
                coeff = 13;
            }
            break;
        case 'Advanced':
            if (problem == "1") {
                coeff = 14;
            } else if (problem == "2") {
                coeff = 14;
            } else if (problem == "3") {
                coeff = 15;
            } else if (problem == "4") {
                coeff = 16;
            }
            break;
    }

    totalPoints = points * coeff / 100;
    if (course === 'Advanced') {
        totalPoints += totalPoints * 0.20;
    } else if (course === 'Basics' && problem == '1') {
        totalPoints -= totalPoints * 0.20;
    }

    console.log(`Total points: ${totalPoints.toFixed(2)}`)
}


solve(1, 100, 'Basics')
solve(2,95,'Advanced')
solve(3, 80, 'Fundamentals')