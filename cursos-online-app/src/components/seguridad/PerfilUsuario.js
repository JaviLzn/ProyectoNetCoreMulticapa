import React from 'react'
import style from '../Tool/Style';
import { Container, Typography, Grid, TextField, Button } from '@material-ui/core';


const PerfilUsuario = () => {
    return (
        <Container component='main' maxWidth='xs' justify='center' >
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Perfil del Usuario
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12} >
                            <TextField name='nombrecompleto' variant='outlined' fullWidth label='Nombres y Apellidos' />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='email' variant='outlined' fullWidth label='Email' />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='password' type='password' variant='outlined' fullWidth label='Contraseña' />
                        </Grid>
                        <Grid item xs={12} >
                            <TextField name='confirmpassword' type='password' variant='outlined' fullWidth label='Confirmar contraseña' />
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12} >
                            <Button type='submit' fullWidth variant='contained' size='large' color='primary' style={style.submit}>
                                Guardar datos
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
}

export default PerfilUsuario;