export function SetTrigger(buttonId, componentId, trId) {
    var button = document.getElementById(buttonId);
    if (button == undefined) {
        return;
    }
    var detailCompoment = document.getElementById(componentId);
    var trSelectable = document.getElementById(trId);
    var className = button.className;
    if (!className.includes("Trigger")) {
        button.className += " Trigger";
        if (detailCompoment != undefined) {
            detailCompoment.className += " blockDetail"
        }
        if (trSelectable != undefined) {
            trSelectable.className += " tr-selected";
        }
    }
    else {
        button.className = "Btn_ShowDetails clickeable d-flex";
        if (detailCompoment != undefined) {
            detailCompoment.className = "tr-details";
        }
        if (trSelectable != undefined) {
            trSelectable.className = "table-row-selectable";
        }

    }
    return true;
}
export function addClassToBody(className) {
    document.body.classList.add(className);
}

export function removeClassFromBody(className) {
    document.body.classList.remove(className);
}
export function ChangeOrder(idOrder) {
    var orderColumn = document.getElementById(idOrder);
    var otherColumns_asc = document.getElementsByClassName("fas fa-sort-up");
    var otherColumns_desc = document.getElementsByClassName("fas fa-sort-down");

    ResetOrder(otherColumns_asc, idOrder);
    ResetOrder(otherColumns_desc, idOrder);

    if (orderColumn == undefined) {
        return;
    }
    var className = orderColumn.className;
    if (className == undefined) {
        return;
    }
    var cleanName = className.replace("fas ", "");
    if (cleanName == ("fa-sort") || cleanName == ("fa-sort-down")) {
        orderColumn.className = "fas fa-sort-up";
    }
    else if (cleanName.includes("fa-sort-up")) {
        orderColumn.className = "fas fa-sort-down";
    }
}

function ResetOrder(list, idOrder) {
    for (var i = 0; i < list.length; i++) {
        var column = list.item(i);

        if (column.id != idOrder) {
            column.className = "fas fa-sort";
        }


    }
}

export function ResetTableDetails() {
    var columnsDetails = document.getElementsByClassName("blockDetail");
    var btnTriggers = document.getElementsByClassName("Trigger");
    if (columnsDetails == undefined || btnTriggers == undefined) {

        console.log("undefined");
        return true;
    }
    for (var i = 0; i < columnsDetails.length; i++) {
        var column = columnsDetails.item(i);
        if (column.className.includes("blockDetail")) {
            column.className = "tr-details";
        }


    }
    for (var x = 0; x < btnTriggers.length; x++) {
        var button = btnTriggers.item(x);
        if (button.className.includes("Trigger")) {
            button.className = "Btn_ShowDetails clickeable";
        }

    }

}
export function DisableButton(idElement) {
    var button = document.getElementById(idElement);
    button.setAttribute('disabled', '');
}

export function EnableButton(idElement) {
    var button = document.getElementById(idElement);
    button.removeAttribute('disabled');
}
