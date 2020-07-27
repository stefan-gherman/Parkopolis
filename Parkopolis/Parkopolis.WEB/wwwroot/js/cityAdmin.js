let citiesList = []
$.getJSON('http://localhost:1028/api/cities', function (data) {
    for (var i = 0; i < data.length; i++) {
        citiesList.push(data[i]);     
    }
    populateCities();
});

function populateCities() {
    for (var i = 0; i < citiesList.length; i++) {
        $("#citiesContainer").append(
            `<p>${citiesList[i].name}</p>  
            `
        );
    }
}

$("#newCitySubmit").click(function () {
    console.log($("#cityName").val());
    let newCityName = $("#cityName").val();
    //alert(name);
    let data = {};
    data[name] = newCityName;
    
    $.ajax({
        type: "POST",
        url: "http://localhost:1028/api/cities",
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