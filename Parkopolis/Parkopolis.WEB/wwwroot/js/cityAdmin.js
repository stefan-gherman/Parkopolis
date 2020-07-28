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
    let newAreaName = await $("#areaNameAddArea").val();
    //alert(newAreaName);
    let cityIdValue = 0;

    //let data = { "name": newCityName };
    ////data[name] = newCityName;
    //console.log(data);

    //$.ajax({
    //    type: "POST",
    //    url: "http://localhost:1028/api/cities",
    //    data: JSON.stringify(data),
    //    contentType: "application/json; charset=utf-8",
    //    crossDomain: true,
    //    dataType: "json",
    //    success: function () {
    //        alert("success");
    //    },
    //    error: function (jqXHR, status) {
    //        // error handler
    //        console.log(jqXHR);
    //        alert('fail' + status.code);
    //    }
    //})
});

async function getCitiesFromDb() {
    await $.getJSON('http://localhost:1028/api/cities', function (data) {
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