import React from 'react';

const OffloadListItem = ({ item }) => (
        <div>
            <img src={item.boatImage} alt= "a boat"/>
            <p> {item.boatName}</p>
            <p> {item.totalWeight}</p>
            <p> {item.largestLanding}</p>
            <p> {item.trips}</p>
        </div>
);

export default OffloadListItem;