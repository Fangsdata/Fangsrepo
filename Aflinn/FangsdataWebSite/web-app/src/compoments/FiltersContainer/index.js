import React,{useState} from 'react';
import FilterCheckBox from '../FilterCheckBox';
import DatePicker from 'react-datepicker';
import down_arrow from "./arrow_drop_down-24px.svg";
import up_arrow from "./arrow_drop_up-24px.svg";
import filter_icon from "./filter_list-24px.svg";
import "react-datepicker/dist/react-datepicker.css";


const FiltersContainer = ({inputEvent, allFilters,updateDate}) => {

   const [showFilters, setShowFilters] = useState(false);
   const [startDate, setStartDate] = useState(new Date());
   const headers = ["Fishing Gear", "Boat leangth", "Fish name", "Landing state"]

   return (
   <div>
      <div className="f-header">
      {showFilters
      ? <><p className="f-container-title" onClick={()=> setShowFilters(!showFilters)}><img className="filter-icon" src={filter_icon} alt=""/>Filter by: <img className="arrow-icon" src={up_arrow} alt=""/></p>
         { showFilters
         ?       <div className="filter-all-filters">
         <div className="date-container">
            <div className="date-headers">
               <p>Select month:</p>
            </div>
            <DatePicker
               selected={startDate}
               onChange={date => setStartDate(date)}
               dateFormat="MM/yyyy"
               showMonthYearPicker
               showFullMonthYearPicker
            />
         </div>
                  { headers.map(header => <p className="f-headers">{header}</p>)}
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