import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

// * Si existe el token en el local storage lo asigna al request
axios.interceptors.request.use(
    (config) => {
        const token_seguridad = window.localStorage.getItem('token_seguridad');
        if (token_seguridad) {
            config.headers.Authorization = `Bearer ${token_seguridad}`;
            return config;
        }
    },
    (error) => Promise.reject(error)
);

const requestGeneric = {
    get: (url) => axios.get(url),
    post: (url, body) => axios.post(url, body),
    put: (url, body) => axios.put(url, body),
    delete: (url) => axios.delete(url),
};

export default requestGeneric;
