function isNumberBetween100And200(number){
    if(number<100){
        console.log("Less than 100")
    } else if (number >=100 && number<=200){
        console.log("Between 100 and 200")
    } else {
        console.log("Greater than 200")
    }
}

// isNumberBetween100And200(120)
// isNumberBetween100And200(100)
// isNumberBetween100And200(10)
// isNumberBetween100And200(201)
// isNumberBetween100And200(200)