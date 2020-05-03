import React,{useState} from 'react';
import FilterCheckBox from '../FilterCheckBox';
import DatePicker from 'react-datepicker';
import down_arrow from "./arrow_drop_down-24px.svg";
import up_arrow from "./arrow_drop_up-24px.svg";
import filter_icon from "./filter_list-24px.svg";


const FiltersContainer = ({inputEvent, allFilters,updateDate}) => {

   const [showFilters, setShowFilters] = useState(false);
   const [startDate, setStartDate] = useState(Date.now());
   const [endDate, setEndDate] = useState(new Date(2020,4));

   return (
   <div>
      <div className="f-header">
      {showFilters
      ? <><p className="f-container-title" onClick={()=> setShowFilters(!showFilters)}><img className="filter-icon" src={filter_icon} alt=""/>Filter by: <img className="arrow-icon" src={up_arrow} alt=""/></p>
         { showFilters
         ?       <div className="filter-all-filters">
         <DatePicker
            selected={startDate}
            onChange={date=>{
               setStartDate(date);
               updateDate(date, endDate)}}
            selectsStart
            startDate={startDate}
            endDate={endDate}
            dateFormat="MM/yyyy"
            showMonthYearPicker
         />
         <DatePicker
            selected={endDate}
            onChange={date=>{
               setEndDate(date);
               updateDate(startDate, date);}}
            selectsEnd
            startDate={startDate}
            endDate={endDate}
            dateFormat="MM/yyyy"
            showMonthYearPicker
         />
                  { Object.keys(allFilters).map(header => <p className="f-container">{header}</p>)}
                   { Object.keys(allFilters).map((item) => {
                     return (<FilterCheckBox
                        key={item} 
                        items={allFilters[item]} 
                        group={item} 
                        inputEvent={inputEvent}
                        />) })}

                  </div>
                  
         : <></>  }</>
      : <><p className="f-header-title" onClick={()=> setShowFilters(!showFilters)}><img className="filter-icon" src={filter_icon} alt=""/>Show filters <img className="arrow-icon" src={down_arrow} alt=""/></p></>
      }
         
      </div>
    </div>
 )};

 export default FiltersContainer;