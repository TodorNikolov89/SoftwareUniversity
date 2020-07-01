let mathEnforcer = require('../mathEnforcer').mathEnforcer;
let expect = require('chai').expect

describe('mathEnforcer', function () {

    describe('addFive Tests', function () {
        it('should return undefined with a non-number parameter', function () {
            let result = mathEnforcer.addFive("a");
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        it('should return undefined with an object as an input', function () {
            let result = mathEnforcer.addFive({ a: 1 });
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        it('should return undefined with an array as inpot', function () {
            let result = mathEnforcer.addFive([1, 2, 3]);
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        //Correct result
        it('should return 10 with input parameter 5', function () {
            let result = mathEnforcer.addFive(5);
            expect(result).to.equal(10)
        });

        it('should return 8.14 with input parameter 3.14', function () {
            let result = mathEnforcer.addFive(3.14);
            expect(isCorrect(result, 8.14)).to.equal(true)
        });

        it('should return 1.86 with input parameter -3.14', function () {
            let result = mathEnforcer.addFive(-3.14);
            expect(isCorrect(result, 1.86)).to.equal(true)
        });
    });

    describe('subtractTen Tests', function () {
        it('should return undefined with a non-number parameter', function () {
            let result = mathEnforcer.subtractTen("a");
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        it('should return undefined with an object as input', function () {
            let result = mathEnforcer.subtractTen({ a: 1 });
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        it('should return undefined with an array as input', function () {
            let result = mathEnforcer.subtractTen([1, 2, 3]);
            expect(result).to.equal(undefined, 'Parameter should be a number')
        });

        //Correct result
        it('should return 10 with input parameter -10', function () {
            let result = mathEnforcer.subtractTen(20);
            expect(result).to.equal(10)
        });

        it('should return -6.86 with input parameter 3.14', function () {
            let result = mathEnforcer.subtractTen(3.14);
            expect(isCorrect(result, -6.86)).to.equal(true)
        });

        it('should return -13.14 with input parameter -3.14', function () {
            let result = mathEnforcer.subtractTen(-3.14);
            expect(isCorrect(result, -13.14)).to.equal(true)
        });
    });

    describe('sum Tests', function () {
        it('should return undefined with a non-number parameter as first parameter', function () {
            let result = mathEnforcer.sum("a", 1);
            expect(result).to.equal(undefined, 'First parameter should be a number')
        });

        it('should return undefined with a non-number parameter as second parameter', function () {
            let result = mathEnforcer.sum(1, 'a');
            expect(result).to.equal(undefined, 'Second parameter should be a number')
        });

        it('should return undefined with a non-number parameters ', function () {
            let result = mathEnforcer.sum('a', 'a');
            expect(result).to.equal(undefined, 'Both parameters should be a number')
        });

        //Correct result
        it('should return correct result with input parameter 11, 12', function () {
            let result = mathEnforcer.sum(11, 12);
            expect(result).to.equal(23)
        });

        it('should return correct result with input parameter -10,-20 ', function () {
            let result = mathEnforcer.sum(-10, -20);
            expect(isCorrect(result, -30)).to.equal(true)
        });

        it('should return correct result with input parameter -12.3,20.1', function () {
            let result = mathEnforcer.sum(-12.3, 20.1);
            expect(isCorrect(result, 7.8)).to.equal(true)
        });

        it('should return correct result with input parameter -12.3,-20.1', function () {
            let result = mathEnforcer.sum(-12.3, -20.1);
            expect(isCorrect(result, -32.4)).to.equal(true)
        });
    })

    function isCorrect(result, value) {
        if (Math.abs(result - value) <= 0.01) {
            return true;
        }

        return false
    }
})