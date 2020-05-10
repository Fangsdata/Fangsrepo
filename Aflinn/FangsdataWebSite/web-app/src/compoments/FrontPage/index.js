import React from 'react';
import { Link } from 'react-router-dom';
import { getOffloads } from '../../services/OffloadService';
import OffloadsList from '../OffloadsList';

// https://fangsdata-api.herokuapp.com/api/offloads?fishingGear=Garn&Count=5

class FrontPage extends React.Component {
  constructor() {
    super();
    this.state = {
      offLoads0: [],
      offLoads1: [],
      offLoads2: [],
      offLoads3: [],
      tableLoaded0: false,
      tableLoaded1: false,
      tableLoaded2: false,
      tableLoaded3: false,
      tableError: false,
    };
  }


  async componentDidMount() {
    this.setState({
      offLoads0:
          await getOffloads({ count: [5], fishingGear: ['Krokredskap'] }),
      tableLoaded0: true,
      offLoads1: await getOffloads({ count: [5], fishingGear: ['Trål'] }),
      tableLoaded1: true,
    });
    this.setState({
      offLoads2: await getOffloads({ count: [5], fishingGear: ['Snurrevad'] }),
      tableLoaded2: true,
      offLoads3: await getOffloads({ count: [5], fishingGear: ['Garn'] }),
      tableLoaded3: true,
    });
  }

  render() {
    const {
      offLoads0,
      offLoads1,
      offLoads2,
      offLoads3,
      tableLoaded0,
      tableLoaded1,
      tableLoaded2,
      tableLoaded3,
      tableError,
    } = this.state;
    return (
      <div className="front-page">

        {!tableError
          ? (
            <>
              { tableLoaded0
                ? (
                  <>
                    <div className="front-list-container">
                      <OffloadsList
                        offloads={offLoads0}
                        title="Største Krokredskap landing"
                      />
                      <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                    </div>
                  </>
                )
                : (
                  <div className="front-loading-container">
                    <div className="offload-header" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item more" />
                  </div>
                )}
            </>
          )
          : <><p>here was an error</p></>}
        {!tableError
          ? (
            <>
              { tableLoaded1
                ? (
                  <>
                    <div className="front-list-container">
                      <OffloadsList
                        offloads={offLoads1}
                        title="Største Trål landing"
                      />
                      <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                    </div>
                  </>
                )
                : (
                  <div className="front-loading-container">
                    <div className="offload-header" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item more" />
                  </div>
                )}
            </>
          )
          : <><p>here was an error</p></>}
        {!tableError
          ? (
            <>
              { tableLoaded2
                ? (
                  <>
                    <div className="front-list-container">
                      <OffloadsList
                        offloads={offLoads2}
                        title="Største Snurrevad landing"
                      />
                      <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                    </div>
                  </>
                )
                : (
                  <div className="front-loading-container">
                    <div className="offload-header" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item more" />
                  </div>
                )}
            </>
          )
          : <><p>here was an error</p></>}
        {!tableError
          ? (
            <>
              { tableLoaded3
                ? (
                  <>
                    <div className="front-list-container">
                      <OffloadsList
                        offloads={offLoads3}
                        title="Største Garn landing"
                      />
                      <Link to="/topoffloads"><div className="more-btn">More</div></Link>
                    </div>
                  </>
                )
                : (
                  <div className="front-loading-container">
                    <div className="offload-header" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item" />
                    <div className="placeholder-item more" />
                  </div>
                )}
            </>
          )
          : <><p>here was an error</p></>}

      </div>
    );
  }
}

export default FrontPage;
