function markRowAsSelected(row) {
    let previouslySelectedRow = document.querySelector(".selected-ver");
    if (previouslySelectedRow) {
        previouslySelectedRow.classList.remove("selected-ver");
    }
    row.classList.add("selected-ver")
}


function onItemSelectedItemTraspasoAlm(sender, args) {
    console.log("Item Selected");
    var dataItem = args.get_value();
    console.log("DataItem");
    var parts = dataItem.split("|");

    if (parts.length > 1) {
        var codigo = parts[0];
        var item = parts[1];

        document.getElementById('<%= txt_activo.ClientID %>').value = codigo;
        document.getElementById('<%= txt_codActivo.ClientID %>').value = item;
    }
}