import React from 'react';
import {getOffloads} from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';
import FilterContainer from '../FiltersContainer';

var filterTimeOut;

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
            month:[],
            year:[],  
            landingState:[],
        },
        allFilters: {
            fishingGear: [ { title:'Not', checkState:false, value:'Not' },
                   { title:'Trål', checkState:false, value:'Trål' },
                   { title:'Bur og ruser', checkState:false, value:'Bur og ruser' },
                   { title:'Andre redskap', checkState:false, value:'Andre redskap' },
                   { title:'Krokredskap', checkState:false, value:'Krokredskap' },
                   { title:'Garn', checkState:false, value:'Garn' },
                   { title:'Snurrevad', checkState:false, value:'Snurrevad' },
                   { title:'Oppdrett/uspesifisert', checkState:false, value:'Oppdrett/uspesifisert' }],
            boatLength: [ { title:'under 11m',value:'under 11m', checkState:false },
                   { title: '11m - 14,99m',value:'11m - 14,99m', checkState:false },
                   { title: '15m - 20,99m',value:'15m - 20,99m',  checkState:false },
                   { title: '21m - 27,99m', value:'21m - 27,99m', checkState:false },
                   { title: '28m og over', value:'28m og over', checkState:false }],
            fishName: [{ title:'Pelagisk fisk', value:'Pelagisk fisk',checkState:false },
                  { title:'Torsk og torskeartet fisk',value:'Torsk og torskeartet fisk', checkState:false },
                  { title:'Flatfisk, annen bunnfisk og dypvannsfisk',value:'Flatfisk, annen bunnfisk og dypvannsfisk', checkState:false },
                  { title:'Bruskfisk (haifisk, skater, rokker og havmus)',value:'Bruskfisk (haifisk, skater, rokker og havmus)', checkState:false },
                  { title:'Skalldyr, bløtdyr og pigghuder',value:'Skalldyr, bløtdyr og pigghuder', checkState:false },
                  { title:'Makroalger (tang og tare)',value:'Makroalger (tang og tare)', checkState:false }],
            landingState:[ { title: 'Finnmark', checkState:false, value:'Finnmark'},
                   { title: 'Nordland', checkState:false, value:'Nordland'},
                   { title: 'Nord-Trøndelag', checkState:false, value:'Nord-Trøndelag'},
                   { title: 'Møre og Romsdal', checkState:false, value:'"Møre og Romsdal"'},
                   { title: 'Rogaland', checkState:false, value:'Rogaland'},
                   { title: 'Vest-Agder', checkState:false, value:'Vest-Agder'},
                   { title: 'Aust-Agder', checkState:false, value:'Aust-Agder'},
                   { title: 'Sør-Trøndelag', checkState:false, value:'Sør-Trøndelag'},
                   { title: 'Hordaland', checkState:false, value:'Hordaland'},
                   { title: 'Troms', checkState:false, value:'Troms'},
                   { title: 'Sogn og Fjordane', checkState:false, value:'"Sogn og Fjordane"'},
                   { title: 'unknown', checkState:false, value:'??'},
                   { title: 'Telemark', checkState:false, value:'Telemark'},
                   { title: 'Østfold', checkState:false, value:'Østfold'},
                   { title: 'Vestfold', checkState:false, value:'Vestfold'},
                   { title: 'Akershus', checkState:false, value:'Akershus'},
                   { title: 'Oslo', checkState:false, value:'Oslo'},
                   { title: 'Buskerud', checkState:false, value:'Buskerud'} ]
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
            
            clearTimeout(filterTimeOut);
            filterTimeOut = setTimeout( async ()=> {
                this.setState({ offLoads : await getOffloads( filter )});
            }, 1000);

        }
        else{
            let currState = filter[target.name];
            let itemIndex = currState.indexOf(target.value);
            currState.splice(itemIndex,1);
            filter[target.name] = currState; 
            this.setState(filter);
            
            clearTimeout(filterTimeOut);
            filterTimeOut = setTimeout( async ()=> {
                this.setState({ offLoads : await getOffloads( filter )});
            }, 1000);        
        }

    }
    async updateDate(start,end){
        if(start.getTime() <= end.getTime()){
            let months = [start.getMonth() + 1, end.getMonth() + 1];
            let years = [start.getFullYear(), end.getFullYear()];
            let filter = this.state.filter;
            filter.month = months;
            filter.year = years;
            this.setState({ filter });
            this.setState({ offLoads : await getOffloads( filter )});
        }
    }
    render(){
        return (
        <div>
            <FilterContainer 
                inputEvent={(e)=>this.inputEvent(e)}
                updateDate={(start, end)=>this.updateDate(start,end)}
                allFilters={this.state.allFilters}/>
            <OffloadsList 
                offloads={ this.state.offLoads }
            />
            { this.state.offLoads.length < 1
                ? <div className="loader">Loading...</div>
                : <></>
            }

        </div>);
    }
}

export default TopOffLoads;