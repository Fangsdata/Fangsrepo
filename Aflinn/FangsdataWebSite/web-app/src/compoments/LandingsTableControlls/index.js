import React,{useState} from 'react';

const LandingsTableControlls = ({nextPage, prevPage, resultNo, page}) => {
    const [ammountInput, setAmmountInput] = useState(5);
    return(
        <div className="controls-container">
            <button onClick={()=>{prevPage()}}>{"<"}</button>
            <p>{page}</p>
            <button onClick={()=>{nextPage()}}>{">"}</button>
            <div className="show-more">
                <button onClick={()=>resultNo(ammountInput)}>Show more:</button>
                <input 
                    type="number"
                    value={ammountInput}
                    onChange={ e => { if (e.target.value <= 25){
                                            setAmmountInput(e.target.value)  
                                    }else{
                                            setAmmountInput(25)
                                    }
                            }}></input>
            </div>
            
        </div>)
};

export default LandingsTableControlls;