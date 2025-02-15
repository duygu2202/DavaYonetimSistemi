$(document).ready(function() {
    // DataTables inicializasyonu
    var table = $('#recordsTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.10.24/i18n/Turkish.json'
        },
        processing: true,
        serverSide: true,
        ajax: {
            url: $('#recordsTable').data('url'),
            type: 'POST',
            data: function(d) {
                var formData = $('#filterForm').serializeArray();
                formData.forEach(function(item) {
                    d[item.name] = item.value;
                });
            }
        },
        columns: getColumns()
    });

    // Filtreleme
    $('#filterForm').on('submit', function(e) {
        e.preventDefault();
        table.ajax.reload();
    });

    // Select2 inicializasyonu
    $('.select2').select2();
});

function showNewRecordModal() {
    $.get($('#recordsTable').data('new-url'), function(data) {
        $('#modalContainer').html(data);
        $('#recordModal').modal('show');
    });
}

function exportReport(type) {
    var formData = $('#filterForm').serialize();
    var url = $('#recordsTable').data('export-url') + '?type=' + type + '&' + formData;
    
    $.post(url, function(response) {
        if (response.success) {
            window.location.href = response.fileUrl;
        } else {
            Swal.fire('Hata!', response.message, 'error');
        }
    });
}

function getColumns() {
    var type = $('#recordsTable').data('type');
    
    if (type === 'Dava') {
        return [
            { data: 'uyapBirimi' },
            { data: 'esasNo' },
            { data: 'ofisNo' },
            { data: 'eskiEsasNo' },
            { data: 'durum' },
            { data: 'davaKonusu' },
            { data: 'davaDegeri' },
            { data: 'davaEdenler' },
            { data: 'davaEdilenler' },
            { data: 'sorumlu' },
            { data: 'asama' },
            { data: 'altAsama' },
            { data: 'kayitTarihi' },
            { data: 'arsivTarihi' }
        ];
    } else {
        return [
            { data: 'uyapBirimi' },
            { data: 'esasNo' },
            { data: 'ofisNo' },
            { data: 'eskiEsasNo' },
            { data: 'durum' },
            { data: 'alacaklilar' },
            { data: 'borclular' },
            { data: 'takipDegeri' },
            { data: 'tahsilat' },
            { data: 'bakiye' },
            { data: 'sorumlu' },
            { data: 'kayitTarihi' },
            { data: 'arsivTarihi' }
        ];
    }
} 