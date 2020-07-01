let mathEnforcer = require('../mathEnforcer').mathEnforcer;
let expect = require('chai').expect

describe('mathEnforcer', function () {
    it('should return correct result with a non-number parameter', function () {
        let result = mathEnforcer.addFive("a");
        expect(result).to.equal(1)
    });
})