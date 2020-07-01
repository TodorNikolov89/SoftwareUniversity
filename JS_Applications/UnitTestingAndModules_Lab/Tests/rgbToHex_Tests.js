let rbgToHex = require('../rgbToHex');

let expect = require("chai").expect;

describe('rgbToHex()', () => {
    describe("Working Cases", () => {
        it('Should return #FFFFFF for (255,255,255)', () => {
            expect(rbgToHex(255, 255, 255)).to.be.equal('#FFFFFF')
        });
        it('Should return #000000 for (0,0,0)', () => {
            expect(rbgToHex(0, 0, 0)).to.be.equal('#000000')
        });
        it('Should return #FF0000 for (255,0,0)', () => {
            expect(rbgToHex(255, 0, 0)).to.be.equal('#FF0000')
        })
    })

    describe("Special Cases", () => {
        it('Should return undefined for(-1,0,0)', () => {
            expect(rbgToHex(-1, 0, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,-1,0)', () => {
            expect(rbgToHex(0, -1, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,0,-1)', () => {
            expect(rbgToHex(0, 0, -1)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,0,{a:1}})', () => {
            expect(rbgToHex(0, 0, { a: 1 })).to.be.equal(undefined)
        });
        it('Should return undefined for(0,{ a: 1 },0)', () => {
            expect(rbgToHex(0, { a: 1 }, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for({ a: 1 },0,0})', () => {
            expect(rbgToHex({ a: 1 }, 0, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for([1],0,0})', () => {
            expect(rbgToHex([1], 0, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,[1],0})', () => {
            expect(rbgToHex(0, [1], 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,0,[1]})', () => {
            expect(rbgToHex(0, 0, [1])).to.be.equal(undefined)
        });

        it('Should return undefined for(2.3,0,0})', () => {
            expect(rbgToHex(2.3, 0, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,2.3,0})', () => {
            expect(rbgToHex(0, 2.3, 0)).to.be.equal(undefined)
        });
        it('Should return undefined for(0,0,2.3})', () => {
            expect(rbgToHex(0, 0, 2.3)).to.be.equal(undefined)
        });
    })
})