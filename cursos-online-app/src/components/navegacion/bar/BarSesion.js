import React, { useState, Fragment } from 'react';
import { Toolbar, IconButton, Typography, Button, Avatar, Drawer } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import { makeStyles } from '@material-ui/styles';
import { useStateValue } from '../../../context/store';
import MenuIzquierda from './MenuIzquierda';
import MenuDerecha from './MenuDerecha';
import { useHistory } from 'react-router-dom';

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
    let history = useHistory();

    const [{ sesionUsuario }] = useStateValue();

    const [menuIzq, setMenuIzq] = useState(false);
    const [menuDer, setMenuDer] = useState(false);

    const abrirMenuIzq = () => setMenuIzq(true);
    const cerrarMenuIzq = () => setMenuIzq(false);
    const abrirMenuDer = () => setMenuDer(true);
    const cerrarMenuDer = () => setMenuDer(false);

    const salirSesion = () => {
        localStorage.removeItem('token_seguridad');
        history.push('/auth/login');
    };

    return (
        <Fragment>
            <Drawer anchor='left' open={menuIzq} onClose={cerrarMenuIzq}>
                <div className={classes.list} onClick={cerrarMenuIzq} onKeyDown={cerrarMenuIzq}>
                    <MenuIzquierda classes={classes} />
                </div>
            </Drawer>
            <Drawer anchor='right' open={menuDer} onClose={cerrarMenuDer}>
                <div className={classes.list} onClick={cerrarMenuDer} onKeyDown={cerrarMenuDer}>
                    <MenuDerecha classes={classes} usuario={sesionUsuario ? sesionUsuario.usuario : null} salirSesion={salirSesion} />
                </div>
            </Drawer>
            <Toolbar>
                <IconButton color='inherit' onClick={abrirMenuIzq}>
                    <MenuIcon />
                </IconButton>
                <Typography variant='h6' className={classes.grow}>
                    Cursos Online
                </Typography>

                <div className={classes.seccionDesktop}>
                    <Button color='inherit'>{sesionUsuario ? sesionUsuario.usuario.NombreCompleto : ''}</Button>
                    <Avatar src={sesionUsuario.usuario.ImagenPerfil} />
                    <Button color='inherit' onClick={salirSesion}>
                        Salir
                    </Button>
                </div>

                <div className={classes.seccionMobile}>
                    <IconButton color='inherit' onClick={abrirMenuDer}>
                        <MoreVertIcon />
                    </IconButton>
                </div>
            </Toolbar>
        </Fragment>
    );
};

export default BarSesion;
