function areaCalculator(shape, val1, val2){
    let area = 0;
    if(shape ==="triangle"){
        area = (val1 * val2)/2;
    } else if(shape==="square"){
        area  = Math.pow(val1, 2);
    } else if (shape === "rectangle"){
        area = val1 * val2;
    } else if(shape==="circle") {
        area = Math.PI * Math.pow(val1,2)
    }

    console.log(area.toFixed(3));
}

// areaCalculator("square", 5)
// areaCalculator("triangle", 4.5, 20)
// areaCalculator("rectangle", 7, 2.5)
// areaCalculator("circle", 6)