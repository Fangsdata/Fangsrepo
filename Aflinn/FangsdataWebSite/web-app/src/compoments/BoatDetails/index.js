import React from 'react';
import VesselMap from '../Map';
import LandingsTable from '../LandingsTable';
import LandingsTableControlls from '../LandingsTableControlls';
import { connect } from 'react-redux';
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
        resultCount: 5
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        const {boatname, BoatStore} = this.props;
        const {pageNo,resultCount} = this.state;
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
        fetch(`https://fangsdata-api.herokuapp.com/api/offloads/${boatname}/${resultCount}/${pageNo}`)
            .then((res2) => res2.json())
            .then((res2) => {
                this.setState({landings: res2});
            });
    }

    async componentDidUpdate(prevProps, prevState) {
        // Typical usage (don't forget to compare props):
        const {pageNo,resultCount} = this.state;
        const {boatname} = this.props;
        
        if(pageNo != prevState.pageNo || resultCount != prevState.resultCount){
            this.setState({landings: []});
            fetch(`https://fangsdata-api.herokuapp.com/api/offloads/${boatname}/${resultCount}/${pageNo}`)
            .then((res2) => res2.json())
            .then((res2) => {
                this.setState({landings: res2});
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
            radioSignalId
        } = this.state.boat;
        const{landings,pageNo,resultCount} = this.state;
        let mapDataFUCKYOUJAVASCRIPT = mapData;
        if(mapData == undefined){
            mapDataFUCKYOUJAVASCRIPT = [];
        }
        if(mapData == null){
            mapDataFUCKYOUJAVASCRIPT = [];
 
        }
        return (    
        <div className="boat-container">
            {radioSignalId !== ""
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
                    { mapDataFUCKYOUJAVASCRIPT.length !== 0 
                        ?<p className="boat-details">Breddegrad / lengdegrad: <br></br>{mapData[0].latitude} / {mapData[0].longitude}</p>
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
            <LandingsTable landings={landings} landingNo={(pageNo - 1) * resultCount}></LandingsTable>
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

const mapStateToProp = reduxStoreState => {
    return {BoatStore: reduxStoreState};
};
export default connect(mapStateToProp)(BoatDetails);
