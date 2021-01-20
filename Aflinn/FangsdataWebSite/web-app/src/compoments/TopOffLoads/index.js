import React from 'react';
import { getOffloads } from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';
import FilterContainer from '../FiltersContainer';
import { normalizeMonth } from '../../services/TextTools';
import LandingsTableControlls from '../LandingsTableControlls';

let filterTimeOut;

class TopOffLoads extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      offLoads: [],
      filter: {
        fishingGear: [],
        boatLength: [],
        fishName: [],
        month: [],
        year: [],
        landingState: [],
        pageNo: [1],
        count: [10]
      },
      allFilters: {
        fishingGear: [{ title: 'Not', checkState: false, value: 'Not' },
          { title: 'Trål', checkState: false, value: 'Trål' },
          { title: 'Pelagisk', checkState: false, value: 'Pelagisk' },
          { title: 'Bur og ruser', checkState: false, value: 'Bur og ruser' },
          { title: 'Andre redskap', checkState: false, value: 'Andre redskap' },
          { title: 'Krokredskap', checkState: false, value: 'Krokredskap' },
          { title: 'Garn', checkState: false, value: 'Garn' },
          { title: 'Snurrevad', checkState: false, value: 'Snurrevad' },
          { title: 'Oppdrett/uspesifisert', checkState: false, value: 'Oppdrett/uspesifisert' }],
        boatLength: [{ title: 'under 11m', value: 'under 11m', checkState: false },
          { title: '11m - 14,99m', value: '11m - 14,99m', checkState: false },
          { title: '15m - 20,99m', value: '15m - 20,99m', checkState: false },
          { title: '21m - 27,99m', value: '21m - 27,99m', checkState: false },
          { title: '28m og over', value: '28 m og over', checkState: false }],
        fishName: [{ title: 'Pelagisk fisk', value: 'Pelagisk fisk', checkState: false },
          { title: 'Torsk og torskeartet fisk', value: 'Torsk og torskeartet fisk', checkState: false },
          { title: 'Flatfisk, annen bunnfisk og dypvannsfisk', value: 'Flatfisk, annen bunnfisk og dypvannsfisk', checkState: false },
          { title: 'Bruskfisk (haifisk, skater, rokker og havmus)', value: 'Bruskfisk (haifisk, skater, rokker og havmus)', checkState: false },
          { title: 'Skalldyr, bløtdyr og pigghuder', value: 'Skalldyr, bløtdyr og pigghuder', checkState: false },
          { title: 'Makroalger (tang og tare)', value: 'Makroalger (tang og tare)', checkState: false }],
        landingState: [{ title: 'Troms og Finnmark', checkState: false, value: 'Troms og Finnmark' },
          { title: 'Nordland', checkState: false, value: 'Nordland' },
          { title: 'Nord-Trøndelag', checkState: false, value: 'Nord-Trøndelag' },
          { title: 'Møre og Romsdal', checkState: false, value: '"Møre og Romsdal"' },
          { title: 'Rogaland', checkState: false, value: 'Rogaland' },
          { title: 'Vest-Agder', checkState: false, value: 'Vest-Agder' },
          { title: 'Aust-Agder', checkState: false, value: 'Aust-Agder' },
          { title: 'Sør-Trøndelag', checkState: false, value: 'Sør-Trøndelag' },
          { title: 'Hordaland', checkState: false, value: 'Hordaland' },
          { title: 'Troms', checkState: false, value: 'Troms' },
          { title: 'Finnmark', checkState: false, value: 'Finnmark' },
          { title: 'Sogn og Fjordane', checkState: false, value: '"Sogn og Fjordane"' },
          { title: 'unknown', checkState: false, value: '??' },
          { title: 'Telemark', checkState: false, value: 'Telemark' },
          { title: 'Østfold', checkState: false, value: 'Østfold' },
          { title: 'Vestfold', checkState: false, value: 'Vestfold' },
          { title: 'Akershus', checkState: false, value: 'Akershus' },
          { title: 'Oslo', checkState: false, value: 'Oslo' },
          { title: 'Buskerud', checkState: false, value: 'Buskerud' }],
      },
      topOffloadsLoaded: false,
      topOfflodError: false,
      selectedMonth: 0,
      selectedYear: 0,
    };
  }

  async componentDidMount() {
    const today = new Date();
    const {filter} = this.state;
    this.setState({ selectedMonth: today.getMonth() + 1, selectedYear: today.getFullYear() });
    this.setState({ offLoads: await getOffloads(filter), topOffloadsLoaded: true });
  }

  async inputEvent(event) {
    const { target } = event;
    const { filter, allFilters } = this.state;

    const index = allFilters[target.name].findIndex((value) => value.title === target.id);
    if (index !== -1 && target.type !== 'radio') {
      allFilters[target.name][index].checkState = !allFilters[target.name][index].checkState;
      this.setState({ allFilters, topOffloadsLoaded: false });
    } else if (target.type === 'radio') {
      allFilters[target.name].forEach((item) => {
        if (item.value !== target.value) {
          item.checkState = false;
        } else {
          item.checkState = true;
        }
      });
      this.setState({ allFilters, topOffloadsLoaded: false });
    }

    if (target.type === 'radio') {
      const currState = filter[target.name];
      currState[0] = target.value;
      filter[target.name] = currState;
      this.setState(filter);


      clearTimeout(filterTimeOut);
      filterTimeOut = setTimeout(async () => {
        this.setState({ offLoads: await getOffloads(filter), topOffloadsLoaded: true });
      }, 1000);
    } else if (target.checked) {
      const currState = filter[target.name];
      currState.push(target.value);
      filter[target.name] = currState;
      this.setState(filter);

      clearTimeout(filterTimeOut);
      filterTimeOut = setTimeout(async () => {
        this.setState({ topOffloadsLoaded: false });
        this.setState({ offLoads: await getOffloads(filter), topOffloadsLoaded: true });
      }, 1000);
    } else {
      const currState = filter[target.name];
      const itemIndex = currState.indexOf(target.value);
      currState.splice(itemIndex, 1);
      filter[target.name] = currState;
      this.setState(filter);

      clearTimeout(filterTimeOut);
      filterTimeOut = setTimeout(async () => {
        this.setState({ offLoads: await getOffloads(filter), topOffloadsLoaded: true });
      }, 1000);
    }
  }

  async updateDate(selectedDate) {
    const years = [selectedDate.getFullYear(), selectedDate.getFullYear()];
    const months = [selectedDate.getMonth() + 1, selectedDate.getMonth() + 2];

    const { filter } = this.state;
    filter.month = months;
    filter.year = years;
    this.setState({ filter, offLoads: [], topOffloadsLoaded: false });

    clearTimeout(filterTimeOut);
    filterTimeOut = setTimeout(async () => {
      this.setState({
        offLoads: await getOffloads(filter),
        selectedMonth: months[0],
        selectedYear: years[0],
        topOffloadsLoaded: true,
      });
    }, 1000);
  }

  render() {
    const {
      topOffloadsLoaded, topOfflodError, selectedMonth, selectedYear, offLoads, allFilters, filter
    } = this.state;
    return (
      <div>
        <FilterContainer
          inputEvent={(e) => {
            this.inputEvent(e);
          }}
          updateDate={(d) => this.updateDate(d)}
          allFilters={allFilters}
        />
        {!topOfflodError
          ? (
            <>
              { topOffloadsLoaded
                ? (
                  <OffloadsList
                    offloads={offLoads}
                    pageNo={filter.pageNo[0]}
                    title={`Største landing i ${normalizeMonth(selectedMonth)} ${selectedYear}`}
                  />
                )
                : <div className="loader">Loading...</div>}
            </>
          )
          : <><p>error Loading toplist</p></>}
            <LandingsTableControlls
              nextPage={async() => {
                let page = filter.pageNo[0];
                page += 1;
                let newFilter = filter;
                newFilter.pageNo = [page];
                this.setState({ filter: newFilter, offLoads: await getOffloads(newFilter) });
              }}
              prevPage={ async ()=>{
                let page = filter.pageNo[0];
                if(page > 1 ){
                  page -= 1;
                  let newFilter = filter;
                  newFilter.pageNo = [page];
                  this.setState({ filter: newFilter, offLoads: await getOffloads(newFilter) });
                }
              }}
              resultNo={ async (no)=>{
                let newFilter = filter;
                newFilter.pageNo = [1];
                newFilter.count = [no];
                this.setState({ filter: newFilter, offLoads: await getOffloads(newFilter) });
              }}
              page={filter.pageNo[0]}
              defaultPageSize={filter.count[0]}
              pageSizeOptions={[10,25,50]}
            />
      </div>
    );
  }
}

export default TopOffLoads;
