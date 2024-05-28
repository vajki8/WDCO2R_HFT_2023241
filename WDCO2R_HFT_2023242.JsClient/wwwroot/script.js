let rentals = [];
let connection = null;
let noncrudarray = [];

let rentalIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35357/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("rentalCreated", (user, message) => {
        getdata();
    });

    connection.on("rentalDeleted", (user, message) => {
        getdata();
    });

    connection.on("rentalUpdated", (user, message) => {
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
    await fetch('http://localhost:35357/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
            display();
        });
}


function display() {
    document.getElementById('resultarea').innerHTML = "";
    rentals.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.rentalId + "</td><td>"
            + t.rentalName + "</td><td>" +
            `<button type="button" onclick="remove(${t.rentalId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.rentalId})">Update</button>`
            + "</td></tr>";
    });
}

function showupdate(id) {
    document.getElementById('rentalnametoupdate').value = rentals.find(t => t['rentalId'] == id)['rentalName'];
    document.getElementById('updateformdiv').style.display = 'flex';
    rentalIdToUpdate = id;
}

function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('rentalnametoupdate').value;
    fetch('http://localhost:35357/rental', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                rentalName: name,
                rentalId: rentalIdToUpdate
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

function create() {
    let name = document.getElementById('rentalname').value;
    fetch('http://localhost:35357/rental', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                rentalName: name
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

function remove(id) {
    fetch('http://localhost:35357/rental/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function noncrud() {
    fetch('http://localhost:35357/MatchStat/AverageOddsByrental')
        .then(x => x.json())
        .then(y => {
            noncrudarray = y;
            var table = document.getElementById("table");
            table.innerHTML = "";
            table.style.width = "100%";
            var tr = document.createElement("tr");
            var th1 = document.createElement("th");
            var th2 = document.createElement("th");
            th1.innerHTML = "Name";
            th2.innerHTML = "Odds";
            tr.appendChild(th1);
            tr.appendChild(th2);
            table.appendChild(tr);
            for (var i = 0; i < y.length; i++) {
                var tr = document.createElement("tr");
                var td1 = document.createElement("td");
                var td2 = document.createElement("td");
                td1.innerHTML = y[i].name;
                td2.innerHTML = y[i].odds;
                tr.appendChild(td1);
                tr.appendChild(td2);
                table.appendChild(tr);
            }
        });
}