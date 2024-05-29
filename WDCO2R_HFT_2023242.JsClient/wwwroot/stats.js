let boardgames = [];
let customers = [];
let rentals = [];


//Getting all data and loading the page in

getalldata();

async function getalldata() {
    await fetch('http://localhost:35357/boardgame')
        .then(x => x.json())
        .then(y => {
            boardgames = y;
        });
    await fetch('http://localhost:35357/customer')
        .then(x => x.json())
        .then(y => {
            customers = y;
        });
    await fetch('http://localhost:35357/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
        });
}

//NON-CRUD Methods
async function getAvgPrice() {
    await getalldata();
    const arr = [];
    for (var i = 0; i < rentals.length; i++) {
        arr[i] = rentals[i].price;
    }
    const avg = arr.reduce((a, b) => a + b, 0) / arr.length;
    return avg.toFixed(2);
}
async function getSumOfGames() {
    await getalldata();
    return boardgames.length;
}
async function getOldestRent() {
    await getalldata();
    const oldest = rentals.sort((a, b) => a.timeLeft - b.timeLeft)[0];
    return oldest.name;
}
async function getYoungestRent() {
    await getalldata();
    const youngest = boardgames.sort((a, b) => b.release - a.release)[0];
    return youngest.title;
}
async function getHighestPriceGame() {
    await getalldata();
    const highp = rentals.sort((a, b) => b.price - a.price)[0];
    return highp.customerName;
}
async function getFreeGames() {
    await getalldata();
    const freeGames = games.filter(game => game.price === 0).map(game => game.gameName);
    //console.log(freeGames);
    return freeGames.join(" - ");
}





//Display, loading, helping functions

function selectStat() {
    let statSelect = document.getElementById('statSelection');
    let selectedStat = statSelect.options[statSelect.selectedIndex].value;
    console.log(selectedStat);
    return selectedStat;
}

async function contentDecider() {
    //Description contents
    const avgPriceText = "This stat shows the average rating of the games.";
    const sumGamesText = "This stat shows the sum of the games.";
    const oldestRentText = "This stat shows the oldest game.";
    const youngestRentText = "This stat shows the youngest game.";
    const mstExpText = "This stat shows the most expensive game.";
    const freeGamesText = "This stat shows the list of free games.";

    if (selectStat() == "avgPrice") {
        //console.log(`Average: ${await getAvgRating()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = avgPriceText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getAvgPrice()} is average price of the rents.`;
    } else if (selectStat() == "sumOfGames") {
        //console.log(`Sum: ${await getSumOfGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = sumGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getSumOfGames()} games in total.`;
    } else if (selectStat() == "oldestGame") {
        //console.log(`Name: ${await getOldestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = oldestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getOldestGame()} is the oldest game.`;
    } else if (selectStat() == "youngestGame") {
        //console.log(`Name: ${await getYoungestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = youngestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getYoungestGame()} is the youngest game.`;
    } else if (selectStat() == "highestPrice") {
        //console.log(`Name: ${await getHighestPriceGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = mstExpText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getHighestPriceGame()} is the most expensive game.`;
    }
    else if (selectStat() == "freeGames") {
        //console.log(`List:\n ${await getFreeGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = freeGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getFreeGames()}`;
    }
}