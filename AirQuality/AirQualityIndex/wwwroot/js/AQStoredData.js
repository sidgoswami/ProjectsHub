function AQStoredData(config) {   
    let objAQTable;

    const UI = {
        ddCities: "#ddCities",
        ddStates: "#ddStates",
        lblLastRefreshed: "#idLastRefreshed"
    }

    const urls = {
        fetchAQData: config.AQDbFetchUrl,
        refreshAQRecordsInDb: config.AQDbRefreshUrl,
        getCityForState: config.AQGetCityForStateUrl,
        getLastRefresh: config.AQLastRefreshUrl,
        getAllStates: config.AQGetStatesUrl,
        getAllCities: config.AQGetCitiesUrl,
    }
    
    function init() {
        objAQTable = config.AQTable;
        objAQTable.hideAQTable();
        refreshLastRefreshLabel();
        refreshAllCities();
        refreshAllStates();
    }    

    function refreshAQRecordsInDb() {
        $.ajax({
            url: `${urls.refreshAQRecordsInDb}`,
            type: "GET",
            contentType: 'application/json',
            success: function (response) {
                console.log(data);
                console.dir(data);
                if (response.success) {
                    alert("The DB records are now refreshed");

                    //Call toast
                    refreshStateList(response.data.states);
                    refreshCityList(response.data.cities);
                    refreshLastRefreshLabel(response.data.lastRefreshed);
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
        statesOptions.push(`<option>${config.AQDropDownStateDefault}</option>`);
        $.each(statesInDb, function (index, state) {
            statesOptions.push(`<option>${state}</option>`);
        })
        $(UI.ddStates).html(statesOptions.concat());
    }

    function refreshCityList(citiesInDb) {
        let citiesOptions = [];
        citiesOptions.push(`<option>${config.AQDropDownCityDefault}</option>`);
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
                    //alert(response.message);
                    //Call toast
                    refreshCityList(response.data);
                }
                else {
                    alert("Unable to refresh dropdown for city.");
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function refreshAllCities() {
        $.ajax({
            url: `${urls.getAllCities}`,
            type: "GET",
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
                console.dir(response);
                if (response.success) {
                    //alert(response.message);
                    //Call toast
                    refreshCityList(response.data);
                }
                else {
                    alert("Unable to refresh dropdown for city.");
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function refreshAllStates() {
        $.ajax({
            url: `${urls.getAllStates}`,
            type: "GET",
            contentType: 'application/json',
            success: function (response) {
                console.log(response);
                console.dir(response);
                if (response.success) {
                    //alert(response.message);
                    //Call toast
                    refreshStateList(response.data);
                }
                else {
                    alert("Unable to refresh dropdown for state.");
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    function fetchAQIndexesFromDb(state, city) {
        console.log("calling controller 2")
        let input = {
            state: state,
            city: city
        }
        $.ajax({
            url: `${urls.fetchAQData}`,
            type: "GET",
            contentType: 'application/json',
            data: input,
            success: function (data) {
                console.log("controller success response");
                console.dir(data);
                objAQTable.generateAQTable(data, `State:${state}-City:${city}`);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    init();

    return {
        refreshAQRecordsInDb: refreshAQRecordsInDb,
        refreshCityListByState: refreshCityListByState,
        fetchAQIndexesFromDb: fetchAQIndexesFromDb
    }
}