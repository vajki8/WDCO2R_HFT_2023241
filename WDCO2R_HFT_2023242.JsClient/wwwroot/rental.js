let rentals = [];
let connection = null;

setupsignalr();


getdata();

let rentalIdtoupdate = -1;

function setupsignalr() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35357/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        (
            "rentalCreated", (user, message) => {
                getdata();
            });


    connection.on
        (
            "rentalDeleted", (user, message) => {
                getdata();
            });

    connection.on
        (
            "rentalUpdated", (user, message) => {
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
    await fetch('http://localhost:35357/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
            console.log(rentals)
            display();
        });
}

function showupdate(id) {
    document.getElementById('rentnameupdate').value = rentals.find(x => x['rentalId'] == id)['name'];
    document.getElementById('rentpriceupdate').value = rentals.find(x => x['rentalId'] == id)['price'];
    document.getElementById('renttimeupdate').value = rentals.find(x => x['rentalId'] == id)['timeLeft'];
    document.getElementById('rentgameupdate').value = rentals.find(x => x['rentalId'] == id)['boardGame'];
    document.getElementById('updateformdiv').style.display = 'flex';
    rentalIdtoupdate = id;
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    rentals.forEach(x => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + x.name + "</td><td>" + x.price + "</td><td>" + x.timeLeft + "</td><td>" + `<button type="button" onclick="remove(${x.rentId})">Delete</button>` + `<button type="button" onclick="showupdate(${x.rentId})">Update</button>` + "</td></tr>"
        console.log(x.title);
    });
}

function remove(id) {
    fetch('http://localhost:35357/rental/' + id, {
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
    let nname = document.getElementById("rentname").value;
    let pprice = document.getElementById("rentprice").value;
    let ttime = document.getElementById("renttime").value;
    let ggame = document.getElementById("rentgame").value;

    fetch('http://localhost:35357/rental', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                name: nname, price: pprice, boardGame: ggame, timeLeft: ttime
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
    let nname = document.getElementById("rentname").value;
    let pprice = document.getElementById("rentprice").value;
    let ttime = document.getElementById("renttime").value;
    let ggame = document.getElementById("rentgame").value;

    fetch('http://localhost:35357/rental', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                rentalId: rentalIdtoupdate, rentalName: nname, rentalPrice: pprice, rentalBoardGame: ggame, rentalTime: ttime
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