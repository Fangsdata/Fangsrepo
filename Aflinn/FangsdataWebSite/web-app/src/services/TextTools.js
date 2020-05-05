import { format } from 'date-fns';


export const normalizeCase = (e) => {
    e = e.toLocaleLowerCase();
    e = e.charAt(0).toUpperCase() + e.slice(1);


    return e;
}

export const normalizeWeight = (e) => {
    if (typeof e == 'number') {
        if (e >= 1000){
            return e / 1000 + " t"
        } 
        else if (e <= 1){
            return e * 1000 + " gr"
        }
        else{
            return e + " kg"
        }
    }

    return e;
}

export const normalizeDate  = (e) => {
    return format(new Date(e), "d/M/yyyy")

}