function graduation(input) {
    let name = input.shift();
    let sum = 0;
    let grade = 0;
    let totalGrades = 1;
    while(!isNaN(grade) && totalGrades <=12){
         if(grade < 4){
            grade = Number(input.shift());
            continue;
         }

         sum += grade;
         grade = Number(input.shift());
         totalGrades++;
    }

    let av = sum / 12;
    console.log(`${name} graduated. Average grade: ${av.toFixed(2)}`)
}

// graduation(['Pesho', 4, 5.5, 6, 5.43, 4.5, 6, 5.55, 5, 6, 6, 5.43, 5])
// graduation(['Ani', 5, 5.32, 6, 5.43, 5, 6, 5.5, 4.55, 5, 6, 5.56, 6])