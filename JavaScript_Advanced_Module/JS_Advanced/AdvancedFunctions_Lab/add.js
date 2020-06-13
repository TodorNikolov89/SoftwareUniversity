function solution(n) {
    n = Number(n);

    return function (number) {
        return Number(number) + n;
    }
}

let add7 = solution(7);
console.log(add7(2));
console.log(add7(3));
