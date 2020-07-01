let isSymetric = require(`../checkForSymmetry`);
const isSymmetric = require('../checkForSymmetry');
let expect = require('chai').expect;

describe('isSymetric() should return true or false', () => {
    it('Should return false 123', () => {
        let result = isSymmetric(123);
        expect(result).equals(false, 'Result should be false!')
    })

    it('Should return true for [1,2,1] ', () => {
        let result = isSymmetric([1, 2, 1]);
        expect(result).equals(true, 'Result should be true!')
    })
  //  C:/Users/Todor's PC/AppData/Roaming/npm/node_modules/mocha/bin/_mocha
    it('Should return true for [0]', () => {
        let result = isSymmetric([0]);
        expect(result).equals(true, 'Result should be true!')
    })

    it("Should return false for ['1','2','2'] ", () => {
        let result = isSymmetric(['1', '2', '2']);
        expect(result).equals(false, 'Result should be false!')
    })

    it("Should return true ['a','v','a']", () => {
        let result = isSymmetric(['a', 'v', 'a']);
        expect(result).equals(true, 'Result should be true!')
    })

    it("Should return true [-1, 2,-1]", () => {
        let result = isSymmetric([-1, 2, - 1]);
        expect(result).equals(true, 'Result should be true!')
    })
})