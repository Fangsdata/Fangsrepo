import React,{useEffect, useState} from 'react';
import Map from '../Map';
import {Pie} from 'react-chartjs-2';
import './index.css';
import { connect } from 'react-redux';

const OffloadDetails = ({offloadId, ReduxOffload}) => 
{
    const [chartData, setChartData] = useState(
          {
            labels: ["NON"],
            datasets: [{
            label: "My First dataset",
            backgroundColor: ['#2B59C3' ],
            data: [1],
            }]
        }
    )
    const [ offloadDetail, setOffloadDetails ] = useState({}); 
    const generateColors = (size) => {
        let colorExample = [ '#2B59C3','#B7DFB3', '#DCB8B8', '#DCD8B8' ];
        let retData = [];
        for (let i = 0; i < size; i++) {
            retData.push( colorExample[i % colorExample.length] );
        }
        return retData;
    }
    const CreatePieChartDataset = (data) => {
        
        let pieData = {
            labels: data.map((data)=> data.label),
            datasets:[{
                label: "fish dataset",
                backgroundColor: generateColors(data.length),
                data: data.map((data)=>data.value)
            }]
        }
        setChartData(pieData); 
    }

    useEffect(()=>{
        fetch(`https://fangsdata-api.herokuapp.com/api/offloads/details/${offloadId}`)
            .then((res) => res.json())
            .then((json) => {
                setOffloadDetails(json);
                var data = json.fish.map((item)=>{
                    let rObj = {    label: item.type, 
                                    value: item.weight };
                    return rObj;
                });
                CreatePieChartDataset(data);
            });
    },[]);

    return (
        <div className="boat-container landing">
        { Object.keys(offloadDetail).length != 0 
        ? <>
                <div className="landing-info-container">
                    <h1>{/*landingTown.toLocaleLowerCase()} in {landingState*/}</h1>
                    <p>Boat : {ReduxOffload.name}</p> 
                    <p>Fishing gear : {ReduxOffload.fishingGear}</p>
                    <p>Landing date : {offloadDetail.landingDate}</p>
                   { <p>Packaging : {offloadDetail.fish[0].packaging}</p>
                    <p>Preservation : {offloadDetail.fish[0].preservation}</p> }
                </div>
                <div className="map-container">
                    <Map
                        lat={offloadDetail.mapData[0].latitude}
                        lng={offloadDetail.mapData[0].longitude}
                    />
                </div>
                <table className="landing-table detail">
                    <tr>
                        <th className="landing-table-header" colSpan="5">Offload details</th>
                    </tr>
                    <tr>
                        <td>Type</td>
                        <td>Condition</td>
                        <td>Quality</td>
                        <td>Application</td>
                        <td>Weight</td>
                    </tr>
                    {
                        offloadDetail.fish.map((fish) => (
                            
                            <tr key={fish.id}>
                                <td>{fish.type}</td>
                                <td>{fish.condition}</td>
                                <td>{fish.quality}</td>
                                <td>{fish.application}</td>
                                <td>{fish.weight} kg</td>
                            </tr>
                        ))
                    }
                    <tr>
                        <td>Total</td>
                        <td> - </td>
                        <td> - </td>
                        <td> - </td>
                        <td>{offloadDetail.totalWeight} kg</td>
                    </tr>
                </table>
                <div className="pie-chart">
                    <Pie data={chartData} legend={{display:true}} redraw   width={200} height={300}></Pie>
                </div>
            </>
        : <div className="loader"></div>}
        </div>
    )
}
const mapStateToProp = reduxStoreState => {
    return {ReduxOffload: reduxStoreState};
};
export default connect(mapStateToProp)(OffloadDetails);