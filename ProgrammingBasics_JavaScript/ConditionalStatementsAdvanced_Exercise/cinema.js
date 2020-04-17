function cinema(movieType, rows, cols){
    rows = Number(rows)
    cols = Number(cols)
    let price = 0;
    let money = 0;

    switch(movieType){
        case "Premiere":
            price = 12.00;
            money = rows * cols * price;
        break;
        case "Normal":
            price = 7.50;
            money = rows * cols * price;
        break;
        case "Discount":
            price = 5.00;
            money = rows * cols * price;
        break;
        default:
    }

    console.log(`${money.toFixed(2)} leva`)
}


// cinema("Premiere", 10, 12)
// cinema("Normal", 21, 13)
// cinema("Discount", 12, 30)