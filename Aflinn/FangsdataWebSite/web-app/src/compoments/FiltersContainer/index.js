import React,{useState} from 'react';
import FilterCheckBox from '../FilterCheckBox';
import down_arrow from "./arrow_drop_down-24px.svg";
import up_arrow from "./arrow_drop_up-24px.svg";
import filter_icon from "./filter_list-24px.svg";


const FiltersContainer = ({inputEvent, allFilters}) => {

   const [showFilters, setShowFilters] = useState(false);

   return (
   <div>
      <div className="f-header">
      {showFilters
      ? <><p className="f-container-title" onClick={()=> setShowFilters(!showFilters)}><img className="filter-icon" src={filter_icon} alt=""/>Filter by: <img className="arrow-icon" src={up_arrow} alt=""/></p>
         { showFilters
         ?       <div className="filter-all-filters">
                  {allFilters.map(header => <p className="f-container">{header.group}</p>)}
                   { allFilters.map((item) => { 
                     return (<FilterCheckBox 
                        key={item.group} 
                        items={item.data} 
                        group={item.group} 
                        inputEvent={inputEvent}/>) })}
                  </div>
         : <></>  }</>
      : <><p className="f-header-title" onClick={()=> setShowFilters(!showFilters)}><img className="filter-icon" src={filter_icon} alt=""/>Show filters <img className="arrow-icon" src={down_arrow} alt=""/></p></>
      }
         
      </div>
      {/* { showFilters
         ?       <div className="filter-all-filters">
                  {allFilters.map(header => <p className="filter-container">{header.group}</p>)}
                  <div className="filter-container"></div>
                   { allFilters.map((item) => { 
                     return (<FilterCheckBox 
                        key={item.group} 
                        items={item.data} 
                        group={item.group} 
                        inputEvent={inputEvent}/>) })}
                  </div>
         : <></>  } */}
    </div>
 )};

 export default FiltersContainer;