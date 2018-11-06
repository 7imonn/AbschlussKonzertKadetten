function tableToExcel() {
    var table = document.getElementById('reservationenTable');
    var html = table.outerHTML;

    while (html.indexOf('ä') != -1) html = html.replace('ä', '&auml;');
    while (html.indexOf('ö') != -1) html = html.replace('ö', '&ouml;');
    while (html.indexOf('ü') != -1) html = html.replace('ü', '&uuml;');

    window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));
};

