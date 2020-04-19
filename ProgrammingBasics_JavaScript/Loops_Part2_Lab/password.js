function password(input){
    let name = input.shift();
    let password = input.shift();

    let data = input.shift();

    while(data !== password){
        data = input.shift();
    }

    console.log(`Welcome ${name}!`);
}