import React from 'react';
import { NavLink } from 'react-router-dom';
import SearchBar from '../SearchBar';
import NavLinks from '../NavLinks';
import Logo from './FangstDataLogo.svg';


class NavigationBar extends React.Component {

  constructor() {
    super();
    this.state = {
      class: "logo"
    }
  }

  componentDidMount() {
     window.addEventListener('scroll', this.getWindowHeight);
    }

 componentWillUnmount(){
     window.removeEventListener('scroll', this.getWindowHeight);
   }

 getWindowHeight = () => {

  const distanceY = window.pageYOffset ||
    document.documentElement.scrollTop
  const shrinkOn = 50;
  if (distanceY > shrinkOn) {
    this.setState({
      class: "small-logo"
    })
  }
  else {
    this.setState({
      class: "logo"
    })
  }
}

  render() {
    return (
      <>
      <NavLink exact to="/">
        <img className={this.state.class} src={Logo} alt="" />
      </NavLink>
      <nav className="navbar">
        <div className="logo-placeholder"></div>
    <SearchBar />
    <NavLinks />
  </nav>
  </>
    );
  }
}

export default NavigationBar;
