

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
            let classLink = "list-group-item list-group-item-action list-group-item-success";
            let forceFreeButtonContent = `<button id="forceFree${data[i].id}" class="btn btn-success">Force Free</button>`;
            if (data[i].isTaken == true) {
                classLink = "list-group-item list-group-item-action list-group-item-danger";
            } else {
                forceFreeButtonContent = "";
            }
            let element = `<a href="#" class="${classLink}">
                        <div class="container">
                            <div class="d-flex justify-content-between">
                                <p>${data[i].name}</p>
                                <p>
                                    ${forceFreeButtonContent}
                                </p>
                                <p>
                                    <button id="editParkingSpace${data[i].id}" class="btn btn-warning">Edit</button>
                                    <button id="deleteParkingSpace${data[i].id}" class="btn btn-danger">Delete</button>
                                </p>
                            </div>
                        </div>
                   </a>`
            $("#parkingSpaceList").append(element);
        }
        $("[class*=btn][class*=btn-success]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("forceFree", "");
            handleForceFreeParkingSpace(parkingSpaceId);
        });
        $("[class*=btn][class*=btn-warning]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("editParkingSpace", "");
            handleEditParkingSpace(parkingSpaceId);
        });
        $("[class*=btn][class*=btn-danger]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("deleteParkingSpace", "");
            handleDeleteParkingSpace(parkingSpaceId);
        });
    });
}

async function handleForceFreeParkingSpace(parkingSpaceId) {
    console.log("Entered force free process");
};

async function handleEditParkingSpace(parkingSpaceId) {
    console.log("Entered Edit process");
};

async function handleDeleteParkingSpace(parkingSpaceId) {
    console.log("Entered Delete process")
};







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
    console.log(`hasCarWash: ${hasCarWash}`);
    let hasCover = false;
    if (await $("#hasCover").val() === "on") {
        hasCover = true;
    }
    console.log(`hasCover: ${hasCover}`);

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