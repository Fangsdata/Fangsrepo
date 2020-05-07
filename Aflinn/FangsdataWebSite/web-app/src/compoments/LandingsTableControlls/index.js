import React, { useState } from 'react';

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
        <p>Show more:</p>

        <select
          onClick={() => resultNo(ammountInput)}
          value={ammountInput}
          onChange={(e) => {
            if (e.target.value <= 25) {
              setAmmountInput(e.target.value);
            } else {
              setAmmountInput(25);
            }
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

export default LandingsTableControlls;
