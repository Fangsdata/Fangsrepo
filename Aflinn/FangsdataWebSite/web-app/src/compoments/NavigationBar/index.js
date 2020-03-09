import React from 'react';
import SearchBar from '../SearchBar';
import NavLinks from '../NavLinks';


const NavigationBar = () => (

    <nav className="navbar navbar-light bg-light">
        <SearchBar />
        <NavLinks />
    </nav>

);

export default NavigationBar