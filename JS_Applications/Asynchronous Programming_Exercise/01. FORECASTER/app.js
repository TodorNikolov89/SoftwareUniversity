import * as data from './data.js';
import el from './dom.js';

const symbols = {
    'Sunny': '&#x2600;', // ☀	
    'Partly sunny': '&#x26C5;', //⛅
    'Overcast': '&#x2601;', // ☁
    'Rain': '&#x2614;', // ☂
    'Degrees': '&#176;'  // 
}

window.addEventListener('load', () => {
    const input = document.querySelector('#location');
    const todayDiv = document.querySelector('#current');
    const mainDiv = document.querySelector('#forecast');
    const upcomingDiv = document.querySelector('#upcoming');
    document.querySelector('#submit').addEventListener('click', getForecast)

    async function getForecast() {
        const locationName = input.value;
        let code = ''
        try {
            code = await data.getCode(locationName)

        } catch (err) {
            input.value = 'Error'
            return;
        }
        const todayP = data.getToday(code);
        const upcomingP = data.getUpcoming(code);

        const [today, upcoming] = [
            await todayP,
            await upcomingP
        ]

        const symbolSpan = el('span', '', { className: 'condition symbol' });
        symbolSpan.innerHTML = symbols[today.forecast.condition];

        const tempSpan = el('span', '', { className: 'forecast-data' });
        tempSpan.innerHTML = `${today.forecast.low}${symbols.Degrees}/${today.forecast.high}${symbols.Degrees}`

        todayDiv.appendChild(el('div', [
            symbolSpan,
            el('span', [
                el('span', today.name, { className: 'forеcast-data' }),
                tempSpan,
                el('span', today.forecast.condition, { className: 'forеcast-data' })
            ], { className: 'condition' })
        ], { className: 'forеcast' }));

        const forecasetInfoDiv = el('div', upcoming.forecast.map(renderUpcoming), { className: 'forecast-info' });

        upcomingDiv.appendChild(forecasetInfoDiv)

        mainDiv.style.display = 'block'
    }

    function renderUpcoming(forecast) {
        const symbolSpan = el('span', '', { className: 'symbol' });
        symbolSpan.innerHTML = symbols[forecast.condition];

        const tempSpan = el('span', '', { className: 'forecast-data' });
        tempSpan.innerHTML = `${forecast.low}${symbols.Degrees}/${forecast.high}${symbols.Degrees}`

        const result = el('span', [
            symbolSpan,
            tempSpan,
            el('span', forecast.condition, { className: 'forecast-data' })
        ], {
            className: 'upcoming'
        });

        return result;
    }

})


