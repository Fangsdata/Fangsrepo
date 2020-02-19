import React from 'react';

class SearchBar extends React.Component {
    state = {
        search : ""
    }
    render(){
        return(
            <div className="form-group">
                <input
                    placeholder="Search for boats"
                    value={this.state.search}
                    onInput= { e=>{ this.setState({search : e.target.value }) }}
                    type="text"
                    className="form-control"
                />
            </div>
        )
    }
};

export default SearchBar;