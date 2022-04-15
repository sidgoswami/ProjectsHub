function AQ(config) {   
    let aqTable = undefined;

    const UI = {
        AQTableBodyId: `#${config.AQTableBodyId}`,
        AQTableId: `#${config.AQTableId}`,
        AQTableWrapper: `#${config.AQTableWrapper}`,
        ddCities: "#ddCities",
        ddStates: "#ddStates",
        lblLastRefreshed: "#idLastRefreshed"
    }

    const urls = {
        fetchRealTime: config.AQFetchUrl,
        refreshAQRecordsInDb: config.AQDbRefreshUrl,
        getCityForState: config.AQGetCityForStateUrl,
        getLastRefresh: config.AQLastRefreshUrl
    }
    
    function init() {
        hideAQTable();
        refreshLastRefreshLabel();
    }

    function fetchAQIndexes(offset, limit, filters) {
        let input = {
            offset: offset,
            limit: limit,
            filters: filters
        }
        $.ajax({
            url: `${urls.fetchRealTime}`,
            type: "GET",
            contentType: 'application/json',
            data: input,
            success: function (data) {
                generateAQTable(data, filters);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function clearTableBody() {
        $(UI.AQTableBodyId).html('');
    }

    function hideAQTable() {
        $(UI.AQTableWrapper).hide();
    }

    function showAQTable() {
        $(UI.AQTableWrapper).show();
    }

    function generateAQTable(tableContent, filters) {
        //if (tableContent.length > 0) {
            clearTableBody();
            if (aqTable != undefined) {
                aqTable.destroy(false);
            }
            let tablehtml = '';
            console.log(tablehtml);
            $.each(tableContent, function (index, value) {
                tablehtml += `<tr>
                                    <td>${value.id}</td>
                                    <td>${value.country}</td>
                                    <td>${value.state}</td>
                                    <td>${value.city}</td>
                                    <td>${value.station}</td>
                                    <td>${value.last_update}</td>
                                    <td>${value.pollutant_id}</td>
                                    <td>${value.pollutant_min}</td>
                                    <td>${value.pollutant_max}</td>
                                    <td>${value.pollutant_avg}</td>
                                    <td>${value.pollutant_unit}</td>
                                </tr>`;
            });
            $(UI.AQTableBodyId).html(tablehtml);            
            aqTable = $(UI.AQTableId).DataTable({
                "language": {
                    "emptyTable": `<span class="alert">No data available for the selected filter : ${filters}</span>`
                }
            });
            showAQTable();
        //}
        //else {
        //    hideAQTable();
        //}
    }

    function refreshAQRecordsInDb() {
        $.ajax({
            url: `${urls.refreshAQRecordsInDb}`,
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                console.log(data);
                console.dir(data);
                if (data.success) {
                    alert("The DB records are now refreshed");

                    //Call toast
                    refreshStateList(data.states);
                    refreshCityList(data.cities);
                    refreshLastRefreshLabel(data.lastRefreshed);
                }
                else {
                    alert("Unable to refresh table in database");
                }                
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function refreshStateList(statesInDb) {
        let statesOptions = [];
        $.each(statesInDb, function (index, state) {
            statesOptions.push(`<option>${state}</option>`);
        })
        $(UI.ddStates).html(statesOptions.concat());
    }

    function refreshCityList(citiesInDb) {
        let citiesOptions = [];        
        $.each(citiesInDb, function (index, city) {
            citiesOptions.push(`<option>${city}</option>`);
        })
        $(UI.ddCities).html(citiesOptions.concat());
    }

    function refreshLastRefreshLabel(lastRefresh) {
        if (lastRefresh == undefined) {
            $.ajax({
                url: `${urls.getLastRefresh}`,
                type: "GET",
                contentType: 'application/json',
                success: function (response) {
                    console.log(response);
                    console.dir(response);
                    if (response.success) {
                        lastRefresh = response.data;
                    }
                    else {
                        alert(`${response.message}. Please refresh the stored records`);
                        lastRefresh = 'NA';
                    }
                    $(UI.lblLastRefreshed).val(lastRefresh);
                },
                error: function (data) {
                    console.log(data);
                }
            });
        }
        else {
            $(UI.lblLastRefreshed).val(lastRefresh);
        }
    }

    function refreshCityListByState(state) {
        console.log(state);
        console.dir(state);
        $.ajax({
            url: `${urls.getCityForState}`,
            type: "GET",
            data: {"State": state},
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
                console.dir(response);
                if (response.success) {
                    alert(response.message);
                    //Call toast
                    refreshStateList(response.data);
                }
                else {
                    alert("Unable to refresh table in database");
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    init(config);

    return {
        fetchAQIndexes: fetchAQIndexes,
        refreshAQRecordsInDb: refreshAQRecordsInDb,
        refreshCityListByState: refreshCityListByState
    }
}