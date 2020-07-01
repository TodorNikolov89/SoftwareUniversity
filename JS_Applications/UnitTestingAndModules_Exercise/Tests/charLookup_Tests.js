let lookupChar = require('../charLookup');
let expect = require('chai').expect

describe('lookupchar()', function () {
    //Testing cases that return undefined
    describe('Cases that return undefined', function () {
        it('Should return undefined with non-string as first parameter', function () {
            expect(lookupChar(12, 41)).to.equal(undefined)
        });

        it('Should return undefined with non-number as second parameter', function () {
            expect(lookupChar("test", "some")).to.equal(undefined)
        });

        it('Should return undefined with floating-pointnumber as a second parameter', function () {
            expect(lookupChar("test", 3.14)).to.equal(undefined)
        });
    });
    //Testing cases that return Incorrect index
    describe('Cases that return Incorrect index', function () {
        it('Should return incorrect index with an incorrect index value', function () {
            expect(lookupChar('Todor', 10)).to.equal('Incorrect index')
        });

        it('Should return incorrect index with an negative index value', function () {
            expect(lookupChar('Todor', -10)).to.equal('Incorrect index')
        });

        it('Should return incorrect index with with value equal to string length', function () {
            expect(lookupChar('Todor', 5)).to.equal('Incorrect index')
        });
    });
    //Testing cases that return correct result
    describe('Cases that return correct result', function () {
        it("Should return correct result with correct parameters", function () {
            expect(lookupChar('Todor', 1)).to.equal('o')
        });

        it("Should return correct result with correct parameters", function () {
            expect(lookupChar('Pesho', 0)).to.equal('P')
        });
    });
})