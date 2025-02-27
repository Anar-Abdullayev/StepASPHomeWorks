const url = 'https://localhost:7150/';
const btnBid = document.querySelector('#bidBtn');
const countDownLabel = document.querySelector('#countDownLabel');
let currentBid = 0;
let timeInterval;


const connection = new signalR.HubConnectionBuilder()
    .withUrl(url + 'hubs/offers')
    .configureLogging(signalR.LogLevel.Information)
    .build();


async function start() {
    try {
        await connection.start();

        const element = document.querySelector('#offervalue');

        $.get(url + 'api/offer', function (data, status) {
            currentBid = data;
            element.innerHTML = 'Begin price: ' + data + '$';
        });


        console.log('SignalR started')

    } catch (error) {
        console.log(err);
        setTimeout(() => {
            start();
        }, 5000);
    }
}

start();
const infoElement = document.querySelector('#info');
const disconnectInfoElement = document.querySelector('#disconnectinfo');


connection.on("ReceiveConnectInfo", (message) => {
    infoElement.innerHTML = message;
})

connection.on("ReceiveDisconnectInfo", (message) => {
    disconnectInfoElement.innerHTML = message;
})

connection.on("ReceiveMessage", (message, data) => {
    let element = document.querySelector('#responseOfferValue');
    currentBid = data;
    element.innerHTML = message + data + '$';
    clearInterval(timeInterval);
    countDownLabel.innerHTML = ''
    btnBid.disabled = false;
})

connection.on("EndBid", (message, data) => {
    btnBid.disabled = true;
    let element = document.querySelector('#responseOfferValue');
    element.innerHTML = message + data + '$';
    if (timeInterval)
        clearInterval(timeInterval);
    countDownLabel.innerHTML = ''
})


async function IncreaseOffer() {
    let user = document.querySelector('#user');
    let element = document.querySelector('#responseOfferValue');
    element.innerHTML = 'Your bid is ' + (currentBid + 100)
    btnBid.disabled = true;
    let countDown = 10;

    countDownLabel.innerHTML = `${countDown--} seconds left`;
    timeInterval = setInterval(() => {
        countDownLabel.innerHTML = `${countDown--} seconds left`;
        console.log(countDown);
        if (countDown == -1) {
            connection.invoke('EndBid', user.value, currentBid)
            clearInterval(timeInterval)
        }
    }, 1000);

    $.get(url + 'api/Offer/Increase?data=100', function (data, status) {
        $.get(url + 'api/Offer', function (data, status) {
            connection.invoke('SendMessage', user.value, data);
        })
    })

}