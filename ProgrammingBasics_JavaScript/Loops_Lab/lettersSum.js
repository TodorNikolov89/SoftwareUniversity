function lettersSum(input, controlNumber, budged){
    controlNumber = Number(controlNumber);
    budged = Number(budged);
    let result = 0;
    let money = 0;
    let letters = ["a", "e", "i", "o", "u", "y"];
    let ouputMessage = '';

    for(let ch of input){
        if(letters.includes(ch)){
            money += 3
        } else {
            money += 1;
        }
    }

    result = money * controlNumber;

    if(budged < result){
        ouputMessage = `Cannot buy ${input}. Product value: ${result.toFixed(2)}`;
    } else {
        let difference = budged - result;
        ouputMessage = `${input} bought. Money left: ${difference.toFixed(2)}`;
    }

    console.log(ouputMessage);
}