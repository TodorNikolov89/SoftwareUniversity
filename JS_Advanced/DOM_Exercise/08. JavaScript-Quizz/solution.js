function solve() {
  let quizzie = document.getElementById("quizzie");
  let sections = document.getElementsByTagName('section');
  let rigthAnswers = ['onclick', 'JSON.stringify()', 'A programming API for HTML and XML documents']
  let correctAnswers = 0;
  let steps = 0;
  let result = document.querySelector(".results-inner h1")

  quizzie.addEventListener('click', (e) => {
    let answer = e.target.innerHTML;
    if (e.target.className === "answer-text") {

      sections[steps].style.display = 'none';

      if (rigthAnswers.includes(answer)) {
        correctAnswers++;
      }
      steps++;
      if (steps < 3) {
        sections[steps].style.display = 'block';
      }

      if (steps == 3) {
        document.getElementById('results').style.display='block'
        let outputMessage = correctAnswers === 3 ? 'You are recognized as top JavaScript fan!' : `You have ${correctAnswers} right answers`
        result.innerHTML = outputMessage;
      }
    }

  })
}
