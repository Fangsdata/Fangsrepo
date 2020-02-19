import React from 'react';


const FilterFishingGear = ({ onChange }) => (

    <div>
        <label htmlFor="Settegarn">Settegarn</label>
        <input type="radio" name="fishingGear" id="Settegarn" onChange={onChange}/>
        <label htmlFor="Reketrål">Reketrål</label>
        <input type="radio" name="fishingGear" id="Reketrål" onChange={onChange}/>
        <label htmlFor="Teiner">Teiner</label>
        <input type="radio" name="fishingGear" id="Teiner" onChange={onChange}/>
        <label htmlFor="Juksa/pilk">Juksa/pilk</label>
        <input type="radio" name="fishingGear" id="Juksa/pilk" onChange={onChange}/>
        <label htmlFor="Andre liner">Andre liner</label>
        <input type="radio" name="fishingGear" id="Andre liner" onChange={onChange}/>
        <label htmlFor="Snurrevad">Snurrevad</label>
        <input type="radio" name="fishingGear" id="Snurrevad" onChange={onChange}/>
        <label htmlFor="Bunntrål">Bunntrål</label>
        <input type="radio" name="fishingGear" id="Bunntrål" onChange={onChange}/>
        <label htmlFor="Autoline">Autoline</label>
        <input type="radio" name="fishingGear" id="Autoline" onChange={onChange}/>
    </div>
);

 export default FilterFishingGear;