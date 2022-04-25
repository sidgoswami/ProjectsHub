function AQStoredData(config) {   
    let objAQTable;
    const notify = config.Notify;

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
        notify.info("Inside Stored Data View")
        objAQTable = config.AQTable;
        refreshLastRefreshLabel();
        refreshAllCities();
        refreshAllStates();
    }    

    function refreshAQRecordsInDb() {
        notify.info("Load started", "Initializing the data load to Storage")
        notify.info("Loading...", null, false);
        $.ajax({
            url: `${urls.refreshAQRecordsInDb}`,
            type: "GET",
            contentType: 'application/json',
            success: function (response) {
                notify.hide();
                console.log(response);
                console.dir(response);
                if (response.success) {
                    notify.success("Load Succeeded", "Data successfully loaded to storage.");
                    refreshStateList(response.data.states);
                    refreshCityList(response.data.cities);
                    refreshLastRefreshLabel(response.data.lastRefreshed);
                }
                else {
                    notify.error("Load Error", "Error while data loading.");
                    console.log("Error", response.message);
                }                
            },
            error: function (data) {
                notify.hide();
                notify.error("Load Error", "Error while data loading.");
                console.log("Exception", data);
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
                        notify.error("Load Error", `${response.message}. Please refresh the stored records.`);                       
                        lastRefresh = 'NA';
                    }
                    $(UI.lblLastRefreshed).val(lastRefresh);
                },
                error: function (data) {
                    notify.error("Load Error", "Error while loading last refresh.");
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
                    notify.error("Dropdown Refresh Error", "Unable to refresh dropdown for city.");                    
                }
            },
            error: function (data) {
                notify.error("Load Error", "Error while loading last refresh.");
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
                    notify.error("Load Error", "Error while refreshing city dropdown.");
                }
            },
            error: function (data) {
                notify.error("Load Error", "Error while loading last refresh.");
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
                    notify.error("Load Error", "Error while loading last refresh.");                    
                }
            },
            error: function (data) {
                notify.error("Load Exception", "Error while loading last refresh.");
                console.log(data);
            }
        });
    }

    function fetchAQIndexesFromDb(state, city) {
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
                notify.success("Success", "Data successfully added to table.");
                objAQTable.generateAQTable(data, `State:${state}-City:${city}`);
            },
            error: function (data) {
                notify.error("Load Error", "Error while loading last refresh.");
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