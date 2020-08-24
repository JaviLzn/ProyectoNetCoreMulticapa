import React from 'react';
import { AppBar } from '@material-ui/core';
import BarSesion from './bar/BarSesion';
import { useStateValue } from '../../context/store';

const AppNavbar = () => {
    const [{ sesionUsuario }] = useStateValue();

    return sesionUsuario ? (
        sesionUsuario.autenticado === true ? (
            <AppBar position='static'>
                <BarSesion />
            </AppBar>
        ) : null
    ) : null;
};

export default AppNavbar;
