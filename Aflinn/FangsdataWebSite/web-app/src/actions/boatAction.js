import { BOAT_OFFLOAD_DETAILS } from '../Constants';

export const StoredBoatDetails = boat => {
    return ({
        type: BOAT_OFFLOAD_DETAILS,
        payload: boat
    })
}