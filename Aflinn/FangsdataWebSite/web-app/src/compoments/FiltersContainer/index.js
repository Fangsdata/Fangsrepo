import React from 'react';
import FilterCheckBox from '../FilterCheckBox';
import FilterSlider from '../FilterSlider';

var fishingGear = ['Settegarn','Reketrål','Teiner','Juksa/pilkAndre', 'liner','Snurrevad','Bunntrål','Autoline']
var months = ['Januar', 'februar', 'Mars', 'April', 'Mai', 'Juli', 'Juni', 'August', 'September', 'Oktober', 'November', 'Desember']
var length = ['under 11m', '11m - 14,99m', '15m - 20,99m', '21m - 27,99m', '28m og over']
var year = [2020,2019,2018,2017,2016,2015,2014,2013,2012,2011,2010,2009,2008,2008];
var state = ['Fartøyfylke', 'Trøndelag', 'Vest-Agder', 'Nordland', 'Rogaland' ,'Hordaland','"Møre og Romsdal"','"Sogn og Fjordane"',
   'Finnmark', 'Akershus' , 'Østfold' , 'Troms', 'Oslo', 'Aust-Agder','Vestfold','Telemark'
   ];
var fish = [
   'Sild, norsk vårgytende',
   'Sei',
   'Tobis og annen sil',
   'Andre skalldyr, bløtdyr og pigghuder',
   'Annen torskefisk',
   'Hyse',
   'Annen flatfisk, bunnfisk og dypvannsfisk',
   'Makrell',
   'Sild, annen',
   'Annen pelagisk fisk',
   'Vassild og strømsild',
   'Kolmule',
   'Øyepål',
   /*'Mesopelagisk fisk',
   'Haifisk',
   'Skater og annen bruskfisk',
   'Torsk',
   'Havbrisling',
   'Dypvannsreke',
   'Uer',
   'Leppefisk',
   'Blåkveite',
   'Steinbiter',
   'Snøkrabbe',
   'Tunfisk og tunfisklignende arter',
   'Taskekrabbe',
   'Lodde',
   'Kongekrabbe, han',
   'Kongekrabbe, annen',
   'Kystbrisling',
   'Brunalger',
   'Andre makroalger',
   'Raudåte',
   'Antarktisk krill',*/
];
var fishType = ['']
const FiltersContainer = ({onChangeFishingGear}) => (
    <div>
      <div className="filter-all-filters filter-header" >
         <p className="filter-container">Filter by</p>
         <p className="filter-container ">fishingGear</p>
         <p className="filter-container ">Length</p>
         <p className="filter-container ">Fish</p>
         <p className="filter-container ">months</p>
         <p className="filter-container ">year</p>
         <p className="filter-container ">State</p>
      </div>
      <div className="filter-all-filters">
         <div className="filter-container"></div>
         <FilterCheckBox items={fishingGear} group="fishingGear"/>
         <FilterCheckBox items={length} group="length"/>
         <FilterCheckBox items={fish} group="fish"/>
         <FilterCheckBox items={months} group="month"/>
         <FilterCheckBox items={year} group="year"/>
         <FilterCheckBox items={state} group="state"/>
      </div>
    </div>
 )

 export default FiltersContainer;