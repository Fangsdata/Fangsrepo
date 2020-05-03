import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter as Router } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.css';
import './index.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import './Styles/base.css';
import './Styles/navbar.css';
import './Styles/boats.css';
import './Styles/offloads.css';
import './Styles/filter.css';
import './Styles/hamburger.css';
import { Provider } from 'react-redux';
import { createStore } from 'redux';
import boatReducer from './reducers/BoatReducer';
import './Styles/landings.css';

ReactDOM.render(
    <Provider
        store={createStore(boatReducer)}>
        <Router>
            <App />
        </Router>
    </Provider>,
    document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
