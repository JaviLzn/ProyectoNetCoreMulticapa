import sesionUsuarioReducer from './sesionUsuarioReducer';
import openSnackbarReducer from './openSnackbarReducer';

export const mainReducer = ({ sesionUsuario, openSnackbar }, action) => {
    return {
        sesionUsuario: sesionUsuarioReducer(sesionUsuario, action),
        openSnackbar: openSnackbarReducer(openSnackbar, action),
    };
};
