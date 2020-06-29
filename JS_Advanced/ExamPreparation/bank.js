class Bank {
    constructor(bankName) {
        this._bankName = bankName
        this.allCustomers = [];

    }

    newCustomer(customer) {
        if (this.allCustomers.some(c => c.personalId === customer.personalId)) {
            throw new Error(`${customer.firstName} ${customer.lastName} is already our customer!`)
        }

        let newCustomer = Object.assign(customer)
        this.allCustomers.push(customer);

        return newCustomer;
    }

    depositMoney(personalId, amount) {
        if (!this.allCustomers.some(c => c.personalId === personalId)) {
            throw new Error(`We have no customer with this ID!`)
        }

        let customer = this.allCustomers.find(c => c.personalId === personalId)
        if (isNaN(customer.totalMoney)) {
            customer.totalMoney = amount;
        } else {
            customer.totalMoney += amount;
        }
        if (!Array.isArray(customer.transactions)) {
            customer.transactions = [];
        }
        let message = `${customer.transactions.length + 1}. ${
            customer.firstName
        } ${customer.lastName} made deposit of ${amount}$!`;
        customer.transactions.push(message);
        return `${customer.totalMoney}$`;
    }

    withdrawMoney(personalId, amount) {
        let customer = this.allCustomers.find((c) => {
            return c.personalId === personalId;
        });
        if (customer === undefined) {
            throw new Error('We have no customer with this ID!');
        } else {
            if (isNaN(customer.totalMoney)) {
                customer.totalMoney = amount;
            }
            if (!Array.isArray(customer.transactions)) {
                customer.transactions = [];
            }
            if (customer.totalMoney < amount) {
                throw new Error(`${customer.firstName} ${customer.lastName} does not have enough money to withdraw that amount!`);
            } else {
                customer.totalMoney -= amount;
 
                let message = `${customer.transactions.length + 1}. ${
                    customer.firstName
                } ${customer.lastName} withdrew ${amount}$!`;
 
                customer.transactions.push(message);
 
                return `${customer.totalMoney}$`;
            }
        }
    }

    customerInfo(personalId) {
        if (!this.allCustomers.some(c => c.personalId === personalId)) {
            throw new Error(`We have no customer with this ID!`)
        }

        let customer = this.allCustomers.find(c => c.personalId === personalId);

        let result = `Bank name: ${this._bankName}\n`
        result += `Customer name: ${customer.firstName} ${customer.lastName}\n`
        result += `Customer ID: ${customer.personalId}\n`
        result += `Total Money: ${customer.totalMoney}$\n`
        result += 'Transactions:\n'

        
        for (let index = customer.transactions.length - 1; index >= 0; index--) {
            result += customer.transactions[index];
            if (index !== 0) {
                result += `\n`;
            }
        }
        return result;
    }
}



let bank = new Bank("SoftUni Bank");
let customer1 = bank.newCustomer({ firstName: 'Svetlin', lastName: 'Nakov', personalId: 1111111 });
let customer2 = bank.newCustomer({ firstName: 'Mihaela', lastName: 'Mileva', personalId: 3333333 });
let totalMoney2 = bank.depositMoney(1111111, 250);
let totalMoney1 = bank.depositMoney(1111111, 250);
let totalMoney3 = bank.depositMoney(3333333, 555);
let totalMoney4 = bank.withdrawMoney(1111111, 125);
console.log(bank.customerInfo(1111111));

let expected = `Bank name: SoftUni Bank
Customer name: Svetlin Nakov
Customer ID: 1111111
Total Money: 375$
Transactions:
3. Svetlin Nakov withdrew 125$!
2. Svetlin Nakov made deposit of 250$!
1. Svetlin Nakov made deposit of 250$!`;
console.log(expected.length)
console.log(expected)
