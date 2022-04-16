function AQRealTimeData(config) {
    let objAQTable = undefined;

    const urls = {
        fetchRealTime: config.AQFetchUrl
    }

    function init() {
        objAQTable = config.AQTable
        objAQTable.hideAQTable();
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
                objAQTable.generateAQTable(data, filters);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }

    init();

    return {
        fetchAQIndexes: fetchAQIndexes
    }
}