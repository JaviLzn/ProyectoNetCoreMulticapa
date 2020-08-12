import React from 'react';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';
import style from '../Tool/Style';

const RegistrarUsuario = () => {
    return (
        <Container component='main' maxWidth='xs' justify='center' style={style.containerCenterXY}>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Registro de usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2} justify='center'>
                        <Grid item xs={12} >
                            <TextField name='nombre' variant='outlined' fullWidth label="Ingrese su nombre" />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='apellidos' variant='outlined' fullWidth label="Ingrese sus apellidos" />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='email' type='email' variant='outlined' fullWidth label="Ingrese su email" />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='username' variant='outlined' fullWidth label="Ingrese su Nombre de Usuario" />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='password' type='password' variant='outlined' fullWidth label="Ingrese su contraseña" />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='confirmpassword' type='password' variant='outlined' fullWidth label="Ingrese nuevamente su contraseña" />
                        </Grid>
                    </Grid>
                    <Grid container justify='center' >
                        <Grid item xs={12} >
                            <Button type='submit' fullWidth variant='contained' color='primary' size='large' style={style.submit}>
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
