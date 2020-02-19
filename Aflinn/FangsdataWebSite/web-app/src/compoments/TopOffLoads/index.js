import React from 'react';
import {getOffloads} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';
import FilterContainer from '../FiltersContainer';

class TopOffLoads extends React.Component {

    async componentDidMount(){
        this.setState({
            offLoads : await getOffloads(),
        });
    }

    state = {
        offLoads: [],
        filter: ""
    }

    async filterList(filter){
        this.setState({ offloads : await getOffloads({boatFishingGear : filter })});
    }

    render(){
        return (
        <div>
            <h1>Offloads</h1>
            <FilterContainer onChangeFishingGear={ e => this.filterList(e.target.id)}/>
            <OffloadsList 
                offloads={ this.state.offLoads }
            />
        </div>);
    }
}

export default TopOffLoads;