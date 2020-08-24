import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { ThemeProvider } from '@material-ui/core/styles';
import theme from './theme';
import Login from './components/seguridad/Login';
import RegistrarUsuario from './components/seguridad/RegistrarUsuario';
import PerfilUsuario from './components/seguridad/PerfilUsuario';
import AppNavbar from './components/navegacion/AppNavbar';
import { useStateValue } from './context/store';
import { obtenerUsuarioActual } from './actions/UsuarioAction';
import { Snackbar } from '@material-ui/core';
import RutaSegura from './components/navegacion/RutaSegura';

function App() {
    const [{ openSnackbar }, dispatch] = useStateValue();

    const [iniciaApp, setIniciaApp] = useState(false);

    useEffect(() => {
        if (!iniciaApp) {
            obtenerUsuarioActual(dispatch)
                .then(() => setIniciaApp(true))
                .catch(() => setIniciaApp(true));
        }
        // eslint-disable-next-line
    }, [iniciaApp]);

    return iniciaApp === false ? null : (
        <React.Fragment>
            <Snackbar
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                open={openSnackbar ? openSnackbar.open : false}
                autoHideDuration={3000}
                ContentProps={{ 'aria-describedby': 'message-id' }}
                message={<span id='message-id'>{openSnackbar ? openSnackbar.mensaje : ''}</span>}
                onClose={() => {
                    dispatch({
                        type: 'OPEN_SNACKBAR',
                        openMensaje: { open: false, mensaje: '' },
                    });
                }}
            />
            <Router>
                <ThemeProvider theme={theme}>
                    <AppNavbar />
                    <Switch>
                        <Route exact path='/auth/login' component={Login} />
                        <Route exact path='/auth/registrar' component={RegistrarUsuario} />
                        <RutaSegura exact path='/auth/perfil' component={PerfilUsuario} />
                        <RutaSegura exact path='/' component={PerfilUsuario} />
                    </Switch>
                </ThemeProvider>
            </Router>
        </React.Fragment>
    );
}

export default App;
