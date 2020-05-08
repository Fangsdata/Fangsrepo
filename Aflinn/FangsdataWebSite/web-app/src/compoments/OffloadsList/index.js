import React from 'react';
import PropTypes from 'prop-types';
import OffladsListItem from '../OffladsListItem';


const OffladsList = ({ offloads, title }) => (
  <div className="offload-table">
    <div className="offload-header">{title}</div>
    <OffladsListItem
      item={{
        boatName: 'Navn',
        boatFishingGear: 'Relskap',
        boatLength: 'Båt lengde',
        totalWeight: 'Total vekt',
      }}
      index="#"
    />

    { offloads.map((item, index) => (
      <OffladsListItem
        key={item.boatRegistrationId}
        item={item}
        index={index + 1}
      />
    ))}
  </div>
);

OffladsList.propTypes = {
  offloads: PropTypes.arrayOf(PropTypes.shape({
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
  })).isRequired,
  title: PropTypes.string,
};

OffladsList.defaultProps = {
  title: '',
};


export default OffladsList;
