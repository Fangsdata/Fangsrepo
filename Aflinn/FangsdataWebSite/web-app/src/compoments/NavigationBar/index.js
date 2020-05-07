import React from 'react';
import { NavLink } from 'react-router-dom';
import SearchBar from '../SearchBar';
// import NavLinks from '../NavLinks';
import NavLinks from '../NavLinks';
import Logo from './FangstDataLogo.svg';


const NavigationBar = () => (

  <nav className="navbar">
    <NavLink exact to="/">
      <img className="logo" src={Logo} alt="" />
    </NavLink>
    <SearchBar />
    <NavLinks />
    {/* <NavLinks  /> */}
  </nav>

);

export default NavigationBar;
