import React,{useState} from 'react';
import FilterCheckBox from '../FilterCheckBox';


const FiltersContainer = ({inputEvent, allFilters}) => {

   const [showFilters, setShowFilters] = useState(false);

   return (
   <div>
      <div className="filter-header" onClick={()=> setShowFilters(!showFilters)}>
      {showFilters
      ? <><p className="filter-container">Filter by</p>
         {allFilters.map(header => <p className="filter-container">{header.group}</p>)}</>
      : <><p>Show filters</p></>
      }
         
      </div>
      { showFilters
         ?       <div className="filter-all-filters">
                  <div className="filter-container"></div>
                   { allFilters.map((item) => { 
                     return (<FilterCheckBox 
                        key={item.group} 
                        items={item.data} 
                        group={item.group} 
                        inputEvent={inputEvent}/>) })}
                  </div>
         : <></>  }
    </div>
 )};

 export default FiltersContainer;