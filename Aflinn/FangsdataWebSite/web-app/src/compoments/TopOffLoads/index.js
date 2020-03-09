import React from 'react';
import {getOffloads} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';
import FilterContainer from '../FiltersContainer';

class TopOffLoads extends React.Component {

    async componentDidMount(){
        this.setState({
            offLoads : await getOffloads(),
        });
    }

    state = {
        offLoads: [],
        filter: {
            fishingGear:[],
            boatLength:[],
            fishName:[],
           /* month:[],
            year:[],  
            state:[],*/
        },
        allFilters: [{
            group:"fishingGear",
            data:['Settegarn','Reketrål','Teiner','Juksa/pilkAndre', 'liner','Snurrevad','Bunntrål','Autoline']
        },{
            group:"boatLength",
            data:['under 11m', '11m - 14,99m', '15m - 20,99m', '21m - 27,99m', '28m og over']
        },{
            group:"fishName",
            data:['Sild, norsk vårgytende','Sei','Tobis og annen sil','Andre skalldyr, bløtdyr og pigghuder', 'Annen torskefisk', 'Hyse','Annen flatfisk, bunnfisk og dypvannsfisk','Makrell','Sild, annen','Annen pelagisk fisk', 'Vassild og strømsild', 'Kolmule','Øyepål', /*'Mesopelagisk fisk','Haifisk','Skater og annen bruskfisk','Torsk','Havbrisling', 'Dypvannsreke','Uer','Leppefisk','Blåkveite','Steinbiter','Snøkrabbe','Tunfisk og tunfisklignende arter','Taskekrabbe','Lodde','Kongekrabbe, han','Kongekrabbe, annen','Kystbrisling','Brunalger','Andre makroalger','Raudåte','Antarktisk krill',*/] 
        }]
    }

    async inputEvent(event){    
        const target = event.target;
        const {filter} = this.state;
        if(target.checked){
            let currState = filter[target.name];
            currState.push(target.value);
            filter[target.name] = currState; 
            this.setState(filter);
            this.setState({ offLoads : await getOffloads( filter )});
        }
        else{
            let currState = filter[target.name];
            let itemIndex = currState.indexOf(target.value);
            currState.splice(itemIndex,1);
            filter[target.name] = currState; 
            this.setState(filter);
            this.setState({ offLoads : await getOffloads( filter )}); 
        }

    }

    render(){
        return (
        <div>
            <FilterContainer 
            inputEvent={(e)=>this.inputEvent(e)}
            allFilters={this.state.allFilters}/>
            <OffloadsList 
                offloads={ this.state.offLoads }
            />
        </div>);
    }
}

export default TopOffLoads;

/*
var months = ['Januar', 'februar', 'Mars', 'April', 'Mai', 'Juli', 'Juni', 'August', 'September', 'Oktober', 'November', 'Desember']
var year = ['2020','2019','2018','2017','2016','2015','2014','2013','2012','2011','2010','2009','2008'];
var state = ['Fartøyfylke', 'Trøndelag', 'Vest-Agder', 'Nordland', 'Rogaland' ,'Hordaland','"Møre og Romsdal"','"Sogn og Fjordane"',
   'Finnmark', 'Akershus' , 'Østfold' , 'Troms', 'Oslo', 'Aust-Agder','Vestfold','Telemark']; */