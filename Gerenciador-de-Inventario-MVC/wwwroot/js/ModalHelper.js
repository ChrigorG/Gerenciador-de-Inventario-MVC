class Modal {
    constructor({
        id = "",
        html = "",
        openAutomatically = true,
    }) {
        this.SetHtmlModal(html);
        this.SetIdModal(id);
        this._openAutomatically = openAutomatically;
    }

    Create() {
        this.SetDivModal();
        $('.modal').modal();
        if (this._openAutomatically) {
            this.Show();
        }

        return this;
    }

    SetDivModal() {
        const idDivModal = `div-${this._id}`;
        let $divModal = $(`#${idDivModal}`);

        //Apaga para que ela perca a antiga ordem no z-index
        $divModal.remove();

        $("body").append(`<div id='${idDivModal}'></div>`);

        $divModal = $(`#${idDivModal}`);

        $divModal.html(this._html);
    }

    SetIdModal(id) {
        const $html = $(this._html);
        if (!id) {
            id = $html.find("div.modal").attr("id");
            if (!id) {
                id = $html.attr("id");
            }
        }

        this._id = id;
    }

    SetHtmlModal(html) {
        if (!html) {
            html = "";
        }

        this._html = html;
    }

    Show() {
        // .modal('show'); exclusivo para o Boostrap
        $(`#${this._id}`).modal('show');

        return this;
    }

    ApplySelfDestructWhenHide() {
        $(`#${this._id}`).on("hidden.bs.modal", () => {
            $(`#div-${this._id}`).remove();
        });

        return this;
    }
}