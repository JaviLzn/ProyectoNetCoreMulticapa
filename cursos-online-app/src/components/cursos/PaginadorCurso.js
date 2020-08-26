import React, { useState, useEffect } from 'react';
import { paginacionCurso } from '../../actions/CursoAction';
import { TableContainer, Paper, Table, TableHead, TableBody, TableRow, TableCell, TablePagination, Hidden, Grid, TextField, Container } from '@material-ui/core';
import ControlTyping from '../Tool/ControlTyping';

const PaginadorCurso = () => {
    const [textoBusquedaCurso, setTextoBusquedaCurso] = useState('');
    const typingBuscadorTexto = ControlTyping(textoBusquedaCurso, 800);

    const [paginadorRequest, setPaginadorRequest] = useState({
        Titulo: '',
        NumeroPagina: 0,
        ElementosPagina: 5,
    });

    const [paginadorReponse, setPaginadorReponse] = useState({
        ListaRegistros: [],
        TotalRegistros: 0,
        CantidadPaginas: 0,
    });

    useEffect(() => {
        const obtenerListaCurso = async () => {
            let Titulo = '';
            let NumeroPagina = paginadorRequest.NumeroPagina + 1;
            if (typingBuscadorTexto) {
                Titulo = typingBuscadorTexto;
                NumeroPagina = 1;
            }

            const objetoPaginadorRequest = {
                Titulo,
                NumeroPagina,
                ElementosPagina: paginadorRequest.ElementosPagina,
            };

            const response = await paginacionCurso(objetoPaginadorRequest);
            setPaginadorReponse(response.data);
        };
        obtenerListaCurso();
    }, [paginadorRequest, typingBuscadorTexto]);

    const cambiarPagina = (event, nuevaPagina) => {
        setPaginadorRequest((anterior) => ({ ...anterior, NumeroPagina: parseInt(nuevaPagina) }))
    }

    const cambiarCantidadRegistros = (e) => {
        setPaginadorRequest((anterior) => ({ ...anterior, ElementosPagina: parseInt(e.target.value), NumeroPagina: 0 }))
    }

    return (
        <Container component='main' style={{ padding: '10px', width: '100%' }}>
            <Grid container style={{ paddingTop: '20px', paddingBottom: '20px' }}>
                <Grid item xs={12}>
                    <TextField onChange={e => setTextoBusquedaCurso(e.target.value)} fullWidth name='textoBusquedaCurso' variant='outlined' label='Busca tu curso' />
                </Grid>
            </Grid>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell align='left'>Cursos</TableCell>
                            <Hidden smDown>
                                <TableCell align='left'>Descripción</TableCell>
                                <TableCell align='left'>Fecha de Publicación</TableCell>
                                <TableCell align='left'>Precio Actual</TableCell>
                                <TableCell align='left'>Precio Promoción</TableCell>
                            </Hidden>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {paginadorReponse.ListaRegistros.map((curso) => (
                            <TableRow key={curso.Titulo}>
                                <TableCell align='left'>{curso.Titulo}</TableCell>
                                <Hidden smDown>
                                    <TableCell align='left'>{curso.Descripcion}</TableCell>
                                    <TableCell align='left'>{(new Date(curso.FechaPublicacion)).toLocaleString()}</TableCell>
                                    <TableCell align='left'>{curso.PrecioActual}</TableCell>
                                    <TableCell align='left'>{curso.Promocion}</TableCell>
                                </Hidden>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <TablePagination
                rowsPerPageOptions={[5, 10, 25]}
                count={paginadorReponse.TotalRegistros}
                rowsPerPage={paginadorRequest.ElementosPagina}
                page={paginadorRequest.NumeroPagina}
                onChangePage={cambiarPagina}
                onChangeRowsPerPage={cambiarCantidadRegistros}
                labelRowsPerPage='Cursos por página'
            />
        </Container>
    );
};

export default PaginadorCurso;
