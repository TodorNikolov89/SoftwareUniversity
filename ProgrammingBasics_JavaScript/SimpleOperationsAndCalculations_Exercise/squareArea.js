function getSquareAreaAndPerimetter(x1,y1,x2,y2){
    let width = Math.abs(x1-x2);
    let heigth = Math.abs(y1-y2);

    let area = width * heigth;
    let perimetter = 2*width + 2*heigth;

    console.log(area.toFixed(2));
    console.log(perimetter.toFixed(2))
}

// getSquareAreaAndPerimetter(60,20,10,50)
// getSquareAreaAndPerimetter(30,40,70,-10)
// getSquareAreaAndPerimetter(600.25, 500.75,100.50,-200.5)