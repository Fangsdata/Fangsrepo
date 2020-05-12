import React, { useState } from 'react';
import { useLocation } from 'react-router-dom';
import SearchItem from '../SearchItem';

const Search = () => {
    const location = useLocation();
    const results = location.state.boats;
    const [searchLoaded, setSearchLoaded] = useState(false);
    const [searchError, setSearchError] = useState(false);
    console.log(results)
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
                            { results.length > 0
                            ? (
                                results.map((item, index) => (
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

export default Search;
