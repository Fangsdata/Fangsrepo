import React from 'react';
import { NavLink } from 'react-router-dom';

class NavLinks extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isSidebarOpen: false,
    };
  }

  handleMenuButtonClick() {
    const { isSidebarOpen } = this.state;
    this.setState({ isSidebarOpen: !isSidebarOpen });
  }

  render() {
    const { isSidebarOpen } = this.state;
    return (
      <div className="navlink-container">
        <div
          className={`burger-container ${isSidebarOpen ? 'change' : ''}`}
          onClick={() => this.handleMenuButtonClick()}
          onKeyDown={this.handleKeyDown}
        >
          <div className="line1" />
          <div className="line2" />
          <div className="line3" />
        </div>

        <nav className={`nav-${isSidebarOpen ? 'show' : 'hide'}`}>
          <div className="navlinks">
            <NavLink
              exact
              to="/"
            >
              Home
            </NavLink>
            {/* <NavLink
                        exact
                        to="/boats">Boat</NavLink> */}
            <NavLink
              exact
              to="/contact"
            >
              Contact us
            </NavLink>
            <NavLink
              exact
              to="/about"
            >
              About us
            </NavLink>
          </div>
        </nav>

        {/* <ul className="navlinks">
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
            </ul> */}
      </div>
    );
  }
}

export default NavLinks;
