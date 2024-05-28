let boardgames = [];
let customers = [];
let rentals = [];


//Default values
const defaultDescription = "Here you will see the description of the chosen stat...";
const defaultContent = "Here you will see the results...";

//Getting all data and loading the page in

defaultValuesToLoad();
getalldata();

async function getalldata() {
    await fetch('http://localhost:35357/boardgame')
        .then(x => x.json())
        .then(y => {
            boardgames = y;
            //console.log(boardgames);
        });
    await fetch('http://localhost:35357/customer')
        .then(x => x.json())
        .then(y => {
            customers = y;
            //console.log(customers);
        });
    await fetch('http://localhost:35357/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
            //console.log(rentals); 
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
async function getSumOfboardgames() {
    await getalldata();
    //console.log(boardgames.length);
    return boardgames.length;
}
async function getOldestRent() {
    await getalldata();
    const oldest = rentals.sort((a, b) => a.release - b.release)[0];
    //console.log(oldest.gameName);
    return oldest.Name;
}
async function getYoungestRent() {
    await getalldata();
    const youngest = rentals.sort((a, b) => b.release - a.release)[0];
    //console.log(youngest.gameName);
    return youngest.Name;
}
async function getHighestPriceRent() {
    await getalldata();
    const mstExp = rentals.sort((a, b) => b.price - a.price)[0];
    //console.log(mstExp.gameName);
    return mstExp.Name;
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
    const avgRatingText = "This stat shows the average rating of the boardgames.";
    const sumboardgamesText = "This stat shows the sum of the boardgames.";
    const oldestGameText = "This stat shows the oldest game.";
    const youngestGameText = "This stat shows the youngest game.";
    const mstExpText = "This stat shows the most expensive game.";



    if (selectStat() == "avgRating") {
        //console.log(`Average: ${await getAvgRating()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = avgRatingText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getAvgPrice()} is average rating of the boardgames.`;
    } else if (selectStat() == "sumOfboardgames") {
        //console.log(`Sum: ${await getSumOfboardgames()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = sumboardgamesText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getSumOfboardgames()} boardgames in total.`;
    } else if (selectStat() == "oldestGame") {
        //console.log(`Name: ${await getOldestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = oldestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getOldestRent()} is the oldest game.`;
    } else if (selectStat() == "youngestGame") {
        //console.log(`Name: ${await getYoungestGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = youngestRentText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getYoungestRent()} is the youngest game.`;
    } else if (selectStat() == "highestPrice") {
        //console.log(`Name: ${await getHighestPriceGame()}`);
        document.getElementById('statTable').rows[1].cells[0].innerHTML = mstExpText;
        document.getElementById('statTable').rows[2].cells[0].innerHTML = `${await getHighestPriceRent()} is the most expensive game.`;
    }
}

function defaultValuesToLoad() {
    document.getElementById('statTable').rows[1].cells[0].innerHTML = defaultDescription;
    document.getElementById('statTable').rows[2].cells[0].innerHTML = defaultContent;
}