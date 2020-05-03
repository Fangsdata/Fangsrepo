import React, { useState } from 'react';
import icon from "./search-24px.svg";
import { Link } from 'react-router-dom';
import {OFFLOADAPI} from "../../Constants";
import { connect } from 'react-redux';
import { StoredBoatDetails } from '../../actions/boatAction';

var timeOut;
const SearchBar = ({StoredBoatDetails}) => {

    const [search, updateSearch] = useState("");
    const [foundBoats, setFoundBoats] = useState([]); 

    const StartSearch = () => {}

    const UpdateQuickSearch = (searchTerm)=> {
        if(searchTerm.length > 2){
            fetch(OFFLOADAPI + '/search/boats/' + searchTerm)
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
    <div className={ `searchbar ${foundBoats.length != 0 ? 'open' : ''}` }>
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
        { foundBoats.length != 0
       ?   <div className="quick-search"> 
           <div className="line"></div>
           { foundBoats.map((boat)=> <QuickSearchItem 
                                        searchItemTitle={boat.name + " - " + boat.registration_id}
                                        RadioSignal={boat.radioSignalId}
                                        ClickedEvent={ (selectedItem) =>{
                                            let item = foundBoats.find(b => b.radioSignalId === selectedItem);
                                            StoredBoatDetails(item);
                                            updateSearch("");
                                            setFoundBoats([]);}}
                                        />) }
            <div className="result-bottom"></div>
        </div>
        : <></>
    }
    </div>
    </>)
}
const QuickSearchItem = ({searchItemTitle, RadioSignal, ClickedEvent}) => (
    <Link to={"/boats/" + RadioSignal} onClick={() => {ClickedEvent(RadioSignal)}}>
        <div className="search-result">
            {searchItemTitle} 
        </div>
    </Link>
)
export default connect(null,{StoredBoatDetails})(SearchBar);