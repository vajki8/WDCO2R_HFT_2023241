let boardgames = [];
let connection = null;

let boardgameIdToUpdate = -1;

setupSignalR();

defaultValuesToLoad()

getdata();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35357/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("boardgameCreated", (user, message) => {
        getdata();
    });
    connection.on("boardgameDeleted", (user, message) => {
        getdata();
    });
    connection.on("boardgameUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:35357/boardgame')
        .then(x => x.json())
        .then(y => {
            boardgames = y;
            //console.log(boardgames);
            display()
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    boardgames.forEach(t => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + t.boardgameId + "</td><td>" +
            t.boardgameName + "</td><td>" +
            `<button type="button" onclick="remove(${t.boardgameId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.boardgameId})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}

function showupdate(id) {
    document.getElementById('boardgameNameToUpdate').value = boardgames.find(t => t['boardgameId'] == id)['boardgameName']
    document.getElementById('formdiv').style.display = 'none';
    document.getElementById('updateformdiv').style.display = 'flex';
    boardgameIdToUpdate = id;
}
function update() {
    document.getElementById('formdiv').style.display = 'flex';
    document.getElementById('updateformdiv').style.display = 'none';

    let name = document.getElementById('boardgameNameToUpdate').value;

    fetch('http://localhost:35357/boardgame', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { boardgameId: boardgameIdToUpdate, boardgameName: name }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
function remove(id) {
    fetch('http://localhost:35357/boardgame/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success', data);
            getdata();
        })
        .catch((error) => { console.log('Error:', error); });
}
function create() {
    let name = document.getElementById('boardgameName').value;

    fetch('http://localhost:35357/boardgame', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { boardgameName: name }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function defaultValuesToLoad() {
    document.getElementById('boardgameName').placeholder = "Enter your boardgame name here...";
}