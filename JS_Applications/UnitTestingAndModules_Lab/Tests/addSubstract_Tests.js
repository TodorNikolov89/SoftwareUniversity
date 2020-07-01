let expect = require("chai").expect;
let assert = require("chai").assert;
let createCalculator = require('../addSubstract').createCalculator;

describe("CreateCalculator()", () => {
    let calc;

    beforeEach("Create calc", function () {
        calc = createCalculator();
    })

    describe('Add() cases', function () {
        it('Should add(2) and return 2 for get()', () => {
            calc.add(2);
            let result = calc.get();
            expect(result).to.be.equal(2)
        });

        it('Should add(0.1) and return 0.1 for get()', () => {
            calc.add(0.1);
            let result = calc.get();
            expect(result).to.be.equal(0.1)
        });

        it('Should add(-2) and return -1 for get()', () => {
            calc.add(-2);
            let result = calc.get();
            expect(result).to.be.equal(-2)
        });

        it('Should add("10") and return 10 for get()', () => {
            calc.add("10");
            let result = calc.get();
            expect(result).to.be.equal(10)
        });

        it('Should add("a10") and return NaN for get()', () => {
            calc.add("a10");
            let result = calc.get();
            expect(result).to.be.NaN
        });
    })

    describe('Subtract() Cases', function () {
        it('Should subtract(2) and return -2 for get()', () => {
            calc.subtract(2);
            let result = calc.get();
            expect(result).to.be.equal(-2)
        });

        it('Should subtract(0.1) and return 0.1 for get()', () => {
            calc.subtract(0.1);
            let result = calc.get();
            expect(result).to.be.equal(-0.1)
        });

        it('Should subtract(-2) and return 2 for get()', () => {
            calc.subtract(-2);
            let result = calc.get();
            expect(result).to.be.equal(2)
        });

        it('Should add("-10") and return 10 for get()', () => {
            calc.subtract("-10");
            let result = calc.get();
            expect(result).to.be.equal(10)
        });

        it('Should add("a10") and return NaN for get()', () => {
            calc.subtract("a10");
            let result = calc.get();
            expect(result).to.be.NaN
        });
    })

})
