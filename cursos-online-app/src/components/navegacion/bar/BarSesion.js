import React from 'react';
import { Toolbar, IconButton, Typography, Button, Avatar } from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import { makeStyles } from '@material-ui/core/styles';
import { useStateValue } from '../../../context/store';

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
}));

const BarSesion = () => {
    const classes = useStyles();

     const [{ sesionUsuario }] = useStateValue();

    return (
        <Toolbar>
            <IconButton color='inherit'>
                <MenuIcon />
            </IconButton>
            <Typography variant='h6' className={classes.grow}>Cursos Online</Typography>
            

            <div className={classes.seccionDesktop}>
                <Button color='inherit'>
                    {sesionUsuario ? sesionUsuario.usuario.NombreCompleto: ''}
                </Button>
                <Avatar />
                <Button color='inherit'>Salir</Button>
            </div>

            <div className={classes.seccionMobile}>
                <IconButton color='inherit'>
                    <MoreVertIcon />
                </IconButton>
            </div>
        </Toolbar>
    );
};

export default BarSesion;
