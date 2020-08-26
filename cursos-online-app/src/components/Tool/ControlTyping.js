import { useEffect, useState } from 'react';

const ControlTyping = (texto, delay) => {
    const [textoValor, setTextoValor] = useState();

    useEffect(() => {
        const manejador = setTimeout(() => {
            setTextoValor(texto);
        }, delay);
        return () => {
            clearTimeout(manejador);
        };
    }, [texto, delay]);

    return textoValor;
};

export default ControlTyping;
