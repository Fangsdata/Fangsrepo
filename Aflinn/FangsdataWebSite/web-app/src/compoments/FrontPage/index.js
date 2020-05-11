import React from 'react';
import { Link } from 'react-router-dom';
import {getOffloadsTest} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';

// https://fangsdata-api.herokuapp.com/api/offloads?fishingGear=Garn&Count=5

class FrontPage extends React.Component {
    state = {
        offLoads: [],
        tableLoaded: false,
        tableError: false,
    }

    async componentDidMount(){
        this.setState({ offLoads : await getOffloadsTest(), tableLoaded:true});
    }

    render(){
        const {
            offLoads,
            tableLoaded,
            tableError
        } = this.state;
        return (
        <div className="front-page">
            
            {!tableError
                ? (
                  <>
                    { tableLoaded
                      ? (
                        <>
                        <div className="front-list-container">
                        <OffloadsList
                            offloads={ offLoads }
                            title="Top List 1"/>
                         <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                        </div>
                        </>
                      )
                      : (
                        <div className="front-loading-container">
                            <div className="offload-header" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item more" />
                        </div>
                      )}
                  </>
                )
                : <><p>here was an error</p></>}
                {!tableError
                ? (
                  <>
                    { tableLoaded
                      ? (
                        <>
                        <div className="front-list-container">
                        <OffloadsList
                            offloads={ offLoads }
                            title="Top List 1"/>
                         <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                        </div>
                        </>
                      )
                      : (
                        <div className="front-loading-container">
                            <div className="offload-header" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item more" />
                        </div>
                      )}
                  </>
                )
                : <><p>here was an error</p></>}
                {!tableError
                ? (
                  <>
                    { tableLoaded
                      ? (
                        <>
                        <div className="front-list-container">
                        <OffloadsList
                            offloads={ offLoads }
                            title="Top List 1"/>
                         <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                        </div>
                        </>
                      )
                      : (
                        <div className="front-loading-container">
                            <div className="offload-header" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item more" />
                        </div>
                      )}
                  </>
                )
                : <><p>here was an error</p></>}
                {!tableError
                ? (
                  <>
                    { tableLoaded
                      ? (
                        <>
                        <div className="front-list-container">
                        <OffloadsList
                            offloads={ offLoads }
                            title="Top List 1"/>
                         <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                        </div>
                        </>
                      )
                      : (
                        <div className="front-loading-container">
                            <div className="offload-header" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item" />
                            <div className="placeholder-item more" />
                        </div>
                      )}
                  </>
                )
                : <><p>here was an error</p></>}

        </div>
        );
    }
}

export default FrontPage;