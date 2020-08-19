import React, { Fragment, useState } from 'react';
import { Toolbar, IconButton, Typography, Button, Avatar, Drawer } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import MoreVertIcon from '@material-ui/icons/MoreVert';

import { makeStyles } from '@material-ui/core/styles';
import { useStateValue } from '../../../context/store';
import MenuIzquierda from './MenuIzquierda';

const useStyles = makeStyles((theme) => ({
    seccionDesktop: {
        display: 'none',
        [theme.breakpoints.up('md')]: {
            display: 'flex',
        },
    },
    seccionMobile: {
        display: 'flex',
        [theme.breakpoints.up('md')]: {
            display: 'none',
        },
    },
    grow: {
        flexGrow: 1,
    },
    avatarSize: {
        width: 40,
        height: 40,
    },
    list: {
        width: 250,
    },
    listItemText: {
        fontSize: '14px',
        fontWeight: 600,
        paddingLeft: '15px',
        color: '#212121',
    },
}));

const BarSesion = () => {
    const classes = useStyles();

    const [{ sesionUsuario }] = useStateValue();

    const [abrirMenuIzq, setAbrirMenuIzq] = useState(false);

    const cerrarMenuIzq = () => {
        setAbrirMenuIzq(false);
    };

    const abrirMenuIzqAction = () => {
        setAbrirMenuIzq(true);
    };
    

    return (
        <Fragment>
            <Drawer open={abrirMenuIzq} onClose={cerrarMenuIzq} anchor='left' >
                <MenuIzquierda classes={classes}/>
            </Drawer>
            <Toolbar>
                <IconButton color='inherit' onClick={abrirMenuIzqAction}>
                    <MenuIcon />
                </IconButton>
                <Typography variant='h6' className={classes.grow}>
                    Cursos Online
                </Typography>

                <div className={classes.seccionDesktop}>
                    <Button color='inherit'>{sesionUsuario ? sesionUsuario.usuario.NombreCompleto : ''}</Button>
                    <Avatar />
                    <Button color='inherit'>Salir</Button>
                </div>

                <div className={classes.seccionMobile}>
                    <IconButton color='inherit'>
                        <MoreVertIcon />
                    </IconButton>
                </div>
            </Toolbar>
        </Fragment>
    );
};

export default BarSesion;
