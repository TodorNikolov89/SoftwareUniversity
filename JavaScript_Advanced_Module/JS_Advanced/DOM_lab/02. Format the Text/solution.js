function solve() {
  let inputText = document.getElementById("input").innerHTML;
  let arrWithSentences = inputText.split('.');
  arrWithSentences.pop();
  let numberOfParagraphs = Math.ceil(arrWithSentences.length / 3)
  let outputText = document.getElementById("output");
  let counter = 0;
  let currentPar = '';

  for (let i = 0; i < numberOfParagraphs; i++) {
    while (arrWithSentences.length !== 0) {
      currentPar += arrWithSentences.shift() + ".";
      counter++;
      if (counter === 3) {
        break;
      }
    }

    let newParagraph = document.createElement("p");
    newParagraph.style.wordWrap = "break-all";
    newParagraph.innerHTML = currentPar;
    outputText.appendChild(newParagraph);
    currentPar = '';
    counter = 0;
  }
}