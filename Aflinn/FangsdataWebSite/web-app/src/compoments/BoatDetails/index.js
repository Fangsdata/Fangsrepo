import React from 'react';
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
            mapData: []
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
                console.log(res)
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
            mapData
        } = this.state.boat;
        console.log(this.state);
        const image = "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg"
        return (    
        <div className="boat-container">
            <img src={image} className="boat-img"></img>
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
            <div className="map-container">
                <iframe className="map"
                src="https://www.openstreetmap.org/export/embed.html?bbox=-27.531738281250004%2C63.68281284508611%2C-20.5389404296875%2C65.28428227574943&amp;layer=mapnik&amp;marker=64.49489437374156%2C-24.035146236419678">
                </iframe><br/>
                <p id="map-link"><a href="https://www.openstreetmap.org/?mlat=64.495&amp;mlon=-24.035#map=8/64.495/-24.035">
                View Larger Map</a></p>
            </div>
        </div>

        );
    };
}
export default BoatDetails;