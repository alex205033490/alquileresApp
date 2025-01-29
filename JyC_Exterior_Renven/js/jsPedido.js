function convertirComaAPunto(event) {
    let valor = event.target.value;

    valor = valor.replace(/,/g, '.');

    event.target.value = valor;
}