document.addEventListener("DOMContentLoaded", function () {
    const searchAll = document.getElementById("searchAll");
    const dateFrom = document.getElementById("dateFrom");
    const dateTo = document.getElementById("dateTo");
    const timeFrom = document.getElementById("timeFrom");
    const timeTo = document.getElementById("timeTo");
    const appointmentRows = document.querySelectorAll(".appointment-row");

    const appointmentsTableBody = document.getElementById("appointmentsTableBody");
    const prevPageButton = document.getElementById("prevPage");
    const nextPageButton = document.getElementById("nextPage");
    const pageInfo = document.getElementById("pageInfo");

    let currentPage = 1;
    const itemsPerPage = 10;
    let filteredAppointments = Array.from(appointmentRows);

    function renderTable() {
        appointmentsTableBody.innerHTML = "";
        const start = (currentPage - 1) * itemsPerPage;
        const end = start + itemsPerPage;

        const pageItems = filteredAppointments.slice(start, end);

        pageItems.forEach((row, index) => {
            // Update the SN based on the current page
            const clonedRow = row.cloneNode(true);
            clonedRow.querySelector("td:first-child").innerText = start + index + 1;
            appointmentsTableBody.appendChild(clonedRow);
        });

        // Update pagination information
        pageInfo.innerText = `Page ${currentPage}`;
        prevPageButton.disabled = currentPage === 1;
        nextPageButton.disabled = end >= filteredAppointments.length;
    }

    function filterAppointments() {
        const searchText = searchAll.value.toLowerCase();

        filteredAppointments = Array.from(appointmentRows).filter(row => {
            const type = row.getAttribute("data-type").toLowerCase();
            const status = row.getAttribute("data-status").toLowerCase();
            const officer = row.getAttribute("data-officer").toLowerCase();
            const visitor = row.getAttribute("data-visitor").toLowerCase();
            const date = row.getAttribute("data-date");
            const startTime = row.getAttribute("data-start-time");
            const endTime = row.getAttribute("data-end-time");

            const matchesSearch = !searchText ||
                type.includes(searchText) ||
                status.includes(searchText) ||
                officer.includes(searchText) ||
                visitor.includes(searchText);

            const matchesDateFrom = !dateFrom.value || date >= dateFrom.value;
            const matchesDateTo = !dateTo.value || date <= dateTo.value;
            const matchesTimeFrom = !timeFrom.value || startTime >= timeFrom.value;
            const matchesTimeTo = !timeTo.value || endTime <= timeTo.value;

            return matchesSearch && matchesDateFrom && matchesDateTo && matchesTimeFrom && matchesTimeTo;
        });

        currentPage = 1; // Reset to the first page after filtering
        renderTable();
    }

    // Attach event listeners to filter fields
    [searchAll, dateFrom, dateTo, timeFrom, timeTo].forEach(input => {
        input.addEventListener("input", filterAppointments);
    });

    // Pagination buttons event listeners
    prevPageButton.addEventListener("click", () => {
        if (currentPage > 1) {
            currentPage--;
            renderTable();
        }
    });

    nextPageButton.addEventListener("click", () => {
        if (currentPage * itemsPerPage < filteredAppointments.length) {
            currentPage++;
            renderTable();
        }
    });

    // Initial render
    filterAppointments();
});
