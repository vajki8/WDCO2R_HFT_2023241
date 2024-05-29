let customers = [];
let connection = null;

setupsignalr();


getdata();

let customerIdtoupdate = -1;

function setupsignalr() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:35357/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on
        (
            "customerCreated", (user, message) => {
                getdata();
            });


    connection.on
        (
            "customerDeleted", (user, message) => {
                getdata();
            });

    connection.on
        (
            "customerUpdated", (user, message) => {
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
    await fetch('http://localhost:35357/customer')
        .then(x => x.json())
        .then(y => {
            customers = y;
            console.log(customers)
            display();
        });
}

function showupdate(id) {
    document.getElementById('custnameupdate').value = customers.find(x => x['customerId'] == id)['customerName'];
    document.getElementById('custageupdate').value = customers.find(x => x['customerId'] == id)['customerAge'];
    document.getElementById('updateformdiv').style.display = 'flex';
    customerIdtoupdate = id;
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    customers.forEach(x => {
        document.getElementById('resultarea').innerHTML += "<tr><td>" + x.customerName + "</td><td>" + x.customerAge + "</td><td>" + `<button type="button" onclick="remove(${x.customerId})">Delete</button>` + `<button type="button" onclick="showupdate(${x.customerId})">Update</button>` + "</td></tr>"
        console.log(x.title);
    });
}

function remove(id) {
    fetch('http://localhost:35357/customer/' + id, {
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
    let nname = document.getElementById("custName").value;
    let aage = document.getElementById("custAge").value;

    fetch('http://localhost:35357/customer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                customerName: nname, customerAge: aage
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
    let nname = document.getElementById('custnameupdate').value;
    let aage = document.getElementById('custageupdate').value;

    fetch('http://localhost:35357/customer', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(
            {
                customerId: customerIdtoupdate, customerName: nname, customerAge: aage
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