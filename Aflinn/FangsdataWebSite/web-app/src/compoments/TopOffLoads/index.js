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
            month:[ { title:'Januar', checkState:false, value:'januar'}, 
                    { title:'Februar', checkState:false, value:'februar'},
                    { title:'March', checkState:false, value:'mars'},
                    { title:'April', checkState:false, value:'april'}, 
                    { title:'May', checkState:false, value:'mai'},
                    { title:'July', checkState:false, value:'juni'}, 
                    { title:'Juny', checkState:false, value:'juli'},
                    { title:'August', checkState:false, value:'august'}, 
                    { title:'September', checkState:false, value:'september'},
                    { title:'Oktomber', checkState:false, value:'oktober'}, 
                    { title:'November', checkState:false, value:'november'},
                    { title:'December', checkState:false, value:'desember'} ],
            year:[ { title:'2020', checkState:false, value:'2020'}, 
                   { title:'2019', checkState:false, value:'2019'},
                   { title:'2018', checkState:false, value:'2018'},
                   { title:'2017', checkState:false, value:'2017'}, 
                   { title:'2016', checkState:false, value:'2016'},
                   { title:'2015', checkState:false, value:'2015'},
                   { title:'2014', checkState:false, value:'2014'}, 
                   { title:'2013', checkState:false, value:'2013'},
                   { title:'2012', checkState:false, value:'2012'},
                   { title:'2011', checkState:false, value:'2011'}, 
                   { title:'2010', checkState:false, value:'2010'},
                   { title:'2009', checkState:false, value:'2009'},
                   { title:'2008', checkState:false, value:'2008'}, 
                   { title:'2007', checkState:false, value:'2007'},
                   { title:'2006', checkState:false, value:'2006'},
                   { title:'2005', checkState:false, value:'2005'}, 
                   { title:'2004', checkState:false, value:'2004'},
                   { title:'2003', checkState:false, value:'2003'},
                   { title:'2002', checkState:false, value:'2002'}, 
                   { title:'2001', checkState:false, value:'2001'},
                   { title:'2000', checkState:false, value:'2000'} ],
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

    render(){
        return (
        <div>
            <FilterContainer 
            inputEvent={(e)=>this.inputEvent(e)}
            allFilters={this.state.allFilters}/>
            <OffloadsList 
                offloads={ this.state.offLoads }
            />
            { this.state.offLoads.length < 1
                ?   <div className="loader">Loading...</div>
                : <></>
            }

        </div>);
    }
}

export default TopOffLoads;