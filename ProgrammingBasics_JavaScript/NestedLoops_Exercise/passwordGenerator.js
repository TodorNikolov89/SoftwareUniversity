function passwrordGenerator(input) {
    let n = Number(input.shift());
    let l = Number(input.shift());

    // let i = 97; ASCII Table
    // let j = 123; ASCII Table
    let str = '';

    for (let i = 1; i < n; i++) {
        for (let j = 1; j < n; j++) {
            for (let a = 97; a < 97 + l; a++) {
                for (let b = 97; b < 97 + l; b++) {
                    for (let k = i > j ? i + 1 : j + 1; k <= n; k++) {
                        str += i + "" + j + "" + String.fromCharCode(a) + "" + String.fromCharCode(b) + "" + k + " "                        
                    }
                }
            }
        }
    }

    console.log(str);
}

// passwrordGenerator([2, 4])
// passwrordGenerator([3, 1])
// passwrordGenerator([4, 2])