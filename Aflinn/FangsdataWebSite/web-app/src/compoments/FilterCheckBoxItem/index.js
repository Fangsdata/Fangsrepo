import React from 'react';


const FilterCheckBoxItem = ({ item,value, group, inputEvent,checkState, checkBoxType }) => (
    <div className="check-item">
        <input  className="checkbox"
            type={checkBoxType}
            name={group} 
            id={item}
            value={value}
            onChange={inputEvent}
            checked={checkState}
        />{console.log(checkBoxType)}
        <label className="filter-label" htmlFor={item}>{item}</label>
    </div>
);

 export default FilterCheckBoxItem;