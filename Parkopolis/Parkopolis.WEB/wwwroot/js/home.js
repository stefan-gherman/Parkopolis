// CURRENTLY PAYING FOR PARKING SPACE DISPLAY
async function generateWarningCardOnTop(cityId, areaId, parkingLotId, parkingSpaceId) {
    let largeUrl = `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces/${parkingSpaceId}`;
    let payingParkingSpaceName = "";
    let payingParkingSpaceHasCarWash = false;
    let payingParkingSpaceCityName = "";
    let payingParkingSpaceAreaName = "";
    let payingParkingSpaceParkingLotName = "";
    let payingParkingSpacePrice = 0.0;
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}`, function (data) {
        payingParkingSpaceCityName = data.name;
    });
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}`, function (data) {
        payingParkingSpaceAreaName = data.name;
    });
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}`, function (data) {
        payingParkingSpaceParkingLotName = data.name;
    });
    await $.getJSON(largeUrl, function (data) {
        payingParkingSpaceName = data.name;
        payingParkingSpaceHasCarWash = data.hasCarWash;
        payingParkingSpacePrice = data.price;
    });
    let orderCarWashElement = "";
    if (payingParkingSpaceHasCarWash) {
        orderCarWashElement = `<a href="#">Car Wash pricing and details</a>`
    }

    let element = `
                    <div class="card text-white bg-warning mb-3" style="max-width: 18rem;">
                      <div class="card-header">${payingParkingSpaceParkingLotName}</div>
                      <div class="card-body">
                        <h5 class="card-title">${payingParkingSpaceName} - ${payingParkingSpacePrice} RON/h</h5>
                        <p class="card-text">${payingParkingSpaceCityName} - ${payingParkingSpaceAreaName}</p>
                        <p class="card-text">${orderCarWashElement}</p>
                        <p class="card-text">Contact Security - 555 212 1911</p>
                        <p><a href="#">Google Maps Directions</a><p>
                        <p><button id="leaveParkingSpaceButton" class="btn btn-danger">Leave Parking Space</button><p>
                      </div>
                    `
    $("#payingParkingSpaceContainer").append(element);
    $("#leaveParkingSpaceButton").click(function () {
        $("#payingParkingSpaceContainer").empty();
        setParkingSpaceToFree(largeUrl);
    });
}

async function setParkingSpaceToFree(largeUrl) {
    await $.getJSON(largeUrl, async function (data) {
        data =
            [
                {
                    "op": "replace",
                    "path": "/isTaken",
                    "value": false
                }
            ]
        await $.ajax({
            type: "PATCH",
            url: largeUrl,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            dataType: "json",
            success: function () {
                console.log("Spot freed-up successfully.");
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
                alert('fail' + status.code);
            }
        })
        // refreshing the Parking Spaces only
        populateResultingParkingSapces();
    });
};


// MAIN SELECTION CONTENT CITY > AREA > PARKINLOT > PARKINGSPACE
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
            let status = "Take";
            let buttonClass = "btn btn-success"
            if (data[i].isTaken == false) {
                status = "Take";
                buttonClass = "btn btn-success"
            }
            else {
                status = "Taken";
                buttonClass = "btn btn-danger"
            }
            let hasCarWash = "";
            if (data[i].hasCarWash == true) {
                hasCarWash = `<span><img src="img/carWash.png" alt="Car Wash" height="30"/></span>`
            }
            let hasCover = "";
            if (data[i].isCovered == true) {
                hasCover = `<span><img src="img/carCover.png" alt="Car Cover" height="30"/></span>`
            }
            let price = data[i].price;
            let hasSecurity = `<span><img src="img/carSecurityGuard.png" alt="Security" height="30"/></span>`
            let element = `
                        <li class="list-group-item">
                            <div class="container">
                                <div class="d-flex justify-content-between">
                                    <p>${data[i].name}</p>
                                    <p>
                                        ${hasCarWash} ${hasCover} ${hasSecurity} <span>${price} RON</span>
                                    </p>
                                    <button id="parkingSpace${data[i].id}" class="${buttonClass}">${status}</button>
                                </div>
                            </div>
                        </li> `;
            $("#resultingParkingSpaces").append(element);    
        } 
    });
    $("[class*=btn][class*=btn-success]").click(function () {
        let parkingSpaceId = this.id.replace("parkingSpace", "");
        handleTakeParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId)
    });
    
}

//Take parking space button pressed
async function handleTakeParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId) {
    // first get the parking space for data
    let URL = `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces/${parkingSpaceId}`;

    // the action when the user presses Take
    await $.getJSON(URL, async function (data) {
        
        data = 
            [
                {
                    "op": "replace",
                    "path": "/isTaken",
                    "value": true
                }
            ]
        
        await $.ajax({
            type: "PATCH",
            url: URL,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            crossDomain: true,
            dataType: "json",
            success: function () {
                console.log("Spot taken successfully.");
            },
            error: function (jqXHR, status) {
                // error handler
                console.log(jqXHR);
                alert('fail' + status.code);
            }
        })
        // refreshing the Parking Spaces only
        populateResultingParkingSapces();
        generateWarningCardOnTop(cityId, areaId, parkingLotId, parkingSpaceId);
    });

    


    //console.log("reached handle function too");
    //async function () {
    //    console.log($("#cityName").val());
    //    let newCityName = $("#cityName").val();
    //    //alert(name);
    //    let data = { "name": newCityName };
    //    //data[name] = newCityName;
    //    console.log(data);

    //    await $.ajax({
    //        type: "POST",
    //        url: "http://localhost:1028/api/cities",
    //        data: JSON.stringify(data),
    //        contentType: "application/json; charset=utf-8",
    //        crossDomain: true,
    //        dataType: "json",
    //        success: function () {
    //            console.log("City added successfully.");
    //        },
    //        error: function (jqXHR, status) {
    //            // error handler
    //            console.log(jqXHR);
    //            alert('fail' + status.code);
    //        }
    //    })
    //    getCitiesFromDb();
}