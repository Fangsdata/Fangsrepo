import React from 'react';
import OffladsListItem from '../OffladsListItem';

const OffladsList = ({ offloads }) => (
    <div className="offload-table">
        <div className="offload-header">Top Offloads</div>
        <OffladsListItem 
        item = {{
            boatName: "Navn",
            boatFishingGear: "Relskap",
            boatLength : 'Båt lengde',
            totalWeight: "Total vekt",
            }} 
            index={'#'} 
            />
            
        { offloads.map((item, index) => <OffladsListItem 
            key = {item.boatRegistrationId} item = {item} index={index+1} />)}
    </div>
);

export default OffladsList;