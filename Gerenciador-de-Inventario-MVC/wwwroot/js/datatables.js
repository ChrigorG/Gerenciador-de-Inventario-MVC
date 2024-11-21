$(document).ready(function () {
    // Inicializar DataTable para todas as tabelas com a classe .dataTable
    $('.dataTable').DataTable({
        columnDefs: [
            { orderable: false, targets: 0 }, // Desabilitar ordenação na coluna de seleção
        ],
        order: [[1, 'asc']], // Coluna inicial para ordenação
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/Portuguese-Brasil.json" // Tradução para português
        }
    });

    // Selecionar todos os checkboxes
    $('.selectAll').on('click', function () {
        var table = $(this).closest('table').DataTable();
        var rows = table.rows({ 'search': 'applied' }).nodes();
        $('input[type="checkbox"].selectItem', rows).prop('checked', this.checked);
    });

    // Atualizar o checkbox principal ao alterar qualquer checkbox individual
    $('table tbody').on('change', 'input[type="checkbox"].selectItem', function () {
        var table = $(this).closest('table').DataTable();
        var el = $('.selectAll').get(0);
        if (!this.checked && el && el.checked && ('indeterminate' in el)) {
            el.indeterminate = true;
        }
    });
});
