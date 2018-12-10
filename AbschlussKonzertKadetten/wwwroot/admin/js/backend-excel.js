function tableToExcel() {
    var table = document.getElementById('reservationenTable');
    var html = table.outerHTML;

    while (html.indexOf('ä') !== -1) html = html.replace('ä', '&auml;');
    while (html.indexOf('ö') !== -1) html = html.replace('ö', '&ouml;');
    while (html.indexOf('ü') !== -1) html = html.replace('ü', '&uuml;');
    while (html.indexOf('é') !== -1) html = html.replace('é', '&eacute;');
    while (html.indexOf('ë') !== -1) html = html.replace('ë', '&euml;');

    window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));
};

