import React from 'react';
import FilterCheckBoxItem from '../FilterCheckBoxItem';

const FilterCheckBox = ({ inputEvent, items, group }) => (

    <div className="filter-container">
        { items.map((item) => <FilterCheckBoxItem key={item} group={group} item={item} inputEvent={inputEvent}/>)}
    </div>
);

 export default FilterCheckBox;