let boardgames = [];
let connection = null;

setupsignalr();


getdata();

let boardGameIdtoupdate = -1;

function setupsignalr() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35357/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        (
            "boardgameCreated", (user, message) => {
                getdata();
            });


    connection.on
        (
            "boardgameDeleted", (user, message) => {
                getdata();
            });

    connection.on
        (
            "boardgameUpdated", (user, message) => {
                getdata();
            });

    connection.onclose
        (async () => {
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
            console.log(boardgames)
            display();
    });
}

function showupdate(id) {
    document.getElementById('gametitleupdate').value = boardgames.find(x => x['boardGameId'] == id)['title'];
    document.getElementById('gametypeupdate').value = boardgames.find(x => x['boardGameId'] == id)['type'];
    document.getElementById('updateformdiv').style.display = 'flex';
    boardGameIdtoupdate = id;
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    boardgames.forEach(x => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + x.boardGameId + "</td><td>" + x.title + "</td><td>" + x.type + "</td>" + `<button type="button" onclick="remove(${x.boardGameId})">Delete</button>` + `<button type="button" onclick="showupdate(${x.boardGameId})">Update</button>` + "</td></tr>"
        console.log(x.title);
    });
}

function remove(id) {
    fetch('http://localhost:35357/boardgame/' + id, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function create() {
    let ttitle = document.getElementById("gametitle").value;
    let ttype = document.getElementById("gametype").value;

    fetch('http://localhost:35357/boardgame', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                type: ttype,
                title: ttitle
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });


}

function update() {
    document.getElementById('updateformdiv').style = 'none';
    let ttitle = document.getElementById('gametitleupdate').value;
    let ttype = document.getElementById('gametypeupdate').value;

    fetch('http://localhost:35357/boardgame', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                boardGameId: boardGameIdtoupdate, title: ttitle, type: ttype
            }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => {
            console.error('Error:', error);
        });


}
