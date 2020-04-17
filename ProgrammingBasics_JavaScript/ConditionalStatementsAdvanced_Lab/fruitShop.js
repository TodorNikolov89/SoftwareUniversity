function fruitShop(fruit, day, amount) {
    amount = Number(amount);
    let price = 0;
    if (day != "Monday" && day != "Tuesday" && day != "Wednesday" && day != "Thursdau" && day != "Friday" && day != "Saturday" && day != "Sunday" ) {
        price = "error"
    } else {
        switch (fruit) {
            case "apple":
                if (day === "Sunday" || day === "Saturday") {
                    price = 1.25;
                } else {
                    price = 1.20;
                }
                break;
            case "banana":
                if (day === "Sunday" || day === "Saturday") {
                    price = 2.70;
                } else {
                    price = 2.50
                }
                break;
            case "orange":
                if (day === "Sunday" || day === "Saturday") {
                    price = 0.90;
                } else {
                    price = 0.85;
                }
                break;
            case "grapefruit":
                if (day === "Sunday" || day === "Saturday") {
                    price = 1.60;
                } else {
                    price = 1.45
                }
                break;
            case "kiwi":
                if (day === "Sunday" || day === "Saturday") {
                    price = 3.00;
                } else {
                    price = 2.70;
                }
                break;
            case "pineapple":
                if (day === "Sunday" || day === "Saturday") {
                    price = 5.60;
                } else {
                    price = 5.50;
                }
                break;
            case "grapes":
                if (day === "Sunday" || day === "Saturday") {
                    price = 4.20;
                } else {
                    price = 3.85
                }
                break;
            default:
                price = "error";
        }
    }
    if (price === "error") {
        console.log("error")
    } else {
        let money = amount * price;
        console.log(money.toFixed(2))
    }
}

// fruitShop("apple", "Tuesday", 2)
// fruitShop("orange", "Sunday", 3)
// fruitShop("tomato", "Monday", 0.5)