import HttpCliente from '../servicios/HttpCliente';

export const register = usuario => {
    return new Promise((resolve, eject) =>{
        HttpCliente.post('/Usuario/registrar', usuario).then(response => {
            resolve(response);
        });
    });
};
 



