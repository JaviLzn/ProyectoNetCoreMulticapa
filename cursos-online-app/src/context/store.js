import React, { createContext, useContext, useReducer } from 'react';

export const StateContext = createContext();

export const StateProvider = ({reducer, initalState, children}) =>(
    <StateProvider value = {useReducer(reducer, initalState)}>
       {children} 
    </StateProvider>
);

export const useStateValue = () => useContext(StateContext);