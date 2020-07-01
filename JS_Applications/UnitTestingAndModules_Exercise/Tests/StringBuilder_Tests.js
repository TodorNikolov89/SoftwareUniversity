const sBuilder = require('../StringBuilder');
const StringBuilder = require('../StringBuilder');
let expect = require('chai').expect;

describe('StringBuilder', () => {
  let testClass;

  //Create instance correctly
  it('Create instance correctly without paramenter', function () {
    expect(() => new StringBuilder()).to.not.throw();
    expect(() => new StringBuilder('test')).to.not.throw();
  })

  //Create instance with invalid parameter 
  it('Create instance with invalid parameter', function () {
    expect(() => new StringBuilder(12412)).to.throw();
  })

  describe('Create instance with empty constructor', function () {
    beforeEach(() => {
      testClass = new StringBuilder();
    })

    //Check for properties
    it('has all properties', function () {
      expect(testClass.hasOwnProperty('_stringArray')).to.equal(true, '_stringArray property is missing')
    })

    //Check for all methods
    it('has all methods', function () {
      expect(Object.getPrototypeOf(testClass).hasOwnProperty('append')).to.equal(true, 'Appent method is missing')
      expect(Object.getPrototypeOf(testClass).hasOwnProperty('prepend')).to.equal(true, 'Prepend method is missing')
      expect(Object.getPrototypeOf(testClass).hasOwnProperty('remove')).to.equal(true, 'Remove method is missing')
      expect(Object.getPrototypeOf(testClass).hasOwnProperty('insertAt')).to.equal(true, 'InsertAt method is missing')
      expect(Object.getPrototypeOf(testClass).hasOwnProperty('toString')).to.equal(true, 'toString method is missing')
    })

    //Creates an empty array
    it('must initialize an empty array', function () {
      expect(testClass._stringArray instanceof Array).to.equal(true, 'Property _stringArray should be type of Array')
    });

    //Length of _stringArray should be 0
    it('Length of _stringArray should be 0', function () {
      expect(testClass._stringArray.length).to.equal(0, 'Length of _stringArray must be 0')
    });
  });

  describe('Create instance with non-empty constructor', function () {
    let param = 'testString'

    beforeEach(() => {
      testClass = new StringBuilder(param);
    });

    it('Initializa property _stringArray', function () {
      expect(testClass._stringArray instanceof Array).to.equal(true, 'Property _stringArray should be type of Array')
      expect(testClass._stringArray.length).to.equal(param.length, `_stringArray Length is incorrect! Should be ${param.length}`)
    });

    //Append
    it('Append should return correct result', function () {
      let newString = 'works';
      testClass.append(newString);
      arraysCompare(testClass._stringArray, Array.from(param + newString))
    });

    //Prepend
    it('Prepend should return correct result', function () {
      let newString = 'works';
      testClass.prepend(newString);
      arraysCompare(testClass._stringArray, Array.from(newString + param))
    });

    //InsertAt
    it('InsertAt should return correct result', function () {
      let newString = 'works';
      testClass.insertAt(newString, 5);
      let expected = Array.from(param);
      expected.splice(5, 0, ...newString)
      arraysCompare(testClass._stringArray, expected)
    });

    //Remove
    it('Remove should return correct result', function () {
      testClass.remove(5, 2);
      let expected = Array.from(param)
      expected.splice(5, 2)
      let actual = testClass._stringArray;
      arraysCompare(actual, expected)
    });

    //toString()
    it('toString should return correct result', function () {
      expect(testClass.toString()).to.equal(param)
    });

    //incorrect Append
    it('Append should return invalid result', function () {
      expect(() => testClass.append(30)).to.throw();
    });

    //incorrect Prepend
    it('Append should return invalid result', function () {
      expect(() => testClass.prepend(30)).to.throw();
    });

    //incorrect indexAt
    it('Append should return invalid result', function () {
      expect(() => testClass.indexAt(30)).to.throw();
    });

  });

  //Compare arrays
  function arraysCompare(actual, expected) {
    expect(actual.length).to.equal(expected.length, 'Arrays are not equal!');
    for (let i = 0; i < actual.length; i++) {
      expect(actual[i]).to.equal(expected[i], `${i} element is different!`)
    }
  }
})