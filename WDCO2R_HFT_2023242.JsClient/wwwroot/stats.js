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
    const oldest = rentals.sort((a, b) => b.timeLeft - a.timeLeft)[0];
    return oldest.name;
}
async function getYoungestRent() {
    await getalldata();
    const youngest = rentals.sort((a, b) => a.timeLeft - b.timeLeft)[0];
    return youngest.name;
}
async function getHighestPrice() {
    await getalldata();
    const highest = rentals.sort((a, b) => b.price - a.price)[0];
    return highest.name;
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
    const avgPriceText = "Atlag kolcsonzesi ar.";
    const sumGamesText = "Hany db jatek.";
    const oldestRentText = "Leghosszabb berles.";
    const youngestRentText = "Legrovidebb berles.";
    const freeGamesText = "legdragabb jatek.";

    if (selectStat() == "avgPrice") {
        document.getElementById('statTable').rows[1].cells[0].innerHTML = avgPriceText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getAvgPrice()}Ft, ennyi atlagosan egy berles most.`;}
     else if (selectStat() == "sumOfGames") {
        //console.log(`Sum: ${await getSumOfGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = sumGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getSumOfGames()}db jatek van eppen berbe adva.`;
    } else if (selectStat() == "oldestRent") {
        //console.log(`Name: ${await getOldestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = oldestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getOldestRent()} a leghosszabb berlesunk.`;
    } else if (selectStat() == "youngestRent") {
        //console.log(`Name: ${await getYoungestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = youngestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getYoungestRent()} a legrovidebb berlesunk.`;
    } else if (selectStat() == "highestPrice") {
        //console.log(`List:\n ${await getFreeGames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = freeGamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getHighestPrice()} a legdragabb berlesunk`;
    }
}