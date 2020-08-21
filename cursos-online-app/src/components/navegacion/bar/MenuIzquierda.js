import React, { Fragment } from 'react';
import { List, ListItem, ListItemText, Divider } from '@material-ui/core';
import { AccountBox as AccountBoxIcon, AddBox as AddBoxIcon, MenuBook as MenuBookIcon, PersonAdd as PersonAddIcon, People as PeopleIcon } from '@material-ui/icons';
import { Link } from 'react-router-dom';

const MenuIzquierda = ({ classes }) => {
    return (
        <Fragment>
            <List>
                <ListItem button component={Link} to='/auth/perfil'>
                    <AccountBoxIcon />

                    <ListItemText classes={{ primary: classes.listItemText }} primary='Perfil' />
                </ListItem>
            </List>
            <Divider />
            <List>
                <ListItem button component={Link} to='/curso/nuevo'>
                    <AddBoxIcon />

                    <ListItemText classes={{ primary: classes.listItemText }} primary='Nuevo Curso' />
                </ListItem>
                <ListItem button component={Link} to='/curso/lista'>
                    <MenuBookIcon />

                    <ListItemText classes={{ primary: classes.listItemText }} primary='Lista Cursos' />
                </ListItem>
            </List>
            <Divider />
            <List>
                <ListItem button component={Link} to='/instructor/nuevo'>
                    <PersonAddIcon />

                    <ListItemText classes={{ primary: classes.listItemText }} primary='Nuevo Instructor' />
                </ListItem>
                <ListItem button component={Link} to='/instructor/lista'>
                    <PeopleIcon />

                    <ListItemText classes={{ primary: classes.listItemText }} primary='Lista Instructor' />
                </ListItem>
            </List>
        </Fragment>
    );
};

export default MenuIzquierda;
