import React from 'react';
import {getOffloads} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';
import FilterContainer from '../FiltersContainer';
import { ta } from 'date-fns/locale';
import { normalizeMonth } from '../../services/TextTools'

class FrontPageTable extends React.Component {

    render(){
        return (
        <div>
        
        {!topOfflodError
                ?<>{ topOffloadsLoaded
                        ?<OffloadsList 
                            offloads={ offLoads }
                            title="Blabla"/>
                        :<div className="loader">Loading...</div>
                }</>
                :<><p>error Loading toplist</p></>
            }

        </div>);
    }
}

export default FrontPageTable;