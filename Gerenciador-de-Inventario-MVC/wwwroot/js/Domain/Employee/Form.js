$(() => {
    const $form = $("#form-employee-modal #employeeForm");

    $("#form-employee-modal #submitEmployeeForm").on("click", function () {
        $form.submit();
    });

    $form.on("submit", async (event) => {
        // Impede o envio padrão do formulário
        event.preventDefault();
        debugger;

        $.ajax({

            type: "POST",
            url: "/Employee/Save",
            data: $form.serializeArray(),
            success: function (response) {
                debugger;
                if (!response.statusErro) {
                    $("#idDivEmployeeTable").html(response.view);
                    $("#form-employee-modal #closeEmployeeForm").click();

                    Swal.fire({
                        title: "Sucesso",
                        text: response.message,
                        icon: "success"
                    });
                } else {
                    if (response.view) {
                        new Modal({
                            id: "form-employee-modal",
                            html: response.view
                        }).Create();
                    } else {
                        Swal.fire({
                            title: "Atenção",
                            text: response.message,
                            icon: "warning"
                        });
                    }
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

