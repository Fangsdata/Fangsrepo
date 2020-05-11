import React from 'react';
import { useLocation } from 'react-router-dom';
import SearchItem from '../SearchItem';
import { OFFLOADAPI } from '../../Constants';

export class Search extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
          searchTerm: '',
          boats: [],
          searchLoaded: false,
          searchError: false,
        };
      }

    componentDidMount() {
        const searchTerm = this.props.searchterm;
        if (searchTerm.length > 2) {
          fetch(`${OFFLOADAPI}/search/boats/${searchTerm}`)
            .then((res) => res.json())
            .then((res) => {
                this.state.boats = res;
            });
        } else {
            this.state.boats = [];
        }
      };

    render () {
        // const location = useLocation();
        // const results = location.state.foundBoats;
        // const location = this.props.location
        // const term = this.props.location.pathname
        const {
            searchLoaded, searchError,
          } = this.state;
       return (

        <div className="offload-table search">
                <div className="offload-header">Search</div>
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
                     this.state.boats.map((item, index) => (
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

export default Search;
