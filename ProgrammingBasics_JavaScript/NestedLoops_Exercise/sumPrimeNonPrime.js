function sumPrimeNonPrime(input) {
    let primeSum = 0;
    let nonPrimeSum = 0;

    for (let element of input) {

        if (element === 'stop') {
            break;
        }

        element = Number(element);
        if (element < 0) {
            console.log(`Number is negative.`);
            continue;
        }

        if (isPrime(element)) {
            primeSum += element;
        } else {
            nonPrimeSum += element;
        }
    }

    console.log(`Sum of all prime numbers is: ${primeSum}`)
    console.log(`Sum of all non prime numbers is: ${nonPrimeSum}`)


    function isPrime(number) {
        if (number === 1) {
            return false;
        }
        else if (number === 2) {
            return true;
        } else {
            for (var x = 2; x < number; x++) {
                if (number % x === 0) {
                    return false;
                }
            }
            return true;
        }
    }
}

// sumPrimeNonPrime([3, 9, 0, 7, 19, 4, "stop"])