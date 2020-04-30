import React from 'react';
import OffladsListItem from '../OffladsListItem';

const OffladsList = ({ offloads }) => (
    <div className="offload-table">
        <div className="offload-header">Top Offloads</div>
        <OffladsListItem 
        item = {{
            boatImage: "https://www.publicdomainpictures.net/pictures/30000/velka/plain-white-background.jpg",
            boatName: "Name",
            boatFishingGear: "boatFishingGear",
            boatLength : 'boatLength',
            totalWeight: "totalWeight",
            }} 
            index={'#'} 
            />
            
        { offloads.map((item, index) => <OffladsListItem 
            key = {item.boatRadioSignalId} item = {item} index={index+1} />)}
    </div>
);

export default OffladsList;