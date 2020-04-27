import React from 'react';
import FilterCheckBoxItem from '../FilterCheckBoxItem';

const FilterCheckBox = ({ inputEvent, items, group }) => (

    <div className="filter-container">
        { items.map((item) => <FilterCheckBoxItem 
            key={item} 
            group={group} 
            item={item.title}
            value={item.value}
            checkState={item.checkState}
            inputEvent={inputEvent}/>)
        }
    </div>
);

 export default FilterCheckBox;