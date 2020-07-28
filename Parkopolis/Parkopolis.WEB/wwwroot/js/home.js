populateCitiesDropdown();

$("#selectCityHome").change(function () {
    populateAreasDropdown();
    // clear parkinglots and spaces eventually
});

$("#selectAreaHome").change(function () {
    populateParkingLotsDropdown();
});

async function populateCitiesDropdown() {
    await $.getJSON('http://localhost:1028/api/cities', function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectCityHome").append(element);
        }
    });
    
}

async function populateAreasDropdown() {
    let cityId = $("#selectCityHome").val();
    let initialElement = `<option value="" disabled selected hidden>Area</option>`;
    $("#selectAreaHome").empty();
    $("#selectAreaHome").append(initialElement);
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectAreaHome").append(element);
        }
    })
}

async function populateParkingLotsDropdown() {
    let cityId = $("#selectCityHome").val();
    let areaId = $("#selectAreaHome").val();
    let initialElement = `<option value="" disabled selected hidden>Parking Lot</option>`;
    $("#selectParkingLotHome").empty();
    $("#selectParkingLotHome").append(initialElement);
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectParkingLotHome").append(element);
        }
    })
}