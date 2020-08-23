export const obtenerDataImagen = (imagen) => {
    return new Promise((resolve, reject) => {
        const Nombre = imagen.name;
        const Extension = imagen.name.split('.').pop();
        const lector = new FileReader();
        lector.readAsDataURL(imagen);
        lector.onload = () =>
            resolve({
                Data: lector.result.split(',')[1],
                Nombre,
                Extension,
            });
        lector.onerror = (err) => PromiseRejectionEvent(err);
    });
};
