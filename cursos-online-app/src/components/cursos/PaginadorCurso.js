import React, { useState, useEffect } from 'react';
import { paginacionCurso } from '../../actions/CursoAction';

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

        paginacionCurso(objetoPaginadorRequest).then((response) => {
            console.log('response paginador :>> ', response);
        });
    }, [paginadorRequest]);

    return (
        <div>
            <div> </div>
        </div>
    );
};

export default PaginadorCurso;
