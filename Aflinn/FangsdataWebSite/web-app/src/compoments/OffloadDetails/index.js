import React, { useEffect, useState } from 'react';
import { Redirect, Link } from 'react-router-dom';
import { Pie } from 'react-chartjs-2';
import PropTypes from 'prop-types';
import MapContainer from '../Map';
import { normalizeCase, normalizeWeight, normalizeDate } from '../../services/TextTools';
import Anchor from './anchor.svg';
import EditIcon from './icons8-edit-64.png';


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
  const [offloadDetail, setOffloadDetails] = useState({});
  const [showEdit, setShowEdit] = useState(false);
  const [colums, setColums] = useState({
    art: true,
    Produkttilstand :true,
    Kvalitet: true,
    Anvendelse : true,
    Landingsmåte: true,
    Konserveringsmåte: true,
    Rundvekt: true
  })

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
      labels: data.map((d) => d.label),
      datasets: [{
        label: 'fish dataset',
        backgroundColor: generateColors(data.length),
        data: data.map((d) => d.value),
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
                  <div className="info-wrapper">
                    <img src={Anchor} className="anchor-img" alt="boat" />
                    <div className="landing-info-container">
                      <div className="landings-header">{`${normalizeCase(offloadDetail.town)} i ${offloadDetail.state}`}</div>

                      <Link
                        to={`/boats/${offloadDetail.boat.registration_id}`}
                      >
                        { `${normalizeCase(offloadDetail.boat.name)} - ${offloadDetail.boat.registration_id} `}
                      </Link>
                      <p>
                        {`Redskap : ${offloadDetail.boat.fishingGear}`}
                      </p>
                      <p>
                        {`Landins dato : ${normalizeDate(offloadDetail.landingDate)}`}
                      </p>
                    </div>
                  </div>
                  <div className="map-container">
                    <MapContainer
                      lat={offloadDetail.mapData[0].latitude}
                      lng={offloadDetail.mapData[0].longitude}
                    />
                  </div>
                  <div className="landing-table-container">
                    {!showEdit
                      ?<img src={EditIcon} alt="edit" className="edit-icon" onClick={()=>setShowEdit(!showEdit)}/>
                      :<><img src={EditIcon} alt="edit" className="edit-icon" onClick={()=>setShowEdit(!showEdit)}/>
                       <Edit items={colums}/></>
                    }
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
                  </div>
                  
                  <div className="pie-chart">
                    <Pie
                      data={chartData}
                      legend={{ display: true, position: 'left' }}
                      redraw
                      width={450}
                      height={400}
                    />
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

OffloadDetails.propTypes = {
  offloadId: PropTypes.number.isRequired,
};

const Edit = ({items, inputEvent})=>(<>
  {Object.keys(items).map((item)=>(
    <>
      <input
        className="checkbox"
        type="checkbox"
        name="filters"
        id={item}
        value={item}
        onChange={inputEvent}
        checked={items[item]}
      />
      <label htmlFor={item}>{item}</label>
    </>
  ))}
</>);

export default OffloadDetails;
