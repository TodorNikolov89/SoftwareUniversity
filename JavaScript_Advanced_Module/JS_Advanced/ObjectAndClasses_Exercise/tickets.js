function solve(tickets, criteria) {
    class Ticket {
        constructor(destination, price, status) {
            this.destination = destination;
            this.price = Number(price);
            this.status = status;
        }
    }
    let result = []

    for (let i = 0; i < tickets.length; i++) {
        let currentTicket = tickets[i].split("|");
        let newTicket = new Ticket(currentTicket[0], currentTicket[1], currentTicket[2]);
        result.push(newTicket);
    }
    
    return result.sort(compare)

    function compare(a, b) {
        if (a[criteria] < b[criteria]) {
            return -1;
        }
        if (a[criteria] > b[criteria]) {
            return 1;
        }
        return 0;
    }
}

// solve(['Philadelphia|94.20|available',
//     'New York City|95.99|available',
//     'New York City|95.99|sold',
//     'Boston|126.20|departed'],
//     'status'
// )

console.log(solve(['Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'],
    'destination'
))