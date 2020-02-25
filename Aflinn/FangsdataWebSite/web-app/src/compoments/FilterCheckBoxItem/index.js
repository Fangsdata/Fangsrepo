import React from 'react';


const FilterCheckBoxItem = ({ item, group }) => (
    <div className="">
        <input type="checkbox" name={group} id={item}/>  
        <label htmlFor={item}>{item}</label>
    </div>
);

 export default FilterCheckBoxItem;