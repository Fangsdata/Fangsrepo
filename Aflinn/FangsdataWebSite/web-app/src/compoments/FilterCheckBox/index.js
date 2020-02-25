import React from 'react';
import FilterCheckBoxItem from '../FilterCheckBoxItem';

const FilterCheckBox = ({ onChange, items, group }) => (

    <div className="filter-container">
        { items.map((item) => <FilterCheckBoxItem group={group} item={item}/>)}
    </div>
);

 export default FilterCheckBox;
 /*
            <label htmlFor="Settegarn">Settegarn</label>
            <input type="checkbox" name="fishingGear" id="Settegarn" onChange={onChange}/>  
*/ 