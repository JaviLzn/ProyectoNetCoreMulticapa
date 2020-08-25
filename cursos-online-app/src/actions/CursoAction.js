import HttpCliente from '../servicios/HttpCliente';

export const guardarCurso = async (curso, imagen) => {
    const endPointCurso = '/Cursos';
    const promesaCurso = HttpCliente.post(endPointCurso, curso);

    if (imagen) {
        const endPointImagen = '/Documento';
        const promesaImagen = HttpCliente.post(endPointImagen, imagen);
        return await Promise.all([promesaCurso, promesaImagen].map((p) => p.catch((e) => e)));
    }

    return await Promise.all([promesaCurso].map((p) => p.catch((e) => e)));
};

export const paginacionCurso = (paginador) => {
    return new Promise((resolve, reject) => {
        HttpCliente.post('/Cursos/report', paginador).then((response) => {
            resolve(response);
        });
    });
};
