import React from 'react';
import { Link } from 'react-router-dom';
import { normalizeLength } from '../../services/TextTools';

const SearchItem = ({ item, index }) => (
  <Link className="offload-link" to={`/boats/${item.registration_id}`}>
    <div className="offload-row">
      <p className="offload-index">{index}</p>
      <p className="offload-name">
        {item.name ? item.name : item.registration_id}
      </p>
      <p className="offload-group">
        {item.fishingGear}
      </p>
      <p className="offload-group">
        {`${normalizeLength(item.length)}`}
      </p>
      <p className="offload-group">
        {item.radioSignalId}
      </p>
    </div>
  </Link>
);

// OffloadListItem.propTypes = {
//   item: PropTypes.shape({
//     avrage: PropTypes.number,
//     boatFishingGear: PropTypes.string,
//     boatImage: PropTypes.string,
//     boatLandingState: PropTypes.string,
//     boatLandingTown: PropTypes.string,
//     boatLength: PropTypes.number,
//     boatName: PropTypes.string,
//     boatNationality: PropTypes.string,
//     boatRadioSignalId: PropTypes.string,
//     boatRegistrationId: PropTypes.string,
//     id: PropTypes.number,
//     largestLanding: PropTypes.number,
//     smallest: PropTypes.number,
//     state: PropTypes.string,
//     totalWeight: PropTypes.number,
//     town: PropTypes.string,
//     trips: PropTypes.number,
//   }).isRequired,
//   index: number.isRequired,
// };

export default SearchItem;
