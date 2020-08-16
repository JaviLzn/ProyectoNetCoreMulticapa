import React from 'react';
import { AppBar } from '@material-ui/core';
import BarSesion from './bar/BarSesion';

const AppNavbar = () => {
    return (
        <AppBar position='static'>
            <BarSesion />
        </AppBar>
    );
};

export default AppNavbar;
