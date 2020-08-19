import React from 'react';
import { List, ListItem, ListItemText, ListItemIcon, Divider } from '@material-ui/core';
import { AccountBox as AccountBoxIcon, AddBox as AddBoxIcon, MenuBook as MenuBookIcon, PersonAdd as PersonAddIcon, People as PeopleIcon } from '@material-ui/icons';
import { Link } from 'react-router-dom';

const MenuIzquierda = ({ classes }) => {
    return (
        <div className={classes.list}>
            <List>
                <ListItem button component={Link} to='/auth/perfil'>
                    <ListItemIcon>
                        <AccountBoxIcon />
                    </ListItemIcon>
                    <ListItemText classes={{ primary: classes.listItemText }} primary='Perfil' />
                </ListItem>
            </List>
            <Divider />
            <List>
                <ListItem buttom component={Link} to='/curso/nuevo'>
                    <ListItemIcon>
                        <AddBoxIcon />
                    </ListItemIcon>
                    <ListItemText classes={{ primary: classes.listItemText }} primary='Nuevo Curso' />
                </ListItem>
                <ListItem button component={Link} to='/curso/lista'>
                    <ListItemIcon>
                        <MenuBookIcon />
                    </ListItemIcon>
                    <ListItemText classes={{ primary: classes.listItemText }} primary='Lista Cursos' />
                </ListItem>
            </List>
            <Divider />
            <List>
                <ListItem button component={Link} to='/instructor/nuevo'>
                    <ListItemIcon>
                        <PersonAddIcon />
                    </ListItemIcon>
                    <ListItemText classes={{ primary: classes.listItemText }} primary='Nuevo Instructor' />
                </ListItem>
                <ListItem button component={Link} to='/instructor/lista'>
                    <ListItemIcon>
                        <PeopleIcon />
                    </ListItemIcon>
                    <ListItemText classes={{ primary: classes.listItemText }} primary='Lista Instructor' />
                </ListItem>
            </List>
        </div>
    );
};

export default MenuIzquierda;
