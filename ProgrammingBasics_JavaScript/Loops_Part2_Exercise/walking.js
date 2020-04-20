function walking(input) {
    let stepsPerOut = input.shift();
    let totalSteps = 0;
    let outputMessage = 0;
    let lastRun = 0;
    let isGoingHome = false;
    
    while (totalSteps < 10000) {
        if (stepsPerOut === 'Going home') {
            let stepsToHome = Number(input.shift());
            totalSteps += stepsToHome;
            isGoingHome = true;
            break;
        }

        totalSteps += Number(stepsPerOut);
        if (totalSteps >= 10000) {
            outputMessage = 'Goal reached! Good job!';
        }
        stepsPerOut = input.shift();
    }

    if (totalSteps < 10000 && isGoingHome) {
        outputMessage = `${10000 - totalSteps} more steps to reach goal.`;
    } else if (totalSteps >= 10000 && isGoingHome) {
        outputMessage = 'Goal reached! Good job!';
    }

    console.log(outputMessage)
}

// walking([1000, 1500, 2000, 6500])
// walking([1500, 3000, 250, 1548, 2000,"Going home", 2000])
// walking([1500,300,2500,3000,'Going home', 200])