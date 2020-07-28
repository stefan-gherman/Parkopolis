console.log("eet works");
let parkingSpacesList = [];
populateParkingLot();

$("#newParkingSpaceSubmit").click(async function () {
    let parkingLotId = await $("h3").first().attr("id").replace("parkingLotId", "");
    //alert(parkingLotId);
    let parkingSpaceName = await $("#parkingSpaceName").val();
    //alert(parkingSpaceName);


    let data = {
        "parkingLotId": parkingLotId,
        "name": parkingSpaceName,
        "isTaken": false,
        "hasCarWash": true,
        "isCovered": true,
        "price": 10.0,
        "details": "Close to exit."
    };
    

    $.ajax({
        type: "POST",
        url: `http://localhost:1028/api/cities/1/areas/1/parkinglots/${parkingLotId}/parkingspaces`,
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
});

async function populateParkingLot() {

    let parkingLotId = await $("h3").first().attr("id").replace("parkingLotId", "");
    $.getJSON(`http://localhost:1028/api/cities/1/areas/1/parkinglots/${parkingLotId}/parkingspaces`, function (data) {
        for (var i = 0; i < data.length; i++) {
            //parkingSpacesList.push(data[i]);
            
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