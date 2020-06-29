function solve() {
   let searchButton = document.getElementById('searchBtn');
   let tableBody = document.getElementsByTagName('tbody')[0].children;
   console.log(tableBody)
   searchButton.addEventListener('click', () => {
      for (let row of tableBody) {
         if (row.className = 'select') {
            row.className = '';
         }
      }
      let text = document.getElementById('searchField').value;

      for (let row of tableBody) {
         if (row.innerHTML.includes(text)) {
            row.className = 'select'
         }
      }
      console.log(text)
   })
}