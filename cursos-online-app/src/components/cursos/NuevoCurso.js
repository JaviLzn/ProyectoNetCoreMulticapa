import React, { useState } from 'react';
import style from '../Tool/Style';
import { Typography, Container, Grid, TextField, Button } from '@material-ui/core';
import DateFnsUtils from '@date-io/date-fns';
import esLocale from 'date-fns/locale/es';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import ImageUploader from 'react-images-upload';
import { v4 as uuidv4 } from 'uuid';
import { obtenerDataImagen } from '../../actions/ImagenAction';

const NuevoCurso = () => {
    const [fechaSeleccionada, setFechaSeleccionada] = useState(new Date());

    const [imagenCurso, setImagenCurso] = useState(null);

    const [curso, setCurso] = useState({
        Titulo: '',
        Descripcion: '',
        Precio: 0.0,
        Promocion: 0.0,
    });

    const cargarValoresCurso = (e) => {
        const { name, value } = e.target;
        setCurso((anterior) => ({ ...anterior, [name]: value }));
    };

    const subirFoto = (imagenes) => {
        var foto = imagenes[0];

        obtenerDataImagen(foto).then((respuesta) => {
            setImagenCurso(respuesta);
        });
    };

    const fotoKey = uuidv4();

    return (
        <Container component='main' maxWidth='xs' justify='center'>
            <div style={style.paper}>
                <Typography component='h1' variant='h5'>
                    Registro de Nuevo Curso
                </Typography>
                <form style={style.form}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField name='Titulo' value={curso.Titulo} onChange={cargarValoresCurso} variant='outlined' fullWidth label='Ingrese Titulo' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Descripcion' value={curso.Descripcion} onChange={cargarValoresCurso} variant='outlined' fullWidth label='Ingrese Descripcion' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Precio' value={curso.Precio} onChange={cargarValoresCurso} variant='outlined' fullWidth label='Ingrese Precio Normal' />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField name='Promocion' value={curso.Promocion} onChange={cargarValoresCurso} variant='outlined' fullWidth label='Ingrese Precio de promoción' />
                        </Grid>
                        <Grid item xs={12}>
                            <MuiPickersUtilsProvider utils={DateFnsUtils} locale={esLocale}>
                                <KeyboardDatePicker
                                    value={fechaSeleccionada}
                                    onChange={setFechaSeleccionada}
                                    margin='normal'
                                    id='fecha-publicacion-id'
                                    label='Seleccione fecha de publicacion'
                                    format='dd/MM/yyyy'
                                    fullWidth
                                    KeyboardButtonProps={{
                                        'aria-label': 'change date',
                                    }}
                                />
                            </MuiPickersUtilsProvider>
                        </Grid>
                        <Grid item xs={12}>
                            <ImageUploader withIcon={false} key={fotoKey} singleImage={true} buttonText='Seleccione una imagen para el curso' onChange={subirFoto} imgExtension={['.jpg', '.gif', '.png', '.jpeg']} maxFileSize={5242880} label={`Tamaño máximo de archivo: 5Mb. Acepta: jpg | gif | png | jpeg`} />
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
