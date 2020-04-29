function sumFirstLast(arr) {
    let first = Number(arr[0]);
    let last = Number(arr[arr.length - 1]);
    let sum = first + last;
    
    console.log(sum); 
}

sumFirstLast(['20', '30', '40'])