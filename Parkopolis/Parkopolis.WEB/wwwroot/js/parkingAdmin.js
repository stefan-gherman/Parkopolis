

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
                        <div class="container" id="editParkingSpaceContainer${data[i].id}"></div>
                   </a>`
            $("#parkingSpaceList").append(element);
        }
        $("[class*=btn][class*=btn-success]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("forceFree", "");
            handleForceFreeParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId);
        });
        $("[class*=btn][class*=btn-warning]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("editParkingSpace", "");
            handleEditParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId);
        });
        $("[class*=btn][class*=btn-danger]").click(function (event) {
            event.preventDefault();
            let parkingSpaceId = this.id.replace("deleteParkingSpace", "");
            handleDeleteParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId);
        });
    });
}

async function handleForceFreeParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId) {
    console.log("Entered force free process");
    let largeUrl = `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces/${parkingSpaceId}`;
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

async function handleEditParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId) {
    console.log("Entered Edit process");
    let largeUrl = `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces/${parkingSpaceId}`;

    // get data about the parking space from DB
    let parkingSpaceFromDb = {
        "id": 0,
        "parkingLotId": 0,
        "name": "",
        "isTaken": false,
        "hasCarWash": false,
        "isCovered": false,
        "price": 0.00,
        "details": ""
    }
    await $.getJSON(largeUrl, async function (data) {
        parkingSpaceFromDb.id = data.id;
        parkingSpaceFromDb.parkingLotId = data.parkingLotId;
        parkingSpaceFromDb.name = data.name;
        parkingSpaceFromDb.isTaken = data.isTaken;
        parkingSpaceFromDb.hasCarWash = data.hasCarWash;
        parkingSpaceFromDb.isCovered = data.isCovered;
        parkingSpaceFromDb.price = data.price;
        parkingSpaceFromDb.details = data.details;
    });

    // create the form to edit with populated info
    $(`#editParkingSpaceContainer${parkingSpaceId}`).empty();
    let editFormHasCarWashChecked = "";
    if (parkingSpaceFromDb.hasCarWash == true) {
        editFormHasCarWashChecked = "checked"
    }
    let editFormIsCoveredChecked = "";
    if (parkingSpaceFromDb.isCovered == true) {
        editFormIsCoveredChecked = "checked"
    }
    let element = `
                    <form>
                        <div class="form-group">
                            <input type="text" class="form-control" id="editParkingSpaceName" value="${parkingSpaceFromDb.name}">
                            <input type="number" class="form-control" id="editParkingSpacePrice" step=".01" value="${parkingSpaceFromDb.price}">
                            <div class="form-check">
                                <input type="checkbox" class="form-check-input" id="editHasCarWash" ${editFormHasCarWashChecked}>
                                <label class="form-check-label" for="editHasCarWash">Has Car Wash Service</label>
                            </div>
                            <div class="form-check">
                                <input type="checkbox" class="form-check-input" id="editHasCover" ${editFormIsCoveredChecked}>
                                <label class="form-check-label" for="editHasCover">Has Cover (roof or underground)</label>
                            </div>
                        </div>
                        <button id="editParkingSpaceSubmit" type="submit" class="btn btn-primary">Confirm Edit</button>
                        <button id="editParkingSpaceCancel" type="submit" class="btn btn-primary">Cancel</button>
                        
                    </form>
                    `;
    $(`#editParkingSpaceContainer${parkingSpaceId}`).append(element);
    $("#editParkingSpaceName").click(function (event) {
        event.preventDefault();
    })
    $("#editParkingSpacePrice").click(function (event) {
        event.preventDefault();
    })
    $("#editParkingSpaceCancel").click(function (event) {
        event.preventDefault();
        $("#editParkingSpaceContainer").empty();
    })
    $("#editParkingSpaceSubmit").click(async function (event) {
        event.preventDefault();
        parkingSpaceFromDb.name = $("#editParkingSpaceName").val();
        parkingSpaceFromDb.price = $("#editParkingSpacePrice").val();
        if ($("input[id='editHasCarWash']:checked").val() === "on") {
            parkingSpaceFromDb.hasCarWash = true;
        }
        if ($("input[id='editHasCover']:checked").val() === "on") {
            parkingSpaceFromDb.isCovered = true;
        }
        await $.ajax({
            type: "PUT",
            url: largeUrl,
            data: JSON.stringify(parkingSpaceFromDb),
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
    })

    
    //await $.getJSON(largeUrl, async function (data) {
    //    data =
    //        [
    //            {
    //                "op": "replace",
    //                "path": "/isTaken",
    //                "value": false
    //            }
    //        ]
    //    await $.ajax({
    //        type: "PATCH",
    //        url: largeUrl,
    //        data: JSON.stringify(data),
    //        contentType: "application/json; charset=utf-8",
    //        crossDomain: true,
    //        dataType: "json",
    //        success: function () {
    //            console.log("Spot freed-up successfully.");
    //        },
    //        error: function (jqXHR, status) {
    //            // error handler
    //            console.log(jqXHR);
    //            alert('fail' + status.code);
    //        }
    //    })
    //    // refreshing the Parking Spaces only
    //    populateResultingParkingSapces();
    //});
};

async function handleDeleteParkingSpace(cityId, areaId, parkingLotId, parkingSpaceId) {
    console.log("Entered Delete process")
    let largeUrl = `http://localhost:1028/api/cities/${cityId}/areas/${areaId}/parkinglots/${parkingLotId}/parkingspaces/${parkingSpaceId}`;
    await $.ajax({
        type: "DELETE",
        url: largeUrl,
        contentType: "application/json; charset=utf-8",
        crossDomain: true,
        success: function () {
            console.log("Spot deleted successfully.");
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            alert('fail' + status.code);
        }
    })
    // refreshing the Parking Spaces only
    populateResultingParkingSapces();
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