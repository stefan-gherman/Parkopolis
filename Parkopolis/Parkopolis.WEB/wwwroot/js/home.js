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
            let element = `
                        <li class="list-group-item">
                            <div class="container">
                                <div class="d-flex justify-content-between">
                                    <p>${data[i].name}</p>
                                    <p>
                                        <span>Wash</span> <span>Roof</span> <span>Security</span> <span>10 RON</span>
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