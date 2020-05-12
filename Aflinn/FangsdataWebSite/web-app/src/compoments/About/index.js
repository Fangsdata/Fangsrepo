import React from 'react';
import { Link } from 'react-router-dom';
import Logo from './FangstDataLogo.svg';
import DataIllustration from './data.svg';

const About = () => (
  <div className="about-container">
    <img className="about-logo" src={Logo} alt="" />
    <div className="about-text-container">
      <h1 className="about-header">About this website</h1>
      <div className="about-content">
        <div className="about-text">
          <p>
            <strong>FangstData</strong>
            {' '}
            is a website with the goal to provide you with various fishing data.
            {' '}
          </p>
          <p>
            We get our data from an open database, not everyone knows
            how to access that information and it can be hard to decipher.
          </p>
          <p>
            We want provide easy accsess to this information
            in a way that is understandandable to everyone.
          </p>
          <p>
            If you have any questions or comments, please don't hesitate to
            <Link to="/contact"> contact us</Link>
            .
          </p>
        </div>
        <img className="data-illustration" src={DataIllustration} alt="" />
      </div>
    </div>
  </div>
);

export default About;
