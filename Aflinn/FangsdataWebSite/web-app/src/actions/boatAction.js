import { BOAT_OFFLOAD_DETAILS } from '../Constants';

export const boatDetailsForOffloads = boat => {
    console.log("in action");
    console.log(boat)
    return ({
        type: BOAT_OFFLOAD_DETAILS,
        payload: boat
    })
}