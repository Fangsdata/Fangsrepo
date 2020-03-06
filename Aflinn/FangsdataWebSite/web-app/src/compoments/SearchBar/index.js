import React from 'react';
import icon from "./search-24px.svg";
class SearchBar extends React.Component {
    state = {
        search : ""
    }
    render(){
        return(
            <div className="searchbar">
                <input className="search-inp"
                    placeholder="Search for boats"
                    value={this.state.search}
                    onInput= { e=>{ this.setState({search : e.target.value }) }}
                    onChange={()=>{}}
                    type="text"
                />
                <button className="search-btn"><img className="search-icon" src={icon} alt=""/></button>

            </div>
        )
    }
};

export default SearchBar;