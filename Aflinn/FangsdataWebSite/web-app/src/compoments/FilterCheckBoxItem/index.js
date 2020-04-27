import React from 'react';


const FilterCheckBoxItem = ({ item,value, group, inputEvent,checkState }) => (
    <div className="check-item">
        <input  className="checkbox"
            type="checkbox" 
            name={group} 
            id={item}
            value={value}
            onChange={inputEvent}
            checked={checkState}
        />  
        <label className="filter-label" htmlFor={item}>{item}</label>
    </div>
);

 export default FilterCheckBoxItem;