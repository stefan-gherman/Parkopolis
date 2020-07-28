let allCities = []
getCitiesFromDb();

// Submitting a new city
$("#newCitySubmit").click(async function () {
    console.log($("#cityName").val());
    let newCityName = $("#cityName").val();
    //alert(name);
    let data = { "name": newCityName};
    //data[name] = newCityName;
    console.log(data);
    
    await $.ajax({
        type: "POST",
        url: "http://localhost:1028/api/cities",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            console.log("City added successfully.");
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            alert('fail' + status.code);
        }
    })
    getCitiesFromDb();
});

// Submitting a new Area in a City
$("#newAreaSubmit").click(async function () {
    let cityId = $("#selectCityAddArea").val();
    let newAreaName = await $("#areaNameAddArea").val();

    let data = {

        "name": newAreaName,
        "cityId": cityId,
    }

    $.ajax({
        type: "POST",
        url: `http://localhost:1028/api/cities/${cityId}/areas`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            alert("success");
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            alert('fail' + status.code);
        }
    })
    
    alert("Check DB");
});

// Submitting a new Parking Lot based on Area and City
$("#selectCityAddParkingLot").change(function () {
    populateAreasDropdownAddParkingLot(); 
});

async function populateAreasDropdownAddParkingLot() {
    let cityId = $("#selectCityAddParkingLot").val();
    resetAreasDropdownAddParkingLot();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectAreaAddParkingLot").append(element);
        }
    })
}

function resetAreasDropdownAddParkingLot() {
    let initialElement = `<option value="" disabled selected hidden>Area</option>`;
    $("#selectAreaAddParkingLot").empty();
    $("#selectAreaAddParkingLot").append(initialElement);
}



$("#newParkingLotSubmit").click(async function () {
    let cityId = $("#selectCityAddParkingLot").val();
    let areaId = $("#selectAreaAddParkingLot").val();
    let name = $("#parkingLotNameAddParkingLot").val();

    
    let data = {
        "areaId": areaId,
        "name": name,
        "location": "placeholder for Long/Lat or street",
        "isPaid": true,
        "isStateOwned": false,
        "totalParkingSpaces": 21,
        "hasSecurity": true,
        "userId": 1
    }

    $.ajax({
        type: "POST",
        url: `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            alert("success");
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            alert('fail' + status.code);
        }
    })

    alert("Check DB");
});

async function getCitiesFromDb() {
    await $.getJSON(`http://localhost:1028/api/cities`, function (data) {
        for (var i = 0; i < data.length; i++) {
            allCities.push(data[i]);
        }   
    });
    populateCities();
    populateCitiesSelectAddArea();
    populateCitiesSelectAddParkingLot();
    allCities = [];
}

function populateCities() {
    for (var i = 0; i < allCities.length; i++) {
        $("#citiesContainer").append(
            `<p id="city${allCities[i].id}">${allCities[i].name}</p>`
        );
    }
}

function populateCitiesSelectAddArea() {
    for (var i = 0; i < allCities.length; i++) {
        let element = `<option value="${allCities[i].id}">${allCities[i].name}</option>`;
        $("#selectCityAddArea").append(element);
    }
    
}

function populateCitiesSelectAddParkingLot() {
    for (var i = 0; i < allCities.length; i++) {
        let element = `<option value="${allCities[i].id}">${allCities[i].name}</option>`;
        $("#selectCityAddParkingLot").append(element);
    }
}