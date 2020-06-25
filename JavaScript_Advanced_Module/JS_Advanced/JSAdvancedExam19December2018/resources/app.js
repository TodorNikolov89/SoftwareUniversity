function solution() {
	let fields = document.getElementById('fields');
	let toyType = document.getElementById('toyType');
	let toyPrice = document.getElementById('toyPrice');
	let toyDescription = document.getElementsByTagName('textarea')[0];
	let addBtn = fields.lastElementChild;
	let outputSection = document.getElementById('christmasGiftShop');

	addBtn.addEventListener('click', (e) => {
		
		if (toyType.value === ""
			|| isNaN(Number(toyPrice.value))
			|| toyDescription.value === "" 
			|| toyDescription.value.length < 50) {
			return;
		}

		let divEl = document.createElement('div');
		let imgEl = document.createElement('img');
		let h2El = document.createElement('h2');
		let pEl = document.createElement('p');
		let buyBtn = document.createElement('button');
		let currPrice = Number(toyPrice.value);

		divEl.className = 'gift';
		imgEl.src = 'gift.png';
		h2El.textContent = toyType.value;
		pEl.textContent = toyDescription.value;
		buyBtn.textContent = `Buy it for $${currPrice.toFixed(2)}`

		buyBtn.addEventListener('click', (e) => {
			e.target.parentElement.remove();

		})

		divEl.appendChild(imgEl);
		divEl.appendChild(h2El);
		divEl.appendChild(pEl);
		divEl.appendChild(buyBtn);

		outputSection.appendChild(divEl);


		
	})
}