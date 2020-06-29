function solve() {
   let addForm = document.getElementById('add-new');
   let addBtn = addForm.lastElementChild;
   let availableProducts = document.getElementById('products').children[1];
   let myProducts = document.getElementById('myProducts').children[1];
   let filterBtn = document.getElementsByClassName('filter')[0].lastElementChild;
   let totalPriceString = document.getElementsByTagName('h1')[1];//string
   let totalPrice = 0;
   let buySection = document.getElementById('myProducts');
   let buyBtn = buySection.lastElementChild;


   addBtn.addEventListener('click', (e) => {
      e.preventDefault();

      let name = addForm.children[1].value;
      let quantity = Number(addForm.children[2].value);
      let price = Number(addForm.children[3].value);

      let li = document.createElement('li');
      let span = document.createElement('span');
      span.textContent = name;
      let strong = document.createElement('strong');
      strong.textContent = `Available: ${quantity}`;
      let button = document.createElement('button');
      button.textContent = `Add to Client's List`;

      let strong1 = document.createElement('strong');
      strong1.textContent = price.toFixed(2);
      let div = document.createElement('div');

      li.appendChild(span);
      li.appendChild(strong);
      div.appendChild(strong1);
      div.appendChild(button);
      li.appendChild(div);
      availableProducts.appendChild(li);

      button.addEventListener('click', (e) => {
         if (quantity - 1 === 0) {
            li.remove();
         } else {
            quantity--;
            strong.textContent = `Available: ${quantity}`;
         }

         let newLi = document.createElement('li');
         let newStrong = document.createElement('strong');
         newStrong.textContent = price.toFixed(2);
         newLi.textContent = name;
         newLi.appendChild(newStrong);
         myProducts.appendChild(newLi);

         totalPrice += price;
         totalPriceString.textContent = `Total Price: ${totalPrice.toFixed(2)}`
      })
   })

   filterBtn.addEventListener('click', (e) => {
      let filterInput = document.getElementById('filter').value;
      filterInput = filterInput.toLowerCase();
      let arr = Array.from(availableProducts.children)
      for (const prod of arr) {
         if (!prod.firstElementChild.innerText.toLowerCase().includes(filterInput.toLowerCase())) {
            prod.style.display = 'none'
         } else if (filterInput.toLowerCase() === "") {
            prod.style.display = 'block'
         }
      }
   })

   buyBtn.addEventListener('click', () => {
      totalPriceString.textContent = `Total Price: 0.00`
      myProducts.innerHTML = "";
   })
}