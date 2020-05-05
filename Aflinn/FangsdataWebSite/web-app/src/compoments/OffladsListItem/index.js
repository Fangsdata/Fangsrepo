import React from 'react';
import { Link } from 'react-router-dom';


const OffloadListItem = ({ item, index, color }) => (
    <Link className="offload-link" to={"/boats/"+ item.boatRadioSignalId}>
        <div className="offload-row">
            <p className="offload-index">{index}</p>
            <p className="offload-name"> {item.boatName}</p>
            <p className="offload-group"> {item.boatFishingGear}</p>
            <p className="offload-group"> {item.boatLength} m</p>
            <p className="offload-group"> {item.totalWeight} kg</p>
        </div>
    </Link>
);

export default OffloadListItem;