// const url = 'https://localhost:7150/';
// const btnBid = document.querySelector('#bidBtn');
// const countDownLabel = document.querySelector('#countDownLabel');
// let currentBid = 0;
// let timeInterval;


// const connection = new signalR.HubConnectionBuilder()
//     .withUrl(url + 'hubs/offers')
//     .configureLogging(signalR.LogLevel.Information)
//     .build();


// async function start() {
//     try {
//         await connection.start();

//         const element = document.querySelector('#offervalue');

//         $.get(url + 'api/offer', function (data, status) {
//             currentBid = data;
//             element.innerHTML = 'Begin price: ' + data + '$';
//         });


//         console.log('SignalR started')

//     } catch (error) {
//         console.log(err);
//         setTimeout(() => {
//             start();
//         }, 5000);
//     }
// }

// start();
// const infoElement = document.querySelector('#info');
// const disconnectInfoElement = document.querySelector('#disconnectinfo');


// connection.on("ReceiveConnectInfo", (message) => {
//     infoElement.innerHTML = message;
// })

// connection.on("ReceiveDisconnectInfo", (message) => {
//     disconnectInfoElement.innerHTML = message;
// })

// connection.on("ReceiveMessage", (message, data) => {
//     let element = document.querySelector('#responseOfferValue');
//     currentBid = data;
//     element.innerHTML = message + data + '$';
//     clearInterval(timeInterval);
//     countDownLabel.innerHTML = ''
//     btnBid.disabled = false;
// })

// connection.on("EndBid", (message, data) => {
//     btnBid.disabled = true;
//     let element = document.querySelector('#responseOfferValue');
//     element.innerHTML = message + data + '$';
//     if (timeInterval)
//         clearInterval(timeInterval);
//     countDownLabel.innerHTML = ''
// })


// async function IncreaseOffer() {
//     let user = document.querySelector('#user');
//     let element = document.querySelector('#responseOfferValue');
//     element.innerHTML = 'Your bid is ' + (currentBid + 100)
//     btnBid.disabled = true;
//     let countDown = 10;

//     countDownLabel.innerHTML = `${countDown--} seconds left`;
//     timeInterval = setInterval(() => {
//         countDownLabel.innerHTML = `${countDown--} seconds left`;
//         console.log(countDown);
//         if (countDown == -1) {
//             connection.invoke('EndBid', user.value, currentBid)
//             clearInterval(timeInterval)
//         }
//     }, 1000);

//     $.get(url + 'api/Offer/Increase?data=100', function (data, status) {
//         $.get(url + 'api/Offer', function (data, status) {
//             connection.invoke('SendMessage', user.value, data);
//         })
//     })

// }



var CURRENT_ROOM = "";
var totalSeconds = 10;
var currentUser = "";
var room = document.querySelector('#room');
var element = document.querySelector('#offerValue');
var timeSection = document.querySelector('#time-section');
var time = document.querySelector('#time');
var button = document.querySelector('#offerBtn');
var roomnames = ['Chevrolet', 'BMW', 'Mercedes'];

const url = 'https://localhost:7150/';
const connection = new signalR.HubConnectionBuilder()
    .withUrl(url + 'hubs/offers')
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function startConnection() {
    try {
        await connection.start();
        console.log('SignalR started')

    } catch (error) {
        console.log(err);
        setTimeout(() => {
            start();
        }, 5000);
    }
}

async function start() {
    try {
        $.get(url + 'api/offer/Room?room=' + CURRENT_ROOM, function (data, status) {
            currentBid = data;
            element.innerHTML = 'Begin price: ' + data + '$';
        });

    } catch (error) {
        console.log(err);
        setTimeout(() => {
            start();
        }, 5000);
    }
}

async function JoinRoom(roomName) {
    CURRENT_ROOM = roomName;
    room.style.display = 'block';
    await start();

    currentUser = document.querySelector('#user').value;
    await connection.invoke("JoinRoom", CURRENT_ROOM, currentUser);
    CreateRoomButtons();
}

async function LeaveRoom(roomName) {
    room.style.display = 'none';
    
    currentUser = document.querySelector('#user').value;
    await connection.invoke("LeaveRoom", CURRENT_ROOM, currentUser);
    CURRENT_ROOM = '';
    CreateRoomButtons();
    let infoUser = document.querySelector('#info');
    infoUser.innerHTML = '';
}



connection.on('ReceiveJoinInfo', (message) => {
    let infoUser = document.querySelector('#info');
    infoUser.innerHTML = message + " connected to our room";
})

connection.on('ReceiveLeaveInfo', (message) => {
    let infoUser = document.querySelector('#info');
    infoUser.innerHTML = message + " disconnected from our room";
})

connection.on('ReceiveRandomMessage', (message) => {
    const randomMessageSpan = document.querySelector('#randomMessage');
    randomMessageSpan.innerHTML = message;
    setInterval(() => {
        randomMessageSpan.innerHTML = ''
    }, 5000);
})

connection.on('RoomIsFull', () => {
    CURRENT_ROOM = '';
    CreateRoomButtons();
})


async function IncreaseOffer() {
    const user = document.querySelector('#user');
    $.get(
        url + `api/Offer/IncreaseRoom?room=${CURRENT_ROOM}&data=500`,
        function (data, status) {
            $.get(url + `api/Offer/Room?room=${CURRENT_ROOM}`,
                async function (data, status) {
                    var element2 = document.querySelector('#offerValue2');
                    element2.innerHTML = 'Your offer is ' + data + '$';

                    await connection.invoke('SendMessageRoom', CURRENT_ROOM, user.value);
                }
            )
        }
    )
}

connection.on('ReceiveInfoRoom', (user, data) => {
    var element2 = document.querySelector('#offerValue2');
    element2.innerHTML = user + `'s offer is: ${data}$`;
})

function CreateRoomButtons() {
    let buttons = ''
    roomnames.forEach(element => {
        console.log(element + ' ' + CURRENT_ROOM)
        console.log(element != CURRENT_ROOM)
        if (element != CURRENT_ROOM)
            buttons += `<button style="margin: 5px" onclick="JoinRoom('${element}')">Join ${element}</button>`
        else
            buttons += `<button style="margin: 5px" onclick="LeaveRoom('${element}')">Leave ${element}</button>`
    });
    const sectionElement = document.querySelector('#joinRoomButtons');
    sectionElement.innerHTML = buttons;
}

startConnection();
CreateRoomButtons();
