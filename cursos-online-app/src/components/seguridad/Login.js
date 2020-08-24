import React, { useState } from 'react';
import style from '../Tool/Style';
import { Container, Avatar, Typography, TextField, Button } from '@material-ui/core';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { loginUsuario } from '../../actions/UsuarioAction';
import { useHistory } from 'react-router-dom';
import { useStateValue } from '../../context/store';

const Login = () => {
    const [, dispatch] = useStateValue();
    let history = useHistory();

    const [usuario, setUsuario] = useState({
        Email: '',
        Password: '',
        // Email: 'jlozanoo@gmail.com',
        // Password: 'Password123$',
    });

    const capturarValores = (e) => {
        const { name, value } = e.target;
        setUsuario({ ...usuario, [name]: value });
    };

    const logear = (e) => {
        e.preventDefault();

        loginUsuario(usuario, dispatch).then((response) => {
            if (response.status === 200) {
                window.localStorage.setItem('token_seguridad', response.data.Token);
                history.push('/');
            } else {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    openMensaje: {
                        open: true,
                        mensaje: 'Las credenciales del usuario son incorrectas.',
                    },
                });
            }
        });
    };

    return (
        <Container maxWidth='xs' style={style.containerCenterXY}>
            <div style={style.paper}>
                <Avatar style={style.avatar}>
                    <LockOutlinedIcon style={style.icon} />
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Iniciar sesión
                </Typography>
                <form style={style.form} autoComplete='off'>
                    <TextField name='Email' variant='outlined' onChange={capturarValores} label='Ingrese su correo electrónico' fullWidth margin='normal' />
                    <TextField name='Password' variant='outlined' onChange={capturarValores} label='Ingrese su contraseña' type='password' fullWidth margin='normal' />
                    <Button type='submit' onClick={logear} fullWidth variant='contained' color='primary' style={style.submit}>
                        ENVIAR
                    </Button>
                </form>
            </div>
        </Container>
    );
};

export default Login;
