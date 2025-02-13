function markRowAsSelected(row) {
    let previouslySelectedRow = document.querySelector(".selected-ver");
    if (previouslySelectedRow) {
        previouslySelectedRow.classList.remove("selected-ver");
    }
    row.classList.add("selected-ver");

}