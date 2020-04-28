import React from 'react';
import Logo from './FangstDataLogo.svg'
import { Link } from 'react-router-dom';

const About = () => (
    <div className="about-container">
        <img className="about-logo" src={Logo} alt=""/>
        <div className="about-text-container">
            <h1 className="about-header">About this website</h1>
            <p><strong>FangstData</strong> is a website with the goal to provide you with various fishing data. </p>
            <p>We get our data from an open database but not everyone knows how to access that information and it can be hard to decipher.</p>
            <p>We want provide easy accsess to this information in a way that is understandandable to everyone.</p>
            <p>If you have any questions or comments, please don't hesitate to <Link exact to="/contact">contact us</Link>.
            </p>
        </div>
    </div>
);

export default About;