import React from 'react';
import style from '../Tool/Style';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';

const Login = () => {
    return (
        <Container maxWidth='xs' style={style.containerCenterXY}>
            <div style={style.paper}>
                <Avatar style={style.avatar} >
                    <LockOutlinedIcon style={style.icon} />
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Iniciar sesión
                </Typography>
                <form style={style.form} autoComplete="off">
                    <TextField variant='outlined' label='Ingrese su nombre de usuario' name='username' fullWidth margin='normal'/>
                    <TextField variant='outlined' label='Ingrese su contraseña' name='password' type='password' fullWidth margin='normal'/>
                    <Button type='submit' fullWidth variant='contained' color='primary' style={style.submit}>
                        ENVIAR
                    </Button>

                </form>
            </div>
        </Container>
    );
};

export default Login;
