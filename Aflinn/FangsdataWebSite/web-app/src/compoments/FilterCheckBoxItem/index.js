import React from 'react';


const FilterCheckBoxItem = ({ item, group, inputEvent }) => (
    <div className="">
        <input 
            type="checkbox" 
            name={group} 
            id={item}
            value={item}
            onChange={inputEvent}
        />  
        <label htmlFor={item}>{item}</label>
    </div>
);

 export default FilterCheckBoxItem;