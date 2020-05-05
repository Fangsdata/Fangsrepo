import React from 'react';
import VesselMap from '../Map'
import LandingsTable from '../LandingsTable'
import {getBoats} from '../../services/OffloadService';
import { connect } from 'react-redux';
import { normalizeCase } from '../../services/TextTools';


class BoatDetails extends React.Component{
    state = {
        boat: {
            id: "",
            registrationId: "",
            radioSignalId: "",
            name: "",
            state: "",
            town: "",
            length: "",
            weight: "",
            builtYear: "",
            enginePower: "",
            fishingGear: "",
            image: "",
            mapData: []
        }
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        const {boatname, BoatStore} = this.props;
        if(Object.keys(BoatStore).length !== 0)
        {
            console.log(BoatStore);
            BoatStore.mapData = [];
            this.setState({boat: BoatStore});
        }
        fetch(`http://fangsdata-api.herokuapp.com/api/Boats/` + boatname)
            .then((res) => res.json())
            .then((res) => {
                this.setState({boat: res});
            });
    }

    Capitalize(str){
        return str.replace(/\w\S*/g, function(txt){
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        });
    }
    
   render() {
        const { 
            name,
            state,
            town,
            length,
            weight,
            builtYear,
            enginePower,
            fishingGear,
            image,
            mapData,
            radioSignalId
        } = this.state.boat;
        let mapDataFUCKYOUJAVASCRIPT = mapData;
        if(mapData == undefined){
            mapDataFUCKYOUJAVASCRIPT = [];
        }
        if(mapData == null){
            mapDataFUCKYOUJAVASCRIPT = [];
 
        }
        console.log(mapData);
        return (    
        <div className="boat-container">
            {radioSignalId !== ""
            ?<><img src={image} className="boat-img" alt="boat"></img>
                <div className="boat-info">
                    <h3>{this.Capitalize(name)}</h3>
                    <p className="boat-details">Length: { length } m</p>
                    <p className="boat-details">Weight: { weight }</p>
                    <p className="boat-details">Year Built: { builtYear }</p>
                    <p className="boat-details">State: { state }</p>
                    <p className="boat-details">Town: { normalizeCase(town) }</p>
                    <p className="boat-details">Engine size: { enginePower } hp</p>
                    <p className="boat-details">Fishing gear: { fishingGear }</p>
                    <br></br>
                    { mapDataFUCKYOUJAVASCRIPT.length !== 0 
                        ?<p className="boat-details">Latitude / Longitude: <br></br>{mapData[0].latitude} / {mapData[0].longitude}</p>
                        :<></>
                    }
                    <br></br>

                </div>
            </> 
            :<div className="loader-container"><div className="loader"></div></div>
            }
            { mapDataFUCKYOUJAVASCRIPT.length !== 0 
                ?<VesselMap lat={mapData[0].latitude} lng={mapData[0].longitude} />
                :<></>
            }
            <LandingsTable boatname={this.props}></LandingsTable>
        </div>

        );
    };
}

const mapStateToProp = reduxStoreState => {
    return {BoatStore: reduxStoreState};
};
export default connect(mapStateToProp)(BoatDetails);
