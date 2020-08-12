import React from 'react';
import MuiThemeProvider from '@material-ui/core/styles/MuiThemeProvider';
import theme from './theme';
import { TextField, Button } from '@material-ui/core';

function App() {
    return (
        <MuiThemeProvider theme={theme}>
            <h1>Proyecto en blanco</h1>
            <TextField variant="outlined" />
            <Button variant="contained" color="primary">Mi boton Material Design</Button>
        </MuiThemeProvider>
    );
}

export default App;
