import React from 'react';
import {getOffloads} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';

class TopOffLoads extends React.Component {

    componentDidMount(){
        this.setState({
            offLoads : getOffloads(),
        });
    }

    state = {
        offLoads: []
    }

    render(){
        return (
        <div>
            <h1>Offloads</h1>
            <OffloadsList 
                offloads={ this.state.offLoads }
            />
        </div>);
    }
}

export default TopOffLoads;