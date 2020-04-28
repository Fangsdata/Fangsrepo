import React, {useState, useEffect} from 'react';
import icon from "./search-24px.svg";
import { Link } from 'react-router-dom';
import CONST from "../../Constants";

var timeOut;
const SearchBar = () => {

    const [search, updateSearch] = useState("");
    const [isTimedOut, setTimedOut] = useState(false);
    const [foundBoats, setFoundBoats] = useState([]); 

    const StartSearch = () => {}

    const UpdateQuickSearch = (searchTerm)=> {
        console.log(searchTerm);
        if(searchTerm.length > 2){
            fetch(CONST.offloadApi + '/search/boats/' + searchTerm)
            .then((res) => res.json())
            .then((res) => {
                setFoundBoats(res);
            });
        }
        else{
            setFoundBoats([]);
        }
    }

    return (
    <>
    <div className="searchbar">
        <input className="search-inp"
            placeholder="Search for boats"
            value={search}
            onInput= { e => {
                updateSearch( e.target.value );
                clearTimeout(timeOut);
                var searchTerm  = e.target.value;
                timeOut = setTimeout(()=> UpdateQuickSearch(searchTerm), 500);
                
                } }
            onKeyPress= { e => {
                    if(e.key == 'Enter'){
                        StartSearch();
                    }
                }}
            type="text"
        />
        <button 
        className="search-btn"
        onClick={() => StartSearch()}>
        <img className="search-icon" src={icon} alt=""/></button>
    </div>
    {foundBoats.length != 0
    ?   <div className="quick-search"> 
        { foundBoats.map((boat)=> <QuickSearchItem 
                                    name={boat.name}
                                    RadioSignal={boat.radioSignalId}
                                    />) }
        <div className="result-bottom"></div>
       </div>
    : <></>
    }
    </>)
}
const QuickSearchItem = ({name, RadioSignal}) => (
    <div className="search-result">
        <Link to={"/boats/" + RadioSignal}>{name} </Link>
    </div>
)
export default SearchBar;