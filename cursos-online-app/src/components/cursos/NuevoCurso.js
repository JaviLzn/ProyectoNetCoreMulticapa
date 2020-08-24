import React from 'react';
import style from '../Tool/Style';
import { Typography, Container, Grid, TextField, Button } from '@material-ui/core';

const NuevoCurso = () => {
    return (
        <Container component='main' maxWidth='xs' justify='center'>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Registro de Nuevo Curso
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField name='Titulo' variant='outlined' fullWidth label='Ingrese Titulo' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Descripcion' variant='outlined' fullWidth label='Ingrese Descripcion' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Precio' variant='outlined' fullWidth label='Ingrese Precio Normal' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Promocion' variant='outlined' fullWidth label='Ingrese Precio de promoción' />
                        </Grid>
                    </Grid>
                    <Grid container justify='center'>
                        <Grid item xs={12}>
                            <Button type='submit' fullWidth variant='contained' color='primary' size='large' style={style.submit}>
                                Guardar Curso
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </div>
        </Container>
    );
};

export default NuevoCurso;
