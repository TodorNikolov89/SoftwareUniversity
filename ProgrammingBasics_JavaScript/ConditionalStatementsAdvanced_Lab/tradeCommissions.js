function tradeCommissions(input) {
    
    let town = input[0];
    let sells = Number(input[1]);
    let commission = 0;
    let isTownFound = true;
    let isValidSells = true;

    if (town != "Sofia" && town != "Plovdiv" && town != "Varna") {
        isTownFound = false;
    } else {
        if (sells >= 0 && sells <= 500) {
            if (town === "Sofia") {
                commission = 5;
            } else if (town === "Plovdiv") {
                commission = 5.5;
            } else if (town === "Varna") {
                commission = 4.5;
            }

        } else if (sells > 500 && sells <= 1000) {
            if (town === "Sofia") {
                commission = 7;
            } else if (town === "Plovdiv") {
                commission = 8;
            } else if (town === "Varna") {
                commission = 7.55;
            }
        } else if (sells > 1000 && sells <= 10000) {
            if (town === "Sofia") {
                commission = 8;
            } else if (town === "Plovdiv") {
                commission = 12;
            } else if (town === "Varna") {
                commission = 10;
            }
        } else if (sells > 10000) {
            if (town === "Sofia") {
                commission = 12;
            } else if (town === "Plovdiv") {
                commission = 14.5;
            } else if (town === "Varna") {
                commission = 13;
            }
        } else {
            isValidSells = false;
        }
    }

    if(isValidSells && isTownFound){
        let result = sells * commission/100;
        console.log(result.toFixed(2));
    } else {
        console.log("error")
    }
}


tradeCommissions(["Sofia", "1500"])
tradeCommissions(["Plovdiv", "499.99"])
tradeCommissions(["Varna", "387.45"])
tradeCommissions(["Kaspichan", "-50"])
tradeCommissions(["Plovdiv", "8543.86"])