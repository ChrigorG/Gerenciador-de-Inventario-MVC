// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleDropdownMenu(event) {
    // Impede que o clique feche menus ou dispare eventos indesejados
    event.stopPropagation();

    // Seleciona o botão clicado e seu menu correspondente
    const $dropdown = $(event.target).closest('.dropdown-table');
    const $menu = $dropdown.find('.dropdown-table-menu');

    // Fecha outros menus abertos
    $('.dropdown-table-menu.show').not($menu).removeClass('show');

    // Alterna a classe 'show' no menu atual
    $menu.toggleClass('show');

    // Fecha o menu se clicar fora dele
    $(document).on('click.closeDropdown', function (e) {
        if (!$dropdown.is(e.target) && $dropdown.has(e.target).length === 0) {
            $menu.removeClass('show');
            $(document).off('click.closeDropdown'); // Remove o evento
        }
    });
}
