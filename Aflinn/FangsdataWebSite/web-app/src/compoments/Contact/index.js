import React from 'react';
import Icon from './email.svg'

const Contact = () => (
    <div className="contact-container">
        <div className="contact-info">
            <h1 className="contact-header">Contact us</h1>
            <p>If you have any questions or comments, please send us a line.</p>
            <a className="mail" href="mailto:mail@example.com">Send email</a>
        </div>
        <img className="email-icon" src={Icon} alt=""/>
    </div>
);

export default Contact;