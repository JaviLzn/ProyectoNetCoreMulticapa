import React, { Fragment } from 'react';
import { List, ListItem, Avatar, ListItemText } from '@material-ui/core';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';

const MenuDerecha = ({ classes, salirSesion, usuario }) => (
    <Fragment>
        <List>
            <ListItem button>
                <Avatar src={usuario ? usuario.foto : ''} />
                <ListItemText classes={{ primary: classes.listItemText }} primary={usuario ? usuario.NombreCompleto : ''} />
            </ListItem>
            <ListItem button onClick={salirSesion}>
                <ExitToAppIcon />
                <ListItemText classes={{ primary: classes.listItemText }} primary='Salir' />
            </ListItem>
        </List>
    </Fragment>
);

export default MenuDerecha;
