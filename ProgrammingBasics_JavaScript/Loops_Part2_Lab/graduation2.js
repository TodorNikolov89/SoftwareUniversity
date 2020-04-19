function graduation2(input) {
    let name = input.shift();
    let sum = 0;
    let grade =  Number(input.shift());
    let totalGrades = 1;
    let excluded = false;
    while (!isNaN(grade) && totalGrades <= 12) {
        if (grade < 4) {
            grade = Number(input.shift());
            excluded = true;
            break;;
        }

        sum += grade;
        grade = Number(input.shift());
        totalGrades++;
    }

    let av = sum / 12;
    if (excluded) {
        console.log(`${name} has been excluded at ${totalGrades} grade`)
    } else {
        console.log(`${name} graduated. Average grade: ${av.toFixed(2)}`)
    }
}

// graduation2([
//     "Gosho",
// 5,
// 5.5,
// 6,
// 5.43,
// 5.5,
// 6,
// 5.55,
// 5,
// 6,
// 6,
// 5.43,
// 5    
// ])