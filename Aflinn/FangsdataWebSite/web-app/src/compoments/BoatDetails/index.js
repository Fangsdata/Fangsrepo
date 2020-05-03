import React from 'react';
import VesselMap from '../Map'
import LandingsTable from '../LandingsTable'
import {getBoats} from '../../services/OffloadService';

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
            mapData: [/*{
                latitude: 0,
                longitude: 0
            }*/]
        }
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        const {boatname} = this.props;
    //    const { match: { params } } = this.props;
        fetch(`http://fangsdata-api.herokuapp.com/api/Boats/` + boatname)
            .then((res) => res.json())
            .then((res) => {
                this.setState({boat: res})
                // console.log(res)
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
            // state,
            town,
            length,
            // weight,
            builtYear,
            // enginePower,
            fishingGear,
            image,
            mapData
        } = this.state.boat;
        return (    
        <div className="boat-container">
            <img src={image} className="boat-img" alt="boat">{console.log(this.state)}</img>
            <div className="boat-info">
                <h3>{this.Capitalize(name)}</h3>
                <p className="boat-details">Length: { length } m</p>
                {/* <p className="boat-details">Weight: { weight }</p> */}
                <p className="boat-details">Year Built: { builtYear }</p>
                {/* <p className="boat-details">State: { state }</p> */}
                <p className="boat-details">Town: { town }</p>
                {/* <p className="boat-details">Engine size: { enginePower }</p> */}
                <p className="boat-details">Fishing gear: { fishingGear }</p>
                <br></br>
                { mapData != undefined && mapData.length != 0
                ?<p className="boat-details">Latitude / Longitude: <br></br>{mapData[0].latitude} / {mapData[0].longitude}</p>
                :<></>
                }
                
            </div>
            { mapData != undefined && mapData.length != 0
            ?<VesselMap lat={mapData[0].latitude} lng={mapData[0].longitude} />
            :<></>
            }
            <LandingsTable boatname={this.props}></LandingsTable>
        </div>

        );
    };
}
export default BoatDetails;