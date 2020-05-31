class Ticket {
    constructor(destination, price, status) {
        this.destination = destination;
        this.price = Number(price);
        this.status = status;
    }
}


function solve(towns, criteria) {
    let result = []
    towns.forEach(ticket => {
        let currentTicket = ticket.split("|");
        let newTicket = new Ticket(currentTicket[0], currentTicket[1], currentTicket[2]);
        result.push(newTicket);
    });

    result.sort(compare())



    console.log(result)
}

solve([
    'Philadelphia|94.20|available',
    'New York City|95.99|available',
    'New York City|95.99|sold',
    'Boston|126.20|departed'],
    'destination'
)


objs.sort(compare);