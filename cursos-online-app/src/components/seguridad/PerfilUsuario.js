import React, { useState, useEffect } from 'react';
import style from '../Tool/Style';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import { obtenerUsuarioActual, actualizarUsuario } from '../../actions/UsuarioAction';

const PerfilUsuario = () => {
    const [usuario, setUsuario] = useState({
        NombreCompleto: '',
        Email: '',
        UserName: '',
        Password: '',
        ConfirmarPassword: '',
    });

    const IngresarValores = (e) => {
        const { name, value } = e.target;
        setUsuario({ ...usuario, [name]: value });
    };

    useEffect(() => {
        obtenerUsuarioActual().then((response) => {
            console.log('response obtenerUsuarioActual :>> ', response);
            setUsuario(response.data);
        });
    }, []);

    const guardarUsuario = (e) => {
        e.preventDefault();
        actualizarUsuario(usuario).then((response) => {
            console.log('response guardarUsuario', response);
            window.localStorage.setItem('token_seguridad', response.data.Token);
        });
    };

    return (
        <Container component='main' maxWidth='xs' justify='center'>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Perfil del Usuario
                </Typography>
                <form style={style.form} onSubmit={guardarUsuario}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField name='NombreCompleto' value={usuario.NombreCompleto} onChange={IngresarValores} variant='outlined' fullWidth label='Nombres y Apellidos' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Email' value={usuario.Email} onChange={IngresarValores} type='email' variant='outlined' fullWidth label='Email' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='UserName' value={usuario.UserName} onChange={IngresarValores} variant='outlined' fullWidth label='Nombre de Usuario' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Password' onChange={IngresarValores} type='password' variant='outlined' fullWidth label='Contraseña' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='ConfirmarPassword' onChange={IngresarValores} type='password' variant='outlined' fullWidth label='Confirmar contraseña' />
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12}>
                            <Button type='submit' fullWidth variant='contained' size='large' color='primary' style={style.submit}>
                                Guardar datos
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
};

export default PerfilUsuario;
