function solve(incomeMoney, avarageGrade, minSalary) {
    let income = Number(incomeMoney);
    let avGrade = Number(avarageGrade);
    let salary = Number(minSalary);

    let socialScholarship = Math.floor(salary * 0.35);
    let excellentScholarship = Math.floor(avGrade * 25);
    let ouputMessage = `You cannot get a scholarship!`;

    //Social
    if (avGrade <= 4.50) {
        ouputMessage = `You cannot get a scholarship!`;
    }
    if (income <= salary && avGrade > 4.50) {
        ouputMessage = `You get a Social scholarship ${socialScholarship} BGN`;
    }

    //Excellent
    if (avGrade >= 5.50) {
        if (excellentScholarship < socialScholarship) {
            ouputMessage = `You get a Social scholarship ${socialScholarship} BGN`;
        } else {
            ouputMessage = `You get a scholarship for excellent results ${excellentScholarship} BGN`;
        }
    }


    console.log(ouputMessage);
}

solve(480.00, 4.60, 450.00)
solve(300.00, 5.65, 420.00)