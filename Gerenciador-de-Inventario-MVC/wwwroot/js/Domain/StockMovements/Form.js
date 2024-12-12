$(() => {
    const $form = $("#form-stock-movements-modal #stockMovementsForm");

    $("#form-stock-movements-modal #submitStockMovementsForm").on("click", function () {
        $form.submit();
    });

    $form.on("submit", async (event) => {
        // Impede o envio padrão do formulário
        event.preventDefault();

        $.ajax({
            type: "POST",
            url: "/StockMovements/Save",
            data: $form.serializeArray(),
            success: function (response) {
                if (!response.statusErro) {
                    // Tudo certo, atualizando a tabela e fechando a modal
                    $("#divTableStockMovements").html(response.view);
                    $("#form-stock-movements-modal #closeFormStockMovements").click();

                    Swal.fire({
                        title: "Sucesso",
                        text: response.message,
                        icon: "success"
                    });
                } else {
                    if (response.view)
                        $("#form-stock-movements-modal #divPartialFormStockMovements").html(response.view); // Atualizo a <div> com as mensagens os dados que foram invalidado no servidor

                    // Mensagem de Erro
                    Swal.fire({
                        title: "Atenção",
                        text: response.message,
                        icon: "warning"
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    title: "Atenção",
                    text: `Não foi possível completar a ação: ${error}`,
                    icon: "error"
                });
            }
        });
    });
})

