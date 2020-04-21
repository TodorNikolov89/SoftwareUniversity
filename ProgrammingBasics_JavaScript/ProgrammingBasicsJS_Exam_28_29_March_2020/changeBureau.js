function changeBureau(input){
    let bitcoin = Number(input.shift());
    let uans = Number(input.shift());
    let comm = Number(input.shift());

    let leva = bitcoin * 1168;
    leva += uans * 0.15 * 1.76;

    let euros = leva / 1.95;
    euros -= euros * comm/100;

    console.log(euros.toFixed(2))
}

changeBureau([20, 5678,2.4])