function solve() {
  let resultText = document.querySelectorAll('textarea')[1];
  let body = document.getElementsByTagName('tbody')[0];
  let buttons = document.getElementsByTagName('button');
  let generateBtn = buttons[0];

  generateBtn.addEventListener('click', generate)
  function generate() {
    let inputItems = JSON.parse(document.querySelectorAll('textarea')[0].value);

    for (const item of inputItems) {
      let row = document.createElement('tr')

      let image = document.createElement('td');
      image.innerHTML = `<img src ="${item.img}">`
      row.appendChild(image);

      let name = document.createElement('td');
      let p1 = document.createElement('p');
      p1.textContent = item.name;
      name.appendChild(p1);
      row.appendChild(name);

      let price = document.createElement('td');
      let p2 = document.createElement('p');
      p2.textContent = item.price;
      price.appendChild(p2);
      row.appendChild(price);

      let decFactor = document.createElement('td');
      let p3 = document.createElement('p');
      p3.textContent = item.decFactor;
      decFactor.appendChild(p3);
      row.appendChild(decFactor);

      let checkBox = document.createElement('td')
      let input = document.createElement('input');
      input.setAttribute('type', 'checkbox');
      checkBox.appendChild(input);
      row.appendChild(checkBox);

      body.appendChild(row);
    }
  }

  let buyBtn = buttons[1];

  let totalPrice = 0;
  let averageDecarationFactor = 0;
  let names = [];
  let count = 0;

  buyBtn.addEventListener('click', result)

  function result() {
    let rows = Array.from(document.getElementsByTagName('tr'));
    for (let i = 2; i < rows.length; i++) {
      if (rows[i].children[4].children[0].checked) {
        names.push(rows[i].children[1].textContent);
        totalPrice += Number(rows[i].children[2].textContent)
        averageDecarationFactor += Number(rows[i].children[3].textContent);
        count++;
      }
    }
    resultText.value = `Bought furniture: ${names.join(', ')}\nTotal price: ${totalPrice.toFixed(2)}\nAverage decoration factor: ${averageDecarationFactor / count}`;

  }
}