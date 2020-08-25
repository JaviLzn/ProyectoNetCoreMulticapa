import React, { useState, useEffect, Fragment } from 'react';
import { paginacionCurso } from '../../actions/CursoAction';
import { TableContainer, Paper, Table, TableHead, TableBody, TableRow, TableCell } from '@material-ui/core';

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

    return (
        <Fragment>
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell align='left'>Cursos</TableCell>
                            <TableCell align='left'>Descripción</TableCell>
                            <TableCell align='left'>Fecha de Publicación</TableCell>
                            <TableCell align='left'>Precio Actual</TableCell>
                            <TableCell align='left'>Precio Promoción</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {paginadorReponse.ListaRegistros.map((curso) => (
                            <TableRow key={curso.Titulo}>
                                <TableCell align='left'>{curso.Titulo}</TableCell>
                                <TableCell align='left'>{curso.Descripcion}</TableCell>
                                <TableCell align='left'>{curso.FechaPublicacion}</TableCell>
                                <TableCell align='left'>{curso.PrecioActual}</TableCell>
                                <TableCell align='left'>{curso.Promocion}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Fragment>
    );
};

export default PaginadorCurso;
