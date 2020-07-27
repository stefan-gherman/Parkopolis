let citiesList = []
$.getJSON('http://localhost:1028/api/cities', function (data) {
    for (var i = 0; i < data.length; i++) {
        citiesList.push(data[i]);
        
    }
    console.log(citiesList);
    populateCities();
});

function populateCities() {
    for (var i = 0; i < citiesList.length; i++) {
        $(".citiesContainer").append(
            `
            <p>ID: ${citiesList[i].id}</p>
            <p>Name: ${citiesList[i].name}</p>
            `
        );
    }
}

