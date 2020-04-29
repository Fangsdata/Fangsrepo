import React, {useState, useEffect} from 'react';
import icon from "./search-24px.svg";
import { Link } from 'react-router-dom';
import CONST from "../../Constants";

var timeOut;
const SearchBar = () => {

    const [search, updateSearch] = useState("");
    const [isTimedOut, setTimedOut] = useState(false);
    const [foundBoats, setFoundBoats] = useState([]); 
    const [isSearchfield, setSearchfield] = useState(false);

    const StartSearch = () => {}

    const UpdateQuickSearch = (searchTerm)=> {
        console.log(searchTerm);
        if(searchTerm.length > 2){
            fetch(CONST.offloadApi + '/search/boats/' + searchTerm)
            .then((res) => res.json())
            .then((res) => {
                setFoundBoats(res);
                setSearchfield(true);
            });
        }
        else{
            setFoundBoats([]);
            setSearchfield(false);
        }
    }

    return (
    <>
    <div className={ `searchbar ${isSearchfield ? 'open' : ''}` }>
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
        {foundBoats.length != 0
        ?   <div className="quick-search"> 
            <div className="line"></div>
            { foundBoats.map((boat)=> <QuickSearchItem 
                                        name={boat.name}
                                        RadioSignal={boat.radioSignalId}
                                        />) }
            <div className="result-bottom"></div>
       </div>
    : <></>
    }
    </div>
    </>)
}
const QuickSearchItem = ({name, RadioSignal}) => (
    <Link to={"/boats/" + RadioSignal}><div className="search-result">
        {name} 
    </div></Link>
)
export default SearchBar;