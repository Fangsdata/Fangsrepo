import React from 'react';
import { Link } from 'react-router-dom';
import { normalizeWeight,normalizeDate, normalizeCase } from '../../services/TextTools';

class LandingsTable extends React.Component {

    constructor(props) {
        super(props);
    }

  render() {
      const {landings, landingNo,boatOffloadLoaded} = this.props;
    return (
        <table className="landing-table">
        <tr>
            <th className="landing-table-header" colSpan="5">Siste landinger</th>
        </tr>
        <tr>
            <td></td>
            <td>Dato</td>
            <td>Kommune</td>
            <td>Fylke</td>
            <td>Total vekt</td>
        </tr>
        { boatOffloadLoaded
         ? landings.map((landing, i)=>(
            <tr>
                <td><Link to={"/offloads/"+ landing.id}> {i+1 + landingNo}. </Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeDate(landing.landingDate)}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeCase(landing.town)}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.state}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeWeight(landing.totalWeight)}</Link></td>
            </tr>))
          :<th colSpan="5"><div className="loader"></div></th>}
        </table>
    );
  }
}

export default LandingsTable;