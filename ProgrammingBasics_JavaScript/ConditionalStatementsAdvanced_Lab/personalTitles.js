function personalTitle(age, gender) {
    age = Number(age);
    let output = '';
    switch (gender) {
        case 'm':
            if (age >= 16) {
                output = 'Mr.';
            } else {
                output = 'Master';
            }
            break;
        case 'f':
            if (age >= 16) {
                output = 'Ms.';
            } else {
                output = 'Miss';
            }
            break;
    }

    console.log(output)
}

// personalTitle(12,'f')
// personalTitle(17, 'm')