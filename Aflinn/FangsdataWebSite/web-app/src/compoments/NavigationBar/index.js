import React from 'react';
import { NavLink } from 'react-router-dom';
import SearchBar from '../SearchBar';
import NavLinks from '../NavLinks';
import Logo from './FangstDataLogo.svg';


class NavigationBar extends React.Component {
  constructor() {
    super();
    this.state = {
      classItem: 'logo',
    };
  }

  componentDidMount() {
    window.addEventListener('scroll', this.getWindowHeight());
  }

  componentWillUnmount() {
    window.removeEventListener('scroll', this.getWindowHeight());
  }

  getWindowHeight() {
    const distanceY = window.pageYOffset
    || document.documentElement.scrollTop;
    const shrinkOn = 50;
    if (distanceY > shrinkOn) {
      this.setState({
        classItem: 'small-logo',
      });
    } else {
      this.setState({
        classItem: 'logo',
      });
    }
  }

  render() {
    const { classItem } = this.state;
    return (
      <>
        <NavLink exact to="/">
          <img className={classItem} src={Logo} alt="" />
        </NavLink>
        <nav className="navbar">
          <div className="logo-placeholder" />
          <SearchBar />
          <NavLinks />
        </nav>
      </>
    );
  }
}

export default NavigationBar;
