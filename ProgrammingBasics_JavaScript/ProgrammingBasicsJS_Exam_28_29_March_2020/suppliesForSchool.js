function suppliesForSchool(input){
    let pens = Number(input.shift());
    let markers = Number(input.shift());
    let cleaner = Number(input.shift());
    let discount = Number(input.shift());

    let totalMoney = pens * 5.80 + markers * 7.20 + cleaner * 1.20;
    totalMoney -= totalMoney * discount / 100;

    console.log(totalMoney.toFixed(3))
}

suppliesForSchool([2,3,2.5,25])