import React from 'react';
import { Link } from 'react-router-dom';
import PropTypes, { number } from 'prop-types';
import { normalizeWeight } from '../../services/TextTools';

const OffloadListItem = ({ item, index }) => (
  <Link className="offload-link" to={`/boats/${item.boatRegistrationId}`}>
    <div className="offload-row">
      <p className="offload-index">{index}</p>
      <p className="offload-name">
        {item.boatName ? item.boatName : item.boatRegistrationId}
      </p>
      <p className="offload-group">
        {item.boatFishingGear}
      </p>
      <p className="offload-group">
        {`${item.boatLength} m`}
      </p>
      <p className="offload-group">
        {normalizeWeight(item.totalWeight)}
      </p>
    </div>
  </Link>
);

OffloadListItem.propTypes = {
  item: PropTypes.shape({
    avrage: PropTypes.number,
    boatFishingGear: PropTypes.string,
    boatImage: PropTypes.string,
    boatLandingState: PropTypes.string,
    boatLandingTown: PropTypes.string,
    boatLength: PropTypes.number,
    boatName: PropTypes.string,
    boatNationality: PropTypes.string,
    boatRadioSignalId: PropTypes.string,
    boatRegistrationId: PropTypes.string,
    id: PropTypes.number,
    largestLanding: PropTypes.number,
    smallest: PropTypes.number,
    state: PropTypes.string,
    totalWeight: PropTypes.number,
    town: PropTypes.string,
    trips: PropTypes.number,
  }).isRequired,
  index: number.isRequired,
};

export default OffloadListItem;
