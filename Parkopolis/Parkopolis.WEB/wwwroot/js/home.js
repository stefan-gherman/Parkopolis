populateCitiesDropdown();
$("#selectCityHome").change(function () {
    populateAreasDropdown();
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