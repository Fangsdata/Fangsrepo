import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import FilterCheckBox from '../FilterCheckBox';
import down_arrow from './arrow_drop_down-24px.svg';
import up_arrow from './arrow_drop_up-24px.svg';
import filter_icon from './filter_list-24px.svg';
import 'react-datepicker/dist/react-datepicker.css';


const FiltersContainer = ({ inputEvent, allFilters, updateDate }) => {
  const [showFilters, setShowFilters] = useState(false);
  const [selectedDate, setSelectedDate] = useState(new Date());
  const headers = ['Fishing Gear', 'Boat leangth', 'Fish name', 'Landing state'];

  return (
    <div>
      <div className="f-header">
        {showFilters
          ? (
            <>
              <p className="f-container-title" onClick={() => setShowFilters(!showFilters)}>
                <img className="filter-icon" src={filter_icon} alt="" />
                Filter by:
                {' '}
                <img className="arrow-icon" src={up_arrow} alt="" />
              </p>
              { showFilters
                ? (
                  <div className="filter-all-filters">
                    <div className="date-container">
                      <div className="date-headers">
                        <p>Velg måned:</p>
                      </div>
                      <DatePicker
                        selected={selectedDate}
                        onChange={(date) => {
                          setSelectedDate(date);
                          updateDate(date);
                        }}
                        dateFormat="MM/yyyy"
                        showMonthYearPicker
                        showFullMonthYearPicker
                      />
                    </div>
                    { headers.map((header) => <p className="f-headers">{header}</p>)}
                    { Object.keys(allFilters).map((item) => {
                      let checboxType = 'checkbox';
                      if (item == 'boatLength') {
                        checboxType = 'radio';
                      }
                      return (
                        <FilterCheckBox
                          key={item}
                          items={allFilters[item]}
                          group={item}
                          inputEvent={inputEvent}
                          checkBoxType={checboxType}
                        />
                      );
                    })}

                  </div>
                )

                : <></> }
            </>
          )
          : (
            <>
              <p className="f-header-title" onClick={() => setShowFilters(!showFilters)}>
                <img className="filter-icon" src={filter_icon} alt="" />
                Show filters
                {' '}
                <img className="arrow-icon" src={down_arrow} alt="" />
              </p>
            </>
          )}

      </div>
    </div>
  );
};

export default FiltersContainer;
