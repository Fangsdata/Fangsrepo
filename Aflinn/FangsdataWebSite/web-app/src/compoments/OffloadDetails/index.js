import React,{useEffect, useState} from 'react';
import Map from '../Map';
import {Pie} from 'react-chartjs-2';
import './index.css';

const OffloadDetails = ({offloadId}) => 
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
    const [ offloadDetail, setOffloadDetails ] = useState(
        {
          /*   "id": "",
            "town": "",
            "state": "",
            "landingDate": "",
            "totalWeight": 0,
            "fish": [
                {
                    "id": 0,
                    "type": "",
                    "condition": "",
                    "preservation": "",
                    "packaging": "",
                    "quality": "",
                    "application": "",
                    "weight": 0
                }
            ],
            "boat": {
                "id": 0,
                "registration_id": "",
                "radioSignalId": "",
                "name": "",
                "state": "",
                "nationality": "",
                "length": 0,
                "fishingGear": "",
                "image": ""
                },
            "mapData": [
                {
                    "latitude": 0,
                    "longitude": 0,
                    "time": ""
                }
            ]
        */}
    ); 
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
        <div className="boat-container">
        { Object.keys(offloadDetail).length != 0 
        ? <>
                <div className="landing-info-container">
                    <h1>{offloadDetail.town.toLocaleLowerCase()} in {offloadDetail.state}</h1>
                    <p>Boat : {offloadDetail.boat.name} - {offloadDetail.boat.registration_id}</p> 
                    <p>Fishing gear : {offloadDetail.boat.fishingGear}</p>
                    <p>Landing date : {offloadDetail.landingDate}</p>
                    <p>Packaging : {offloadDetail.fish[0].packaging}</p>
                    <p>Preservation : {offloadDetail.fish[0].preservation}</p>
                </div>
                <div className="map-container">
                    <Map
                        lat={offloadDetail.mapData[0].latitude}
                        lng={offloadDetail.mapData[0].longitude}
                    />
                </div>
                <div className="fish-container">
                    <div className="fish-container-item"><p>Offload details</p></div>
                    <div className="fish-container-item">
                        <p>Type</p>
                        <p>Condition</p>
                        <p>Quality</p>
                        <p>Application</p>
                        <p>Weight</p>
                    </div>
                    {
                        offloadDetail.fish.map((fish) => (
                            
                            <div className="fish-container-item" key={fish.id}>
                                <p>{fish.type}</p>
                                <p>{fish.condition}</p>
                                <p>{fish.quality}</p>
                                <p>{fish.application}</p>
                                <p>{fish.weight} kg</p>
                            </div>
                        ))
                    }
                    <div className="fish-container-item">
                        <p>Total</p>
                        <p> - </p>
                        <p> - </p>
                        <p> - </p>
                        <p>{offloadDetail.totalWeight} kg</p>
                    </div>
                </div>
                <div className="pie-chart">
                    <Pie data={chartData} legend={{display:true}} redraw   width={300} height={400}></Pie>
                </div>
            </>
        : <div className="loader"></div>}
        </div>
    )
}
export default OffloadDetails;