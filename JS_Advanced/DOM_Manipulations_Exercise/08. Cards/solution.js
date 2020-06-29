function solve() {
   let allCards = document.querySelectorAll('img');
   let resultDiv = document.getElementById('result');
   let history = document.getElementById('history');
   let leftSpan = resultDiv.children[0];
   let rightSpan = resultDiv.children[2];
   let counter = 0;
   let firstPlayer;
   let secondPlyer;
   let resultHTML = [];

   Array.from(allCards).forEach(element => {
      element.addEventListener('click', () => {
         let hand = '';
         element.src = "images/whiteCard.jpg";
         counter++;

         if (element.parentElement.id === 'player1Div') {
            firstPlayer = element;
            leftSpan.textContent = element.name;
         } else {
            rightSpan.textContent = element.name;
            secondPlyer = element;
         }

         if (counter === 2) {
            let leftNumber = Number(leftSpan.textContent)
            let rightNumber = Number(rightSpan.textContent)
            if (leftNumber > rightNumber) {
               firstPlayer.style.border = "2px solid green";
               secondPlyer.style.border = "2px solid red";
            } else {
               secondPlyer.style.border = "2px solid green";
               firstPlayer.style.border = "2px solid red";
            }

            hand = `[${firstPlayer.name} vs ${secondPlyer.name}]`
            resultHTML.push(hand);

            history.textContent = resultHTML.join(' ') + ' '
            leftSpan.textContent = "";
            rightSpan.textContent = "";
            
            counter = 0;
         }
      })
   });
}