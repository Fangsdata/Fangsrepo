import React from 'react';
import { Link } from 'react-router-dom';
import PropTypes, {
  string, arrayOf, shape, bool, number,
} from 'prop-types';
import { normalizeWeight, normalizeDate, normalizeCase } from '../../services/TextTools';

const LandingsTable = ({
  landings, landingNo, boatOffloadLoaded, boatOffloadError,
}) => (
  <table className="landing-table">
    <tr>
      <th className="landing-table-header" colSpan="5">Siste landinger</th>
    </tr>
    <tr>
      <td />
      <td>Dato</td>
      <td>Kommune</td>
      <td>Fylke</td>
      <td>Total vekt</td>
    </tr>
    { !boatOffloadError
      ? (
        <>
          { boatOffloadLoaded
            ? landings.map((landing, i) => (
              <tr>
                <td>
                  <Link to={`/offloads/${landing.id}`}>
                    {i + 1 + landingNo}
                    .
                  </Link>
                </td>
                <td><Link to={`/offloads/${landing.id}`}>{normalizeDate(landing.landingDate)}</Link></td>
                <td><Link to={`/offloads/${landing.id}`}>{normalizeCase(landing.town)}</Link></td>
                <td><Link to={`/offloads/${landing.id}`}>{landing.state}</Link></td>
                <td><Link to={`/offloads/${landing.id}`}>{normalizeWeight(landing.totalWeight)}</Link></td>
              </tr>
            ))
            : (
              <th colSpan="5">
                <div className="loader" />
                Loading
              </th>
            )}
        </>
      )
      : <p>there was an error here</p>}
  </table>
);

LandingsTable.propTypes = {
  landings: arrayOf(shape({
    id: number.isRequired,
    landingDate: PropTypes.instanceOf(Date).isRequired,
    town: string.isRequired,
    state: string.isRequired,
    totalWeight: number.isRequired,
  }).isRequired).isRequired,
  landingNo: number,
  boatOffloadLoaded: bool,
  boatOffloadError: bool,
};

LandingsTable.defaultProps = {
  boatOffloadLoaded: true,
  boatOffloadError: false,
  landingNo: 0,
};

export default LandingsTable;
