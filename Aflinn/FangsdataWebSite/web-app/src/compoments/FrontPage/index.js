import React from 'react';
import { Link } from 'react-router-dom';
import {getOffloadsTest} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';

// https://fangsdata-api.herokuapp.com/api/offloads?fishingGear=Garn&Count=5

class FrontPage extends React.Component {
    state = {
        offLoads: []
    }

    async componentDidMount(){
        this.setState({ offLoads : await getOffloadsTest(), topOffloadsLoaded:true});
    }

    render(){
        const {offLoads} = this.state;
        console.log(offLoads)
        return (
        <div className="front-page">
            <div className="front-list-container">
            <OffloadsList
                offloads={ offLoads }
                title="Top List 1"/>
             <Link to="/"><div className="more-btn">More</div></Link>
            </div>
            <div className="front-list-container">
            <OffloadsList 
                offloads={ offLoads }
                title="Top List 2"/>
                <Link to="/"><div className="more-btn">More</div></Link>
            </div>
            <div className="front-list-container">
            <OffloadsList 
                offloads={ offLoads }
                title="Top List 3"/>
                <Link to="/"><div className="more-btn">More</div></Link>
            </div>
            <div className="front-list-container">
            <OffloadsList 
                offloads={ offLoads }
                title="Top List 4"/>
                <Link to="/"><div className="more-btn">More</div></Link>
            </div>

        </div>
        );
    }
}

export default FrontPage;