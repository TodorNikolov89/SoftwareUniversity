function cinemaTickets(input) {
    let movie = input.shift();
    let standardSeats = 0;
    let studentSeats = 0;
    let kidSeats = 0;
    let totalTickets = 0;

    while (movie !== 'Finish') {
        let availableSeats = Number(input.shift());
        let seatType = input.shift();
        let takenSeats = 0;
        while (seatType !== 'End') {

            switch (seatType) {
                case 'student':
                    studentSeats++;
                    break;
                case 'standard':
                    standardSeats++;
                    break;
                case 'kid':
                    kidSeats++;
                    break;
            }
            totalTickets++;
            takenSeats++;

            if (takenSeats >= availableSeats) {
                break;
            }
            seatType = input.shift();
        }

        let percentageTakenSeats = takenSeats / availableSeats * 100
        console.log(`${movie} - ${percentageTakenSeats.toFixed(2)}% full.`);

        movie = input.shift();
    }
    let percentageStudents = studentSeats / totalTickets * 100;
    let percentageKids = kidSeats / totalTickets * 100;
    let percentageStandart = standardSeats / totalTickets * 100;

    console.log(`Total tickets: ${totalTickets}`);
    console.log(`${percentageStudents.toFixed(2)}% student tickets.`)
    console.log(`${percentageStandart.toFixed(2)}% standard tickets.`)
    console.log(`${percentageKids.toFixed(2)}% kids tickets.`)
}


// cinemaTickets([
//     'Taxi',
//     10,
//     'standard',
//     'kid',
//     'student',
//     'student',
//     'standard',
//     'standard',
//     'End',
//     'Scary Movie',
//     6,
//     'student',
//     'student',
//     'student',
//     'student',
//     'student',
//     'student',
//     'Finish',
// ])