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
        allFilters: {
            fishingGear:[ { title:'Settegarn', checkState:false, value:'Settegarn' },
                   { title:'Reketrål', checkState:false, value:'Reketrål' }],
            boatLength: [ { title:'under 11m',value:'under 11m', checkState:false },
                   { title: '11m - 14,99m',value:'11m - 14,99m', checkState:false },
                   { title: '15m - 20,99m',value:'15m - 20,99m',  checkState:false },
                   { title: '21m - 27,99m', value:'21m - 27,99m', checkState:false },
                   { title: '28m og over', value:'28m og over', checkState:false }],
            fishName: [{ title:'Sild, norsk vårgytende', value:'Sild, norsk vårgytende',checkState:false },
                  { title:'Sei',value:'sei', checkState:false }]
        }
    }

    async inputEvent(event){    
        const target = event.target;
        const {filter, allFilters } = this.state;
        let index = allFilters[target.name].findIndex((value)=> value.title == target.id );
        if(index != -1){
            allFilters[target.name][index].checkState = !allFilters[target.name][index].checkState;
            this.setState(allFilters);
        }
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