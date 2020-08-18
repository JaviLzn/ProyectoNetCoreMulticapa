import React, { useEffect } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { ThemeProvider } from '@material-ui/core/styles';
import theme from './theme';
import Login from './components/seguridad/Login';
import RegistrarUsuario from './components/seguridad/RegistrarUsuario';
import PerfilUsuario from './components/seguridad/PerfilUsuario';
import AppNavbar from './components/navegacion/AppNavbar';
import { useStateValue } from './context/store';
import { obtenerUsuarioActual } from './actions/UsuarioAction';

function App() {
    const [, dispatch] = useStateValue();

    useEffect(() => {
        obtenerUsuarioActual(dispatch);
    },[]);

    return (
        <Router>
            <ThemeProvider theme={theme}>
                <AppNavbar />
                <Switch>
                    <Route exact path='/' component={Login} />
                    <Route exact path='/auth/login' component={Login} />
                    <Route exact path='/auth/registrar' component={RegistrarUsuario} />
                    <Route exact path='/auth/perfil' component={PerfilUsuario} />
                </Switch>
            </ThemeProvider>
        </Router>
    );
}

export default App;
