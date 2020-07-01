let sum = require('../sumOfNumbers');
let expect = require('chai').expect;

describe('sum(arr) sum array of numbers', () => {
    it('Should return 5', () => {
        let result = sum([1, 2, 3, -1]);
        expect(result).to.be.equal(5, 'The sum shoul be 5')
    });

    it('Should return 6', () => {
        let result = sum([1, 2, 3]);
        expect(result).to.be.equal(6, 'The sum shoul be 6')
    })

    it('Should return invalid data', () => {
        let result = sum('invalid DAta');
        expect(result).to.be.NaN
    })
})