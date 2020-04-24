import React from 'react';
import SearchBar from '../SearchBar';
import NavLinks from '../NavLinks';
import { NavLink } from 'react-router-dom';
import Logo from './FangstDataLogo.svg'


const NavigationBar = () => (

    <nav className="navbar">
        <NavLink exact to="/">
            <img className="logo" src={Logo} alt=""/>
        </NavLink>
        <SearchBar />
        <NavLinks  />
    </nav>

);

export default NavigationBar