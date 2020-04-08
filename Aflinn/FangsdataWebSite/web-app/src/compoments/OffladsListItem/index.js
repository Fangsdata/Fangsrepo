import React from 'react';
import { NavLink } from 'react-router-dom';


const OffloadListItem = ({ item, index, color }) => (
    <NavLink to={"/boats/"+ item.boatRadioSignalId}>
        <div className="row" onClick>
            <p className="col-0">{index}</p>
            <img className="col-1 rounded" src={item.boatImage} alt= "a boat"/>
            <p className="col"> {item.boatName}</p>
            <p className="col-2"> {item.boatFishingGear}</p>
            <p className="col-2"> {item.boatLength} m</p>
            <p className="col-2"> {item.totalWeight} kg</p>
        </div>
    </NavLink>
);

export default OffloadListItem;