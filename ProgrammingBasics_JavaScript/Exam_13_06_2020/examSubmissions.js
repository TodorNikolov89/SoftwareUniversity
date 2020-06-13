function solve(students, problems) {
     students = Number(students)
     problems = Number(problems);

    let totalSubmitions = students * Math.ceil(problems * 2.8);
    totalSubmitions += students * Math.floor(problems / 3)

    let totalMemory = 5 * Math.ceil(totalSubmitions / 10)
    console.log(`${totalMemory} KB needed`)
    console.log(`${totalSubmitions} submissions`)

}

solve(11, 7)
solve(25,10)