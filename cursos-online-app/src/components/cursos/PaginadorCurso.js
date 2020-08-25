import React, { useState, useEffect, Fragment } from 'react';
import { paginacionCurso } from '../../actions/CursoAction';
import { TableContainer, Paper, Table, TableHead, TableBody, TableRow, TableCell, TablePagination, Hidden } from '@material-ui/core';

const PaginadorCurso = () => {
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
        const objetoPaginadorRequest = {
            Titulo: paginadorRequest.Titulo,
            NumeroPagina: paginadorRequest.NumeroPagina + 1,
            ElementosPagina: paginadorRequest.ElementosPagina,
        };

        const obtenerListaCurso = async () => {
            const response = await paginacionCurso(objetoPaginadorRequest);
            setPaginadorReponse(response.data);
        };
        obtenerListaCurso();
    }, [paginadorRequest]);

    const cambiarPagina = (event, nuevaPagina) => {
        setPaginadorRequest((anterior) => ({ ...anterior, NumeroPagina: parseInt(nuevaPagina) }))
    }

    const cambiarCantidadRegistros = (e) => {
        setPaginadorRequest((anterior) => ({ ...anterior, ElementosPagina: parseInt(e.target.value), NumeroPagina: 0 }))
    }

    return (
        <Fragment>
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
        </Fragment>
    );
};

export default PaginadorCurso;
