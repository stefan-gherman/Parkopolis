

populateCitiesDropdown();

$("#selectCityParkingAdmin").change(function () {
    populateAreasDropdown();
    resetParkingLotsDropdown();
    $("#parkingSpaceList").empty();
});

$("#selectAreaParkingAdmin").change(function () {
    populateParkingLotsDropdown();
});

$("#selectParkingLotParkingAdmin").change(function () {
    populateResultingParkingSapces();
});

//populateParkingLot();

async function populateCitiesDropdown() {
    await $.getJSON('http://localhost:1028/api/cities', function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectCityParkingAdmin").append(element);
        }
    });
}

async function populateAreasDropdown() {
    let cityId = $("#selectCityParkingAdmin").val();
    resetAreasDropdown();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas`, function (data) {
        for (var i = 0; i < data.length; i++) {
            let element = `<option value="${data[i].id}">${data[i].name}</option>`;
            $("#selectAreaParkingAdmin").append(element);
        }
    })
}

function resetAreasDropdown() {
    let initialElement = `<option value="" disabled selected hidden>Area</option>`;
    $("#selectAreaParkingAdmin").empty();
    $("#selectAreaParkingAdmin").append(initialElement);
}

async function populateParkingLotsDropdown() {
    let loggedInUserId = $("#loggedInUser").text();
    let loggedInUserRank = 0;
    await $.getJSON(`http://localhost:1234/api/users/${loggedInUserId}`, function (data) {
        loggedInUserRank = data.rank;
    })

    let cityId = $("#selectCityParkingAdmin").val();
    let areaId = $("#selectAreaParkingAdmin").val();
    resetParkingLotsDropdown();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots`, function (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].applicationUserId == loggedInUserId || loggedInUserRank == 2) {
                let element = `<option value="${data[i].id}">${data[i].name}</option>`;
                $("#selectParkingLotParkingAdmin").append(element);
            }   
        }
    })
}

function resetParkingLotsDropdown() {
    let initialElement = `<option value="" disabled selected hidden>Parking Lot</option>`;
    $("#selectParkingLotParkingAdmin").empty();
    $("#selectParkingLotParkingAdmin").append(initialElement);
}

async function populateResultingParkingSapces() {
    $("#parkingSpaceList").empty();
    let cityId = $("#selectCityParkingAdmin").val();
    let areaId = $("#selectAreaParkingAdmin").val();
    let parkingLotId = $("#selectParkingLotParkingAdmin").val();
    await $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces`, function (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].isTaken === false) {
                $("#parkingSpaceList").append(
                    `<a href="#" class="list-group-item list-group-item-action list-group-item-success">${data[i].name}</a>`
                );
            } else {
                $("#parkingSpaceList").append(
                    `<a href="#" class="list-group-item list-group-item-action list-group-item-danger">${data[i].name}</a>`
                );
            }  
        }
    });
}









$("#newParkingSpaceSubmit").click(async function (event) {
    event.preventDefault();

    let cityId = $("#selectCityParkingAdmin").val();
    let areaId = $("#selectAreaParkingAdmin").val();
    let parkingLotId = $("#selectParkingLotParkingAdmin").val();
    let parkingSpaceName = await $("#parkingSpaceName").val();
    let parkingSpacePrice = await $("#parkingSpacePrice").val();
    let parkingSpaceDetails = await $("#parkingSpaceDetails").val();
    let hasCarWash = false;
    if (await $("input[id='hasCarWash']:checked").val() === "on") {
        hasCarWash = true;
    }
    alert(`hasCarWash: ${hasCarWash}`);
    let hasCover = false;
    if (await $("#hasCover").val() === "on") {
        hasCover = true;
    }
    alert(`hasCover: ${hasCover}`);

    let data = {
        "parkingLotId": parkingLotId,
        "name": parkingSpaceName,
        "isTaken": false,
        "hasCarWash": hasCarWash,
        "isCovered": hasCover,
        "price": parkingSpacePrice,
        "details": parkingSpaceDetails
    };

    await $.ajax({
        type: "POST",
        url: `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces`,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        dataType: "json",
        success: function () {
            console.log("success");
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            console.log('fail' + status.code);
        }
    })
    populateParkingLot();
});

async function populateParkingLot() {
    $("#parkingSpaceList").empty();
    let cityId = $("#selectCityParkingAdmin").val();
    let areaId = $("#selectAreaParkingAdmin").val();
    let parkingLotId = $("#selectParkingLotParkingAdmin").val();

    $.getJSON(`http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces`, function (data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].isTaken === false) {
                $("#parkingSpaceList").append(
                    `<a href="#" class="list-group-item list-group-item-action list-group-item-success">${data[i].name}</a>`
                );
            } else {
                $("#parkingSpaceList").append(
                    `<a href="#" class="list-group-item list-group-item-action list-group-item-danger">${data[i].name}</a>`
                );
            }   
        }
    });

}