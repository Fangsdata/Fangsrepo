import React from 'react';


const FilterCheckBoxItem = ({ item, group, inputEvent }) => (
    <div className="check-item">
        <input  className="checkbox"
            type="checkbox" 
            name={group} 
            id={item}
            value={item}
            onChange={inputEvent}
        />  
        <label className="filter-label" htmlFor={item}>{item}</label>
    </div>
);

 export default FilterCheckBoxItem;