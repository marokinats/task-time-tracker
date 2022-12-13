let myModal;

function showModal(modalId) {
    myModal = new bootstrap.Modal(document.getElementById(modalId), {});
    myModal.show();
}

function hideModal(modalId) {
    myModal.hide();
}
