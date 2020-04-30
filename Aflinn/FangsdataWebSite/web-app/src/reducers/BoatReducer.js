import { BOAT_OFFLOAD_DETAILS } from '../Constants';

export default function(state = [], action){
    switch(action.type) {
        case (BOAT_OFFLOAD_DETAILS): 
            console.log("in reducer");
            console.log(action.payload);
            return action.payload;
        default: return state;

    }
}