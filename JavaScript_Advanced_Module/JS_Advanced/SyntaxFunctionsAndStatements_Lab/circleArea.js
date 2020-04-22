function circleArea(arg) {
    let radius;
    let outputMessage;
    let inputType = typeof arg;

    if (inputType !== 'number') {
        outputMessage = `We can not calculate the circle area, because we receive a ${typeof arg}.`;
    } else {

        radius = arg;
        let area = Math.PI * Math.pow(radius, 2);
        outputMessage = area.toFixed(2);
    }


    console.log(outputMessage);

}

circleArea(5);
circleArea('name')