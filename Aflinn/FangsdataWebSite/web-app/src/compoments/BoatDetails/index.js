import React from 'react';
import VesselMap from '../Map';
import LandingsTable from '../LandingsTable';
import LandingsTableControlls from '../LandingsTableControlls';
import { normalizeCase } from '../../services/TextTools';
import boaticon from "./boat.png";
import { th } from 'date-fns/locale';


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
        },
        landings: [/*{
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0,
            id: ""
        }*/],
        pageNo: 1,
        resultCount: 5,
        boatDetailLoaded: false,
        boatOffloadLoaded: false,
        boatDetailError: false,
        boatOffloadError: false
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        const {boatname} = this.props;
        const {pageNo,resultCount} = this.state;
        fetch(`http://fangsdata-api.herokuapp.com/api/Boats/registration/` + boatname)
            .then((res) => res.json())
            .then((res) => this.setState({boat: res, boatDetailLoaded:true}))
            .catch(err => this.setState({boatDetailError: true}));

        fetch(`https://fangsdata-api.herokuapp.com/api/offloads/${boatname}/${resultCount}/${pageNo}`)
            .then((res2) => res2.json())
            .then((res2) => this.setState({landings: res2, boatOffloadLoaded: true }))
            .catch((err => this.setState({boatOffloadLoaded: true})));
    }

    async componentDidUpdate(prevProps, prevState) {
        const {pageNo,resultCount} = this.state;
        const {boatname} = this.props;
        if(pageNo != prevState.pageNo || resultCount != prevState.resultCount){
            this.setState({ boatOffloadLoaded: false, boatDetailError: false, boatOffloadError: false }); 
            this.setState({landings: []});
            fetch(`https://fangsdata-api.herokuapp.com/api/offloads/${boatname}/${resultCount}/${pageNo}`)
            .then((res2) => res2.json())
            .then((res2) => {
                this.setState({landings: res2, boatOffloadLoaded: true});
            });
        }
        if(boatname != prevProps.boatname){
            this.setState({ boatOffloadLoaded: false, boatDetailError: false, boatOffloadError: false }); 
            fetch(`http://fangsdata-api.herokuapp.com/api/Boats/registration/` + boatname)
            .then((res) => res.json())
            .then((res) => 
                this.setState({boat: res, boatDetailLoaded:true}));

        fetch(`https://fangsdata-api.herokuapp.com/api/offloads/${boatname}/${resultCount}/${pageNo}`)
            .then((res2) => res2.json())
            .then((res2) => {
                this.setState({landings: res2, boatOffloadLoaded: true});
            });
        }
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
            registrationId
        } = this.state.boat;
        const{landings,pageNo,resultCount,boatDetailLoaded, boatOffloadLoaded,boatDetailError, boatOffloadError} = this.state;
        
        let cleanMapData = mapData;
        if(mapData == undefined){
            cleanMapData = [];
        }
        if(mapData == null){
            cleanMapData = [];
 
        }
        return (    
        <div className="boat-container">
        {!boatDetailError
            ?<>{ boatDetailLoaded
                ?<><img src={boaticon} className="boat-img" alt="boat"></img>
                    <div className="boat-info">
                        <h3>{normalizeCase(name)}</h3>
                        <p className="boat-details">lengde: { length } m</p>
                        <p className="boat-details">Vekt: { weight }</p>
                        <p className="boat-details">År bygd: { builtYear }</p>
                        <p className="boat-details">Fylke: { state }</p>
                        <p className="boat-details">Kommune: { normalizeCase(town) }</p>
                        <p className="boat-details">Motor kraft: { enginePower } hp</p>
                        <p className="boat-details">Relskap: { fishingGear }</p>
                        <br></br>
                        { cleanMapData.length !== 0 
                            ?<p className="boat-details">Breddegrad / lengdegrad: <br></br>{mapData[0].latitude} / {mapData[0].longitude}</p>
                            :<></>
                        }
                        <br></br>

                    </div>
                </> 
                :<div className="loader-container">
                <div className="placeholder-item"></div>
                <div className="placeholder-info-container">
                    <div className="placeholder-item header"></div>
                    <div className="placeholder-item info"></div>
                    <div className="placeholder-item info"></div>
                    <div className="placeholder-item info"></div>
                    <div className="placeholder-item info"></div>
                </div>
                </div>}</>
            :<><p>here was an error</p></>
            }
            { cleanMapData.length !== 0 
                ?<div className="map-container">
                    <VesselMap lat={mapData[0].latitude} lng={mapData[0].longitude} />
                </div>
                :<></>
            }
            <LandingsTable 
                landings={landings}
                landingNo={(pageNo - 1) * resultCount}
                boatOffloadLoaded={boatOffloadLoaded}
                boatOffloadError={boatOffloadError}
                />
            <LandingsTableControlls 
                nextPage={()=>{
                    let page = this.state.pageNo;
                    page = page + 1;
                    this.setState({pageNo: page});
                }}
                prevPage={()=>{
                    let page = this.state.pageNo;
                    if(page > 1){
                        page = page - 1;
                        this.setState({pageNo: page});
                    } 
                }}
                resultNo={(no)=>{
                    let page = this.state.pageNo;
                    page = 1;
                    this.setState({resultCount: no, pageNo: page})
                }}
                page={this.state.pageNo}></LandingsTableControlls>
        </div>
        );
    };
}

export default BoatDetails;
