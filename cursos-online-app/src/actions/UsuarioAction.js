import HttpCliente from '../servicios/HttpCliente';
import axios from 'axios';

const instancia = axios.create();
instancia.CancelToken = axios.CancelToken;
instancia.isCancel = axios.isCancel;

export const register = (usuario) => {
    return new Promise((resolve, reject) => {
        instancia.post('/Usuario/registrar', usuario).then((response) => resolve(response));
    });
};

export const obtenerUsuarioActual = (dispatch) => {
    return new Promise((resolve, reject) =>
        HttpCliente.get('/Usuario').then((response) => {
            if (response.data && response.data.ImagenPerfil) {
                let fotoPerfil = response.data.ImagenPerfil;
                const nuevoArchivo = `data:image/${fotoPerfil.Extension};base64,${fotoPerfil.Data}`;
                response.data.ImagenPerfil = nuevoArchivo;
            }

            dispatch({
                type: 'INICIAR_SESION',
                sesion: response.data,
                autenticado: true,
            });
            resolve(response);
        })
    );
};

export const actualizarUsuario = (usuario, dispatch) => {
    return new Promise((resolve, reject) => {
        HttpCliente.put('/Usuario', usuario)
            .then((response) => {
                if (response.data && response.data.ImagenPerfil) {
                    let fotoPerfil = response.data.ImagenPerfil;
                    const nuevoArchivo = `data:image/${fotoPerfil.Extension};base64,${fotoPerfil.Data}`;
                    response.data.ImagenPerfil = nuevoArchivo;
                }

                dispatch({
                    type: 'INICIAR_SESION',
                    sesion: response.data,
                    autenticado: true,
                });
                
                resolve(response);
            })
            .catch((err) => {
                resolve(err.response);
            });
    });
};

export const loginUsuario = (usuario) => {
    return new Promise((resolve, reject) => instancia.post('/Usuario/login', usuario).then((response) => resolve(response)));
};
