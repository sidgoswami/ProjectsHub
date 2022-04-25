function AQRealTimeData(config) {
    let objAQTable = undefined;
    const notify = config.Notify;

    const urls = {
        fetchRealTime: config.AQFetchUrl
    }

    function init() {
        notify.info("Inside Real Data View");       
        objAQTable = config.AQTable
    }

    function fetchAQIndexes(offset, limit, filters) {
        let input = {
            offset: offset,
            limit: limit,
            filters: filters
        }
        notify.info("Loading...", null, false);
        $.ajax({
            url: `${urls.fetchRealTime}`,
            type: "GET",
            contentType: 'application/json',
            data: input,
            success: function (data) {
                notify.hide();
                objAQTable.generateAQTable(data, filters);
                notify.success("Success", "Data successfully added to table.");
            },
            error: function (data) {
                notify.hide();
                notify.error("API Fetch Error", "Error while fetching data from API.");
                console.log(data);
            }
        });
    }

    init();

    return {
        fetchAQIndexes: fetchAQIndexes
    }
}