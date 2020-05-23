import React, { useEffect, useState } from 'react';
import { Redirect, Link } from 'react-router-dom';
import { Pie } from 'react-chartjs-2';
import PropTypes, { func } from 'prop-types';
import MapContainer from '../Map';
import { normalizeCase, normalizeWeight, normalizeDate } from '../../services/TextTools';
import Anchor from './anchor.svg';
import EditIcon from './icons8-edit-64.png';

const OffloadDetails = ({ offloadId }) => {
  const [chartData, setChartData] = useState(
    {
      labels: ['NON'],
      datasets: [{
        label: '',
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
                       <Edit items={colums} 
                       inputEvent={(e)=>{
                         let newColums = colums;
                         newColums[e] = !newColums[e];
                         setColums(newColums);
                       }}/></>
                    }
                    <LandingsTable
                      totalWeight={offloadDetail.totalWeight}
                      fish={offloadDetail.fish}
                      headers={colums}
                    />
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

class Edit extends React.Component {

  constructor(props) {
    super(props);
    const {items} = this.props;
    this.state = {
      items: items
    };
  }
  com
  render() {
    const {inputEvent} = this.props;
    const {items} = this.state;
    return (
      <>
        {Object.keys(items).map((item)=>(
          <>
            <input
              className="checkbox"
              type="checkbox"
              name="filters"
              id={item}
              value={item}
              onChange={()=>{
                inputEvent(item);
                let newItems = items;
                if(newItems[item]){
                  newItems[item] = true;
                }
                else{
                  newItems[item] = false;
                }
                this.setState({items: newItems})
              }}
              checked={items[item]}
            />
            <label htmlFor={item}>{item}</label>
          </>
        ))}
      </>);
  }
}

Edit.propTypes = {
  items: PropTypes.object,
  inputEvent: func
};

const LandingsTable = (({headers, fish, totalWeight })=>{

  const offloadWidthIndex = () => {
    let i = 0;
    Object.keys(headers).map((head) => { if (headers[head]) i++ })
    return i;
  }

  return( <table className="landing-table detail">
  <tr>
    <th className="landing-table-header" colSpan="7">Landing Detaljer</th>
  </tr>
  <tr>
  { Object.keys(headers).map((head)=>(
    <>
      {headers[head]
      ?<td>{head}</td>
      :<></>}
    </>
  ))}
  </tr>
  {
    fish.map((fish, i) => (
      <tr key={i}>
        {headers["art"] ?<td>{fish.type}</td> :<></>}
        {headers["Produkttilstand"] ?<td>{fish.condition}</td>: <></>}
        {headers["Kvalitet"] ?<td>{fish.quality}</td>: <></>}
        {headers["Anvendelse"] ?<td>{fish.application}</td>: <></>}
        {headers["Landingsmåte"] ?<td>{normalizeCase(fish.packaging)}</td>: <></>}
        {headers["Konserveringsmåte"] ?<td>{fish.preservation}</td>: <></>}
        {headers["Rundvekt"] ?<td>{normalizeWeight(fish.weight)}</td>: <></>}
      </tr>
    ))
  }
  <tr>
    { offloadWidthIndex() !== 1
   ?<>
      <td colSpan={ offloadWidthIndex() - 1 }>Total Rundvekt</td>
      <td>{normalizeWeight(totalWeight)}</td>
    </>
    :<td>Total Rundvekt {normalizeWeight(totalWeight)}</td>}
  </tr>
</table>)
});

export default OffloadDetails;
