document.addEventListener("DOMContentLoaded", function () {
    const searchAll = document.getElementById("searchAll");
    const dateFrom = document.getElementById("dateFrom");
    const dateTo = document.getElementById("dateTo");
    const timeFrom = document.getElementById("timeFrom");
    const timeTo = document.getElementById("timeTo");
    const appointmentRows = document.querySelectorAll(".appointment-row");

    function filterAppointments() {
        const searchText = searchAll.value.toLowerCase();

        appointmentRows.forEach(row => {
            const type = row.getAttribute("data-type").toLowerCase();
            const status = row.getAttribute("data-status").toLowerCase();
            const officer = row.getAttribute("data-officer").toLowerCase();
            const visitor = row.getAttribute("data-visitor").toLowerCase();
            const date = row.getAttribute("data-date");
            const startTime = row.getAttribute("data-start-time");
            const endTime = row.getAttribute("data-end-time");

            // Match any of the four fields with the search text
            const matchesSearch = !searchText ||
                type.includes(searchText) ||
                status.includes(searchText) ||
                officer.includes(searchText) ||
                visitor.includes(searchText);

            const matchesDateFrom = !dateFrom.value || date >= dateFrom.value;
            const matchesDateTo = !dateTo.value || date <= dateTo.value;
            const matchesTimeFrom = !timeFrom.value || startTime >= timeFrom.value;
            const matchesTimeTo = !timeTo.value || endTime <= timeTo.value;

            // Show row if all filters match
            if (matchesSearch && matchesDateFrom && matchesDateTo && matchesTimeFrom && matchesTimeTo) {
                row.style.display = "";
            } else {
                row.style.display = "none";
            }
        });
    }

    // Attach event listeners to filter fields
    [searchAll, dateFrom, dateTo, timeFrom, timeTo].forEach(input => {
        input.addEventListener("input", filterAppointments);
    });
});
