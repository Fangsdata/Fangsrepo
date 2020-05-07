import { BOAT_OFFLOAD_DETAILS } from '../Constants';

export default StoredBoatDetails = (boat) => ({
  type: BOAT_OFFLOAD_DETAILS,
  payload: boat,
});
