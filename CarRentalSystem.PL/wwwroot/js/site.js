// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let inputSearch = document.getElementById("SearchInput");

// Listen to every key typed
inputSearch.addEventListener("keyup", () => {
    let query = inputSearch.value;

    let xhr = new XMLHttpRequest();
    let url = `/Car/Index?SearchInput=${encodeURIComponent(query)}`;
    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            // Replace the table body with the new filtered result
            let parser = new DOMParser();
            let doc = parser.parseFromString(this.responseText, "text/html");

            // Get the new table or empty message from the response
            let newTable = doc.querySelector("table");
            let container = document.querySelector("#CarsContainer");

            container.innerHTML = newTable ? newTable.outerHTML : "<div class='alert alert-info'>No Cars Found</div>";
        }
    };

    xhr.send();
});
