console.log("eet works");

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