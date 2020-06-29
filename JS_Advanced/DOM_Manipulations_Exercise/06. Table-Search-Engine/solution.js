function solve() {
   let searchBtn = document.querySelector('#searchBtn');
   let tableBody =Array.from(document.getElementsByTagName('tbody')[0].children);
   let searchField = document.getElementById('searchField');

   searchBtn.addEventListener('click', function () {
      let regex = new RegExp(searchField.value, 'gim');
      tableBody.map(e => {
         e.classList.remove('select');
         if (e.innerHTML.match(regex) !== null) {
            e.className = 'select';
         }
      });
      searchField.value = '';
   });
}