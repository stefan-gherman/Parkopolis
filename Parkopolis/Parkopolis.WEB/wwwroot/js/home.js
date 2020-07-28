populateCitiesDropdown();

$("#selectCityHome").change(function () {
    populateAreasDropdown();
    resetParkingLotsDropdown();
    $("#resultingParkingSpaces").empty();
});

$("#selectAreaHome").change(function () {
    populateParkingLotsDropdown();
});

$("#selectParkingLotHome").change(function () {
    populateResultingParkingSapces();
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
    resetAreasDropdown();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectAreaHome").append(element);
        }
    })
}

function resetAreasDropdown(){
    let initialElement = `<option value="" disabled selected hidden>Area</option>`;
    $("#selectAreaHome").empty();
    $("#selectAreaHome").append(initialElement);
}

async function populateParkingLotsDropdown() {
    let cityId = $("#selectCityHome").val();
    let areaId = $("#selectAreaHome").val();
    resetParkingLotsDropdown();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectParkingLotHome").append(element);
        }
    })
}

function resetParkingLotsDropdown() {
    let initialElement = `<option value="" disabled selected hidden>Parking Lot</option>`;
    $("#selectParkingLotHome").empty();
    $("#selectParkingLotHome").append(initialElement);
}

async function populateResultingParkingSapces() {
    $("#resultingParkingSpaces").empty();
    let cityId = $("#selectCityHome").val();
    let areaId = $("#selectAreaHome").val();
    let parkingLotId = $("#selectParkingLotHome").val();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `
                        <li class="list-group-item">
                            <div class="container">
                                <div class="d-flex justify-content-between">
                                    <p>${data[i].name}</p>
                                    <p>
                                        <span>Wash</span> <span>Roof</span> <span>Security</span> <span>10 RON</span>
                                    </p>
                                    <a href="#" class="btn btn-success">Take</a>
                                </div>
                            </div>
                        </li> `;
            $("#resultingParkingSpaces").append(element);
        }
    })
}

