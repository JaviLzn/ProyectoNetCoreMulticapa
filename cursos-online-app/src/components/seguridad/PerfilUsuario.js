import React, { useState, useEffect } from 'react';
import style from '../Tool/Style';
import { Container, Typography, Grid, TextField, Button, Avatar } from '@material-ui/core';
import { actualizarUsuario } from '../../actions/UsuarioAction';
import { useStateValue } from '../../context/store';
import { v4 as uuidv4 } from 'uuid';
import ImageUploader from 'react-images-upload';
import { obtenerDataImagen } from '../../actions/ImagenAction';


const PerfilUsuario = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();

    const [usuario, setUsuario] = useState({
        NombreCompleto: '',
        Email: '',
        UserName: '',
        Password: '',
        ConfirmarPassword: '',
        ImagenPerfil: null,
        FotoUrl: '',
    });

    const IngresarValores = (e) => {
        const { name, value } = e.target;
        setUsuario({ ...usuario, [name]: value });
    };

    useEffect(() => {
        setUsuario(sesionUsuario.usuario);
        setUsuario((anterior) => ({ ...anterior, FotoUrl: sesionUsuario.usuario.ImagenPerfil }));
        // eslint-disable-next-line
    }, []);

    const guardarUsuario = (e) => {
        e.preventDefault();
        actualizarUsuario(usuario).then((response) => {
            if (response.status === 200) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    openMensaje: { open: true, mensaje: 'Se guardó correctamente el perfil del usuario' },
                });
                window.localStorage.setItem('token_seguridad', response.data.Token);
            } else {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    openMensaje: { open: true, mensaje: 'Errores al intentar guardar en:' + Object.keys(response.data.errors) },
                });
            }
        });
    };

    const fotoKey = uuidv4();
    const subirFoto = (imagenes) => {
        const foto = imagenes[0];
        const FotoUrl = URL.createObjectURL(foto);

        obtenerDataImagen(foto).then((respuesta) => {
            setUsuario({ ...usuario, ImagenPerfil: respuesta, FotoUrl });
        });
    };

    return (
        <Container component='main' maxWidth='xs' justify='center'>
            <div style={style.paper}>
                <Avatar style={style.avatar} src={usuario.FotoUrl ? usuario.FotoUrl : null} />
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
                        <Grid item xs={12}>
                            <ImageUploader withIcon={false} key={fotoKey} singleImage={true} buttonText='Seleccione una imagen de perfil' onChange={subirFoto} imgExtension={['.jpg', '.gif', '.png', '.jpeg']} maxFileSize={5242880} label={`Tamaño máximo de archivo: 5Mb. Acepta: jpg | gif | png | jpeg`} />
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
