document.addEventListener("DOMContentLoaded", () => {
    const inputSearch = document.getElementById("SearchInput");
    if (!inputSearch) return;

    let typingTimer;
    const delay = 300; // ms delay before firing search

    inputSearch.addEventListener("keyup", () => {
        clearTimeout(typingTimer);
        typingTimer = setTimeout(() => {
            const query = inputSearch.value.trim();
            const url = `/Car/Index?SearchInput=${encodeURIComponent(query)}`;

            fetch(url)
                .then(res => res.text())
                .then(html => {
                    const parser = new DOMParser();
                    const doc = parser.parseFromString(html, "text/html");
                    const newCars = doc.querySelector("#CarsContainer");
                    const container = document.querySelector("#CarsContainer");

                    if (container && newCars) {
                        container.innerHTML = newCars.innerHTML;
                    } else if (container) {
                        container.innerHTML = "<div class='alert alert-info text-center'>No cars found.</div>";
                    }
                })
                .catch(err => console.error("Error:", err));
        }, delay);
    });
});
