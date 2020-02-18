import React from 'react';
import OffladsListItem from '../OffladsListItem';

const OffladsList = ({ offloads }) => (
    <div>
        <div className="row">
        <p className="col-0"></p>
            <p className="col-1"></p>
            <p className="col">Name</p>
            <p className="col-2">Total Offloaded</p>
            <p className="col-2">Largest offloading</p>
            <p className="col-1">Trips</p>
        </div>
        { offloads.map((item, index) => <OffladsListItem 
            key = {item.boatRadioSignalId} item = {item} index={index} />)}
    </div>
);

export default OffladsList;