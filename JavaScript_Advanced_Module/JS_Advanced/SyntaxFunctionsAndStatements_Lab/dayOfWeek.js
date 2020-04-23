function dayOfWeek(input) {
    let outputString ;

        switch (input) {
            case 'Monday':
                outputString = 1;
                break;
            case 'Tuesday':
                outputString = 2;
                break;
            case 'Wednesday':
                outputString = 3;
                break;
            case 'Thursday':
                outputString = 4;
                break;
            case 'Friday':
                outputString = 5;
                break;
            case 'Saturday':
                outputString = 6;
                break;
            case 'Sunday':
                outputString = 7;
                break;
                default:
                    outputString = 'error'
        }
 

    console.log(outputString);
}


dayOfWeek('Invalid')