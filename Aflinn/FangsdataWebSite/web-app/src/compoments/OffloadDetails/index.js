import React, { useEffect, useState } from 'react';
import { Redirect } from 'react-router-dom';
import { Pie } from 'react-chartjs-2';
import Map from '../Map';
import { normalizeCase, normalizeWeight, normalizeDate } from '../../services/TextTools';


const OffloadDetails = ({ offloadId }) => {
  const [chartData, setChartData] = useState(
    {
      labels: ['NON'],
      datasets: [{
        label: 'My First dataset',
        backgroundColor: ['#2B59C3'],
        data: [1],
      }],
    },
  );
  const [offloadLoading, setOffloadLoad] = useState(false);
  const [offloadError, setOffloadError] = useState(false);
  const [offloadDetail, setOffloadDetails] = useState({/*
        "id": "",
        "town": "",
        "state": "",
        "landingDate": "",
        "totalWeight": 0.0,
        "fish": [],
        "boat": {
            "id": -1,
            "registration_id": "",
            "radioSignalId": "",
            "name": "",
            "state": "",
            "nationality": "",
            "town": "",
            "length": 0,
            "fishingGear": "",
            "image": ""
            },
         "mapData": [{
                "latitude": 0.0,
                "longitude": 0.0
            }] */
  });

  const generateColors = (size) => {
    const colorExample = ['#2B59C3', '#B7DFB3', '#DCB8B8', '#DCD8B8'];
    const retData = [];
    for (let i = 0; i < size; i++) {
      retData.push(colorExample[i % colorExample.length]);
    }
    return retData;
  };
  const CreatePieChartDataset = (data) => {
    const pieData = {
      labels: data.map((data) => data.label),
      datasets: [{
        label: 'fish dataset',
        backgroundColor: generateColors(data.length),
        data: data.map((data) => data.value),
      }],
    };
    setChartData(pieData);
  };

  useEffect(() => {
    setOffloadLoad(false);
    setOffloadLoad(false);
    fetch(`https://fangsdata-api.herokuapp.com/api/offloads/details/${offloadId}`)
      .then((res) => res.json())
      .then((json) => {
        setOffloadDetails(json);
        const data = json.fish.map((item) => {
          const rObj = {
            label: item.type,
            value: item.weight,
          };
          return rObj;
        });
        CreatePieChartDataset(data);
        setOffloadLoad(true);
      })
      .catch(() => {
        setOffloadError(true);
      });
  }, []);

  return (
    <div className="boat-container landing">
      { !offloadError
        ? (
          <>
            {' '}
            { offloadLoading
              ? (
                <>
                  <div className="landing-info-container">
                    <h1>
                      {normalizeCase(offloadDetail.town)}
                      {' '}
                      in
                      {' '}
                      {offloadDetail.state}
                    </h1>

                    <p>
                      Båt :
                      {normalizeCase(offloadDetail.boat.name)}
                      {' '}
                      -
                      {offloadDetail.boat.registration_id}
                    </p>
                    <p>
                      Redskap :
                      {offloadDetail.boat.fishingGear}
                    </p>
                    <p>
                      Landins dato :
                      {normalizeDate(offloadDetail.landingDate)}
                    </p>
                  </div>
                  <div className="map-container">
                    <Map
                      lat={offloadDetail.mapData[0].latitude}
                      lng={offloadDetail.mapData[0].longitude}
                    />
                  </div>
                  <table className="landing-table detail">
                    <tr>
                      <th className="landing-table-header" colSpan="7">Landing Detaljer</th>
                    </tr>
                    <tr>
                      <td>Art</td>
                      <td>Produkttilstand</td>
                      <td>Kvalitet</td>
                      <td>Anvendelse</td>
                      <td>Landingsmåte</td>
                      <td>Konserveringsmåte</td>
                      <td>Rundvekt</td>
                    </tr>
                    {
                            offloadDetail.fish.map((fish, i) => (

                              <tr key={i}>
                                <td>{fish.type}</td>
                                <td>{fish.condition}</td>
                                <td>{fish.quality}</td>
                                <td>{fish.application}</td>
                                <td>{normalizeCase(fish.packaging)}</td>
                                <td>{fish.preservation}</td>
                                <td>{normalizeWeight(fish.weight)}</td>
                              </tr>
                            ))
                        }
                    <tr>
                      <td colSpan="2">Total Rundvekt</td>
                      <td> - </td>
                      <td> - </td>
                      <td> - </td>
                      <td> - </td>
                      <td>{normalizeWeight(offloadDetail.totalWeight)}</td>
                    </tr>
                  </table>
                  <div className="pie-chart">
                    <Pie data={chartData} legend={{ display: true }} redraw width={200} height={300} />
                  </div>
                </>
              )
              : (
                <div className="loader-container">
                  <div className="placeholder-info-container landings">
                    <div className="placeholder-item header" />
                    <div className="placeholder-item info" />
                    <div className="placeholder-item info" />
                    <div className="placeholder-item info" />
                    <div className="placeholder-item info" />
                  </div>
                  <div className="placeholder-item mapload" />
                  <div className="loader-container"><div className="loader" /></div>
                </div>
              )}
          </>
        )
        : (
          <>
            <Redirect to="/404" />
          </>
        )}
    </div>
  );
};
export default OffloadDetails;
