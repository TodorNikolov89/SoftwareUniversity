function onTimeForTheExam(examHour, examMinutes, arriveHour, arriveMinutes) {
    examHour = Number(examHour);
    examMinutes = Number(examMinutes);
    arriveHour = Number(arriveHour);
    arriveMinutes = Number(arriveMinutes);
    let outputMessage = ``;
    var examTime = new Date(0, 0, 0, examHour, examMinutes, 0, 0);
    var arriveTime = new Date(0, 0, 0, arriveHour, arriveMinutes, 0, 0);

    var difference = Math.abs(arriveTime - examTime)
    let minutes = Math.floor((difference / (1000 * 60)) % 60);
    let hours = Math.floor((difference / (1000 * 60 * 60)) % 24);

    //Format time
    formatedHours = hours > 0 ? hours + ":" : "";
    if (hours >= 1) {
        formatedMinutes = minutes > 9 ? minutes : '0' + minutes;
    } else {
        formatedMinutes = minutes;
    }

    if (examTime.getUTCHours() === arriveTime.getUTCHours() && examTime.getUTCMinutes() === arriveTime.getUTCMinutes()) {
        outputMessage = `On time`
    } else if (examTime > arriveTime && difference <= 1800000) {
        outputMessage = `On time\n${formatedMinutes} minutes before the start`;
    } else if (examTime > arriveTime && difference > 1800000) {
        outputMessage = `Early\n${formatedHours}${formatedMinutes} ${hours >= 1 ? 'hours' : 'minutes'} before the start`
    } else if (examTime < arriveTime) {
        outputMessage = `Late\n${formatedHours}${formatedMinutes} ${hours >= 1 ? 'hours' : 'minutes'} after the start`;
    }

    console.log(outputMessage)
}

// onTimeForTheExam(9, 30, 9, 50)
// onTimeForTheExam(9, 00, 8, 30)
// onTimeForTheExam(16, 00, 15, 00)
// onTimeForTheExam(9, 00, 10,30)
// onTimeForTheExam(14, 00, 13, 55)
// onTimeForTheExam(11, 30, 8, 12)
// onTimeForTheExam(10, 00, 10, 00)
// onTimeForTheExam(11, 30, 10, 55)
// onTimeForTheExam(11, 30, 12, 29)