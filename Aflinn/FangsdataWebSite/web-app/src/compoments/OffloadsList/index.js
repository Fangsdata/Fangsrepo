import React from 'react';
import OffladsListItem from '../OffladsListItem';

const OffladsList = ({ offloads }) => (
    <div>
        { offloads.map(item => <OffladsListItem 
            key = {item.boatRadioSignalId} item = {item} />)}
    </div>
);

export default OffladsList;