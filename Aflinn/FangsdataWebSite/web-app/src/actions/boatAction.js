import { BOAT_OFFLOAD_DETAILS } from '../Constants';

export const boatDetailsForOffloads = boat => {
    return ({
        type: BOAT_OFFLOAD_DETAILS,
        payload: boat
    })
}