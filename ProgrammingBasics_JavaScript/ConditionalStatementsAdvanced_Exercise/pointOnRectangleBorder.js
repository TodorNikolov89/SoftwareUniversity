function pointOnRectangleBorder(x1, y1, x2, y2, x, y){
    x1 =Number(x1);
    y1 = Number(y1);
    x2 = Number(x2);
    y2 = Number(y2);
    x = Number(x);
    y = Number(y);
    let isInsideOutside = true;
    //Border
    if(x >= x1 && x <= x2){
        if(y === y1 || y === y2){
            isInsideOutside = false;
        }
    } 
    
    if(y >= y1 && y <= y2){
        if(x === x1 || x === x2){
            isInsideOutside = false;
        }
    }    


    if(isInsideOutside){
        console.log("Inside / Outside")
    } else {
        console.log("Border")
    }
}

// pointOnRectangleBorder(2, -3, 12, 3, 8, -1)
// pointOnRectangleBorder(2, -3, 12, 3, 12, -1)