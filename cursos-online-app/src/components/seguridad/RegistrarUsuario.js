import React, { useState } from 'react';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../Tool/Style';
import { register } from '../../actions/UsuarioAction';

const RegistrarUsuario = () => {

    const [usuario, setUsuario] = useState({
        NombreCompleto: '',
        Email: '',
        Password: '',
        ConfirmarPassword: '',
        UserName: ''
    });

    const ingresarValores = (e) => {
        const {name, value} = e.target;
        setUsuario({...usuario, [name]: value});
    };

    const Registrar = (e) => {
        e.preventDefault();
        register(usuario).then((response) => {
            console.log('se registró', response);
            window.localStorage.setItem("token_seguridad", response.data.Token);
        });

        
    };

    return (
        <Container component='main' maxWidth='md' justify='center' style={style.containerCenterXY}>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Registro de usuario
                </Typography>
                <form style={style.form} autoComplete='off'>
                    <Grid container spacing={2} justify='center'>
                        <Grid item xs={12} md={12}>
                            <TextField name='NombreCompleto'  onChange={ingresarValores} variant='outlined' fullWidth label="Ingrese su nombre y apellidos" />
                        </Grid>
                        <Grid item xs={12} md={6} >
                            <TextField name='UserName'  onChange={ingresarValores} variant='outlined' fullWidth label="Ingrese su Nombre de Usuario" />
                        </Grid>
                        <Grid item xs={12} md={6} >
                            <TextField name='Email'  onChange={ingresarValores} type='email' variant='outlined' fullWidth label="Ingrese su email" />
                        </Grid>
                        <Grid item xs={12} md={6} >
                            <TextField name='Password'  onChange={ingresarValores} type='password' variant='outlined' fullWidth label="Ingrese su contraseña" />
                        </Grid>
                        <Grid item xs={12} md={6} >
                            <TextField name='ConfirmarPassword'  onChange={ingresarValores} type='password' variant='outlined' fullWidth label="Ingrese nuevamente su contraseña" />
                        </Grid>
                    </Grid>
                    <Grid container justify='center' >
                        <Grid item xs={12} >
                            <Button type='submit' onClick={Registrar} fullWidth variant='contained' color='primary' size='large' style={style.submit}>
                                Enviar
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
};

export default RegistrarUsuario;
