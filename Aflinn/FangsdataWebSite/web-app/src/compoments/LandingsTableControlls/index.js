import React, { useState } from 'react';
import { func, string } from 'prop-types';

const LandingsTableControlls = ({
  nextPage, prevPage, resultNo, page,
}) => {
  const [ammountInput, setAmmountInput] = useState(5);
  return (
    <div className="controls-container">
      <button onClick={() => { prevPage(); }}>{'<'}</button>
      <p>{page}</p>
      <button onClick={() => { nextPage(); }}>{'>'}</button>
      <div className="show-more">
        <p>mer resultat:</p>

        <select
          value={ammountInput}

          onChange={(e) => {
            console.log(e.target.value);
            if (e.target.value <= 25) {
              setAmmountInput(e.target.value);
            } else {
              setAmmountInput(25);
            }
            resultNo(e.target.value);
          }}
        >
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="25">25</option>
        </select>
      </div>

    </div>
  );
};

LandingsTableControlls.propTypes = {
  nextPage: func.isRequired,
  prevPage: func.isRequired,
  resultNo: func.isRequired,
  page: string,
};

LandingsTableControlls.defaultProps = {
  page: '',
};

export default LandingsTableControlls;
