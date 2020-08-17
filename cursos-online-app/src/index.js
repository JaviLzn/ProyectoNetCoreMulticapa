import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { initialState } from './context/initialState';
import { StateProvider } from './context/store';
import { mainReducer } from './context/reducers';

ReactDOM.render(
    <React.StrictMode>
        <StateProvider initialState={initialState} reducer={mainReducer}>
            <App />
        </StateProvider>
    </React.StrictMode>,
    document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
