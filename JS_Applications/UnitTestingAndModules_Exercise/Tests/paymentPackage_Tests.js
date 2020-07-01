const PaymentPackage = require('../paymentPackage').PaymentPackage;
let expect = require('chai').expect;

describe('PaymentPackage', function () {
    const validName = 'My Package';
    const validValue = 120;


    describe('Instantiation', function () {
        it('Create instance corectly', function () {
            expect(() => new PaymentPackage(validName, validValue)).to.not.throw();
        });

        it('is correctly set up', function () {
            const instance = new PaymentPackage(validName, validValue);
            expect(instance.name).to.equal(validName)
            expect(instance.value).to.equal(validValue)
            expect(instance.VAT).to.equal(20)
            expect(instance.active).to.equal(true)
        })

        it('does not work with invalid name', function () {
            expect(() => new PaymentPackage('', validValue)).to.throw();
            expect(() => new PaymentPackage(undefined, validValue)).to.throw();
            expect(() => new PaymentPackage({}, validValue)).to.throw();
        });

        it('does not work with invalid value', function () {
            expect(() => new PaymentPackage(validName, '')).to.throw();
            expect(() => new PaymentPackage(validName, -1)).to.throw();
            expect(() => new PaymentPackage(validName, [])).to.throw();
        });

        it('has all properties', function () {
            const instance = new PaymentPackage(validName, validValue);
            expect(instance).to.have.property('name');
            expect(instance).to.have.property('value');
            expect(instance).to.have.property('VAT');
            expect(instance).to.have.property('active');
        });

        it('VAT and active are set by default', function () {
            const instance = new PaymentPackage(validName, validValue);
            expect(instance.VAT).to.equal(20)
            expect(instance.active).to.equal(true)
        });
    });

    describe('Accessors', function () {
        let instance;

        beforeEach(() => {
            instance = new PaymentPackage(validName, validValue)
        })

        //name
        it('accepts and sets valid name', function () {
            expect(() => instance.name === 'New Package').to.not.throw();
            instance.name = 'New Package';
            expect(instance.name).to.equal('New Package')
        });

        it('rejects invalid name', function () {
            expect(() => instance.name = '').to.throw('Name must be a non-empty string');
            expect(() => instance.name = undefined).to.throw('Name must be a non-empty string');
            expect(() => instance.name = {}).to.throw('Name must be a non-empty string');
        });

        //value
        it('Accepts and sets valid value', function () {
            instance.value = 90;
            expect(instance.value).to.equal(90)
        });

        it('rejects invalid value', function () {
            expect(() => instance.value = '').to.throw('Value must be a non-negative number');
            expect(() => instance.value = undefined).to.throw('Value must be a non-negative number');
            expect(() => instance.value = -5).to.throw('Value must be a non-negative number');
        });

        //VAT
        it('Accepts and sets valid VAT', function () {
            instance.VAT = 10;
            expect(instance.VAT).to.equal(10)
        });

        it('rejects invalid VAT', function () {
            expect(() => instance.VAT = '').to.throw('VAT must be a non-negative number');
            expect(() => instance.VAT = undefined).to.throw('VAT must be a non-negative number');
            expect(() => instance.VAT = -52).to.throw('VAT must be a non-negative number');
        });

        //active
        it('Accepts and sets valid active', function () {
            instance.active = true;
            expect(instance.active).to.be.true;

            instance.active = false;
            expect(instance.active).to.be.false;
        });

        it('rejects invalid active', function () {
            expect(() => instance.active = '').to.throw('Active status must be a boolean');
            expect(() => instance.active = undefined).to.throw('Active status must be a boolean');
            expect(() => instance.active = -52).to.throw('Active status must be a boolean');
        });
    });

    describe('String info', function () {
        let instance;

        beforeEach(() => {
            instance = new PaymentPackage(validName, validValue)
        });

        it('contains the name', function () {
            expect(instance.toString()).to.contains(validName)
        });

        it('contains the value', function () {
            expect(instance.toString()).to.contains(validValue.toString())
        });

        it('contains the VAT', function () {
            expect(instance.toString()).to.contains(instance.VAT + '%')
        });

        it('inactive lable', function () {
            instance.active = false;
            expect(instance.toString()).to.contains('inactive')
        });

        it('updates info through setters', function () {
            instance.name = 'New Package';
            instance.value = 90;
            instance.VAT = 9;

            const output = instance.toString();
            expect(output).to.contains('New Package')
            expect(output).to.contains('90')
            expect(output).to.contains('9%')
        });
    });
})

