import React from 'react';
import { Link } from 'react-router-dom';
import NotFoundImg from "./404.png";

class NotFound extends React.Component{
    render(){
        return (
        <div className="not-found-container">
            <img src={NotFoundImg} className="not-found-img" alt="404"></img>
            <h1 className="not-found-header">Page not found!</h1>
            <Link to="/"><div className="not-found-btn">Go back to home</div></Link>
          </div>
        );
    }
}export default NotFound;