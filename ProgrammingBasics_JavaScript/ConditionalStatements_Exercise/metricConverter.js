function metricConverter(inputNumber, inputDim1, inputDim2){
    let number = Number(inputNumber)
    let convertFrom = String(inputDim1)
    let convertTo = String(inputDim2)

    let result = 0;
    if(convertFrom === "mm"){
        if(convertTo==="cm"){
            result = number/10;
        } else if(convertTo==="m"){
            result = number/1000;
        }
    } else if(convertFrom ==="cm"){
        if(convertTo==="m"){
            result = number/100;
        } else if(convertTo==="mm"){
            result = number*10;
        }
    } else if(convertFrom ==="m"){
        if(convertTo==="mm"){
            result = number*1000;
        } else if(convertTo==="cm"){
            result = number*100;
        }
    } 
    console.log(result.toFixed(3))
}

// metricConverter(12, "mm", "m")