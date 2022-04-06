function AQ(config) {   
    let fetchRealTimeUrl = '';
    let refreshAQRecordsInDbUrl = '';
    let aqTable = undefined;

    function init(config) {
        fetchRealTimeUrl = config.AQFetchUrl;
        refreshAQRecordsInDbUrl = config.AQDbRefreshUrl;
        hideAQTable();
    }

    function fetchAQIndexes(offset, limit, filters) {
        let input = {
            offset: offset,
            limit: limit,
            filters: filters
        }
        $.ajax({
            url: `${fetchRealTimeUrl}`,
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
        $(`#${config.AQTableBodyId}`).html('');
    }

    function hideAQTable() {
        $(`#${config.AQTableWrapper}`).hide();
    }

    function showAQTable() {
        $(`#${config.AQTableWrapper}`).show();
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
            $(`#${config.AQTableBodyId}`).html(tablehtml);            
            aqTable = $(`#${config.AQTableId}`).DataTable({
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
            url: `${refreshAQRecordsInDbUrl}`,
            type: "GET",
            contentType: 'application/json',
            success: function (data) {
                console.log(data);
                console.dir(data);
                if (data == true) {
                    alert("The DB records are now refreshed");
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
        refreshAQRecordsInDb: refreshAQRecordsInDb
    }
}