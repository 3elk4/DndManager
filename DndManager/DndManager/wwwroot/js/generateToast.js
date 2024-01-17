function createToastElement(type, content) {
    let toastContainer = document.createElement('div');
    toastContainer.classList.add('toast-container');

    let toast = document.createElement('div');
    toast.classList.add('toast');

    let toastBody = document.createElement('div');
    toastBody.classList.add('toast-body', type);
    toastBody.innerText = content;

    toast.appendChild(toastBody);
    toastContainer.appendChild(toast);

    if ($('.wrapping-element').length <= 0) {
        let wrappingElement = document.createElement('div');
        wrappingElement.classList.add('wrapping-element');
        wrappingElement.appendChild(toastContainer);
        document.body.appendChild(wrappingElement);
    }
    else {
        $('.wrapping-element').append(toastContainer);
    }
}

function toast(type, content) {
    createToastElement(type, content);
    let toastElement = $('.toast');
    toastElement.toast({
        delay: type === 'danger' || type === 'warning' ? 20000 : 3000,
    });
    toastElement.toast('show');
    toastElement.on('hidden.bs.toast', function () {
        toastElement.remove();
    });
}