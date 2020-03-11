import React from 'react';
import VesselMap from '../Map'

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
            mapData: "",
        }
    };

    componentDidMount() {

        fetch(`https://localhost:5000/api/boats/mkv`)
            .then((res) => res.json())
            .then((res) => this.setState({boat: res}));
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
            mapData
        } = this.state.boat;
        const image = "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg"
        return (    
        <div className="boat-container">
            <img src={image} className="boat-img" alt="boat"></img>
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
                <p className="boat-details">Latitude / Longitude: <br></br>64.49491° / -24.0298°{ mapData }</p>
            </div>
            <VesselMap lat={64.49491} lng={-24.0298} />
        </div>

        );
    };
}
export default BoatDetails;