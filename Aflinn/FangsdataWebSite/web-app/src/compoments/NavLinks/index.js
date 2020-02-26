import React from 'react';
import { NavLink } from 'react-router-dom';

const NavLinks = () => (
    <ul>
        <li>
            <NavLink
            exact
            to="/">Home</NavLink>
        </li>
        <li>
        <NavLink
            exact
            to="/boats">Boat</NavLink>
        </li>
    </ul>
);

export default NavLinks;