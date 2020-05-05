import React,{useState} from 'react';

const LandingsTableControlls = ({nextPage, prevPage, resultNo}) => {
    const [ammountInput, setAmmountInput] = useState(5);
    return(
        <>
            <button onClick={()=>{prevPage()}}>Prev Page</button>
            <button onClick={()=>{nextPage()}}>Next Page</button>
            <input 
                type="number"
                value={ammountInput}
                onChange={ e => { if (e.target.value <= 25){
                                        setAmmountInput(e.target.value)  
                                }else{
                                        setAmmountInput(25)
                                }
                        }}></input>
            <button onClick={()=>resultNo(ammountInput)}>Submit</button>
        </>)
};

export default LandingsTableControlls;