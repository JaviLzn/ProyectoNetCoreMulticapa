import HttpCliente from '../servicios/HttpCliente';

export const guardarCurso = async (curso, imagen) => {
    const endPointCurso = '/Cursos';
    const endPointImagen = '/Documento';

    const promesaCurso = HttpCliente.post(endPointCurso, curso);
    const promesaImagen = HttpCliente.post(endPointImagen, imagen);

    const responseArray = await Promise.all([promesaCurso, promesaImagen]);
    return responseArray;
};
