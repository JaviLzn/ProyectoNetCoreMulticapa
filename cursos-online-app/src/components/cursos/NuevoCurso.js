import React, { useState } from 'react';
import style from '../Tool/Style';
import { Typography, Container, Grid, TextField, Button } from '@material-ui/core';
import DateFnsUtils from '@date-io/date-fns';
import esLocale from 'date-fns/locale/es';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import ImageUploader from 'react-images-upload';
import { v4 as uuidv4 } from 'uuid';
import { obtenerDataImagen } from '../../actions/ImagenAction';
import { guardarCurso } from '../../actions/CursoAction';
import { useStateValue } from '../../context/store';

const NuevoCurso = () => {
    const [, dispatch] = useStateValue();
    const [fechaSeleccionada, setFechaSeleccionada] = useState(new Date());

    const [imagenCurso, setImagenCurso] = useState(null);

    const [curso, setCurso] = useState({
        Titulo: '',
        Descripcion: '',
        Precio: 0.0,
        PrecioPromocion: 0.0,
    });

    const resetearForm = () => {
        setFechaSeleccionada(new Date());
        setImagenCurso(null);
        setCurso({
            Titulo: '',
            Descripcion: '',
            Precio: 0.0,
            PrecioPromocion: 0.0,
        });
    };

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

    const guardarCursoBoton = (e) => {
        e.preventDefault();

        //Validaciones

        const CursoId = uuidv4();

        const objetoCurso = {
            Titulo: curso.Titulo,
            Descripcion: curso.Descripcion,
            Precio: parseFloat(curso.Precio || 0.0),
            PrecioPromocion: parseFloat(curso.PrecioPromocion || 0.0),
            FechaPublicacion: fechaSeleccionada,
            CursoId,
        };

        let objetoImagen = null;
        if (imagenCurso) {
            objetoImagen = {
                ObjectoReferencia: CursoId,
                Data: imagenCurso.Data,
                Nombre: imagenCurso.Nombre,
                Extension: imagenCurso.Extension,
            };
        }

        guardarCurso(objetoCurso, objetoImagen).then((respuestas) => {
            console.log('respuestas arreglo:>> ', respuestas);

            const responseCurso = respuestas[0];
            const responseImagen = respuestas[1];

            let mensaje = '';
            if (responseCurso.status === 200) {
                mensaje += 'Se guardó exitosamente el curso.';
                resetearForm();
            } else {
                mensaje += 'Errores: ' + Object.keys(responseCurso.response.data.errors);
            }

            if (responseImagen) {
                if (responseImagen.status === 200) {
                    mensaje += ' Se guardó la imagen correctamente.';
                } else {
                    mensaje += ' Errores en imagen: ' + Object.keys(responseCurso.response.data.errors);
                }
            }

            dispatch({
                type: 'OPEN_SNACKBAR',
                openMensaje: {
                    open: true,
                    mensaje,
                },
            });
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
                            <TextField name='PrecioPromocion' value={curso.PrecioPromocion} onChange={cargarValoresCurso} variant='outlined' fullWidth label='Ingrese Precio de promoción' />
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
                            <Button type='submit' onClick={guardarCursoBoton} fullWidth variant='contained' color='primary' size='large' style={style.submit}>
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
