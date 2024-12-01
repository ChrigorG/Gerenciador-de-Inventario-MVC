class DataTableHelper {
    constructor(selector, options = {}) {
        const element = document.querySelector(selector);

        if (!element || element.tagName !== 'TABLE') {
            console.error("O seletor fornecido deve ser uma tabela.");
            return;
        }

        // Estilo padrão do Bootstrap
        element.classList.add('table', 'table-bordered', 'table-hover');

        // Opções padrão e validação
        this.selector = selector;
        const defaultLanguage = {
            url: "/json/datatable_pt_br.json",
            emptyTable: "Nenhum registro disponível",
            search: "Buscar:",
        };

        this.defaultOptions = {
            paging: false,          // Paginação
            searching: true,        // Campo de busca
            ordering: true,         // Ordenação
            order: [[0, 'asc']],    // Ordenação padrão
            info: true,             // Mostra informações sobre a tabela
            autoWidth: false,       // Largura automática
            responsive: true,       // Tabela responsiva
            language: defaultLanguage,
        };

        // Combinação de opções com validação
        if (typeof options !== 'object') {
            options = {};
        }

        // Combinando as informações passadas do "options" com os "defaultOptions"
        this.options = { ...this.defaultOptions, ...options };

        // Personalização de idiomas
        if (options.language) {
            this.options.language = { ...defaultLanguage, ...options.language };
        }

        // Suporte a botões de exportação
        if (options.enableExport) {
            this.options.dom = 'Bfrtip'; // Adiciona container para botões
            this.options.buttons = ['copy', 'csv', 'excel', 'pdf', 'print'];
        }
    }

    init() {
        this.table = $(this.selector).DataTable(this.options);
        return this.table;
    }
}
