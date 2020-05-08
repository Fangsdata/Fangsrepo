import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import PropTypes, { func } from 'prop-types';
import FilterCheckBox from '../FilterCheckBox';
import downArrow from './arrow_drop_down-24px.svg';
import upArrow from './arrow_drop_up-24px.svg';
import filterIcon from './filter_list-24px.svg';
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
                <img className="filter-icon" src={filterIcon} alt="" />
                Filter by:
                {' '}
                <img className="arrow-icon" src={upArrow} alt="" />
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
                      if (item === 'boatLength') {
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
                <img className="filter-icon" src={filterIcon} alt="" />
                Show filters
                {' '}
                <img className="arrow-icon" src={downArrow} alt="" />
              </p>
            </>
          )}

      </div>
    </div>
  );
};

FiltersContainer.propTypes = {
  inputEvent: func.isRequired,
  allFilters: PropTypes.object.isRequired,
  updateDate: func.isRequired,
};


export default FiltersContainer;
