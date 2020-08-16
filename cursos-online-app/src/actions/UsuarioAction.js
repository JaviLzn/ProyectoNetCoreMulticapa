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

export const obtenerUsuarioActual = () => {
    return new Promise((resolve, reject) => instancia.get('/Usuario').then((response) => resolve(response)));
};

export const actualizarUsuario = (usuario) => {
    return new Promise((resolve, reject) => HttpCliente.put('/Usuario', usuario).then((response) => resolve(response)));
};

export const loginUsuario = (usuario) => {
    return new Promise((resolve, reject) => instancia.post('/Usuario/login', usuario).then((response) => resolve(response)));
};
