function animalType(input) {
    let type = input;
    let animal = '';

    switch (type) {
        case 'dog':
            animal = 'mammal'
            break;
        case 'crocodile':
        case 'tortoise':
        case 'snake':
            animal = 'reptile';
            break;
        default:
            animal = 'unknown'
    }

    console.log(animal)
}

//  animalType('cat')
// animalType('crocodile')
// animalType('snake')
// animalType('dog')