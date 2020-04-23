function wordsUppercase(input) {
    let re = RegExp("[^0-9A-Za-z]+")
    let text = input.split(re);

    text = text.map(function(x){ return x.toUpperCase() })
    console.log(text.join(','))
}


wordsUppercase('Hi, how are you?')