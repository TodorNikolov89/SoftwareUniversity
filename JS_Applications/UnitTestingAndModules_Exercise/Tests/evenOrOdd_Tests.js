let isOddOrEven = require('../evenOrOdd');
let expect = require('chai').expect
let assert = require('chai').assert

describe('isOddOrEven()', function () {
    describe('Special cases that return undefined', () => {
        it('Should return undefined with a number parameter', function () {
            expect(isOddOrEven(13)).to.equal(undefined, 'Function did not return the correct result')
        });

        it('Should return undefined with an object parameter', function () {
            expect(isOddOrEven({ name: 'Pesho' })).to.equal(undefined, 'Function did not return the correct result')
        });
    });

    describe('Cases that return correct result with even or odd', () => {
        it('Should return correct result with an even length', function () {
            assert.equal(isOddOrEven('roar'), 'even', 'Function did not return the corrent result')
        });

        it('Should return correct result with an odd length', function () {
            assert.equal(isOddOrEven('yep'), 'odd', 'Function did not return the corrent result')
        });

        it('Should return correct values with multiple consecutive checks', function () {
            assert.equal(isOddOrEven('yep'), 'odd', 'Function did not return the corrent result')
            assert.equal(isOddOrEven('yoo'), 'odd', 'Function did not return the corrent result')
            assert.equal(isOddOrEven('what'), 'even', 'Function did not return the corrent result')
        });
    })
})
