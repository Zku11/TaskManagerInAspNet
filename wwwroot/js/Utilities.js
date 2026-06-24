async function handleErrorApi(response) {
    let errorMessage = '';
    if (response.status === 400) {
        errorMessage = await response.text();
    }
    else if (response.status === 404) {
        errorMessage = resourceNotFound;
    }
    else {
        errorMessage = unexpectedError;
    }
    ShowErrorMessage(errorMessage);
}

function ShowErrorMessage(message) {
    Swal.fire({
        icon: 'error',
        title: '¡Error!',
        text: message
    });
}