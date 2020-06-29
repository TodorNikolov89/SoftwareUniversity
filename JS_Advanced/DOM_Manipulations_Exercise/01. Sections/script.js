function create(words) {
   let contentElement = document.getElementById('content');

   for (let i = 0; i < words.length; i++) {
      let newP = document.createElement('p');
      newP.textContent = words[i];
      newP.style.display = 'none'

      let newDiv = document.createElement('div');
      newDiv.appendChild(newP);
      newDiv.addEventListener('click', showParagraph)

      contentElement.appendChild(newDiv);
   }
   
   function showParagraph() {
      let currentParagraph = this.firstChild;
      currentParagraph.style.display = 'block';
   }

}