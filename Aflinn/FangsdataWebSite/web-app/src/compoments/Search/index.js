import React from 'react';
import { useLocation } from 'react-router-dom';
import { withRouter } from 'react-router-dom';
import { searchTest } from '../../services/OffloadService';
import SearchItem from '../SearchItem';
import { OFFLOADAPI } from '../../Constants';

class Search extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
          searchTerm: '',
          boats: [],
          searchLoaded: false,
          searchError: false,
        };
      }

      async componentDidMount() {
        this.state.searchTerm = window.location.pathname.substr(8);
        console.log(this.state.searchTerm);
        this.setState({ boats: await searchTest(this.state.searchTerm), searchLoaded: true });
      };

    async componentDidUpdate() {
        this.state.searchTerm = window.location.pathname.substr(8);
        // console.log(this.state.searchTerm)
        this.setState({ boats: await searchTest(this.state.searchTerm), searchLoaded: true });
      };

    async getResults() {
        const searchTerm = this.props.searchterm;
        console.log('load')
        if (searchTerm.length > 0) {
          fetch(`${OFFLOADAPI}/search/boats/${searchTerm}`)
            .then((res) => res.json())
            .then((res) => {
                this.state.boats = res;
            });
        } else {
            this.state.boats = [];
        }
    }

    render () {
        // const location = useLocation();
        // const results = location.state.foundBoats;
        const location = this.props.location
        // const term = this.props.location.pathname
        // console.log(this.props.location.state.searchTerm)
        const {
            searchLoaded, searchError, boats, searchTerm
          } = this.state;
       return (

        <div className="offload-table search">
                <div className="offload-header">Results for {searchTerm}</div>
                <SearchItem
                item={{
                    name: 'Navn',
                    fishingGear: 'Relskap',
                    length: 'Båt lengde',
                    radioSignalId: 'Radio signal',
                }}
                index="#"
                />
        {!searchError
            ? (
              <>
                { searchLoaded
                  ? (
                     boats.map((item, index) => (
                        <SearchItem
                            key={item.boatRegistrationId}
                            item={item}
                            index={index + 1}
                        />
                        ))
                  )
                  : <div className="loader">Loading...</div>}
              </>
            )
            : <><p>error Loading toplist</p></>}
            </div>
        ); 
    }
    
}


export default withRouter(Search);
