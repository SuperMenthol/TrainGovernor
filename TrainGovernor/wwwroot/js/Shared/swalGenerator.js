export function generateSwal(response, actionOnSuccess) {
    let act = response.success ? actionOnSuccess : null;

    swal.fire({
        icon: response.success ? 'success' : 'error',
        title: response.success ? 'Success' : 'Error',
        text: response.message
    })
        .then(() => act);
}