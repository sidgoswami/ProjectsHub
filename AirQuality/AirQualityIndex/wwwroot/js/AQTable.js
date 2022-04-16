function AQTable(config) {
    let aqTable = undefined;

    function init() {
        hideAQTable();
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
}