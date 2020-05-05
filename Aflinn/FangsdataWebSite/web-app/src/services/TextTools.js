import { format } from 'date-fns';
import { id, el } from 'date-fns/locale';


export const normalizeCase = (e) => {
    e = e.toLocaleLowerCase();
    e = e.charAt(0).toUpperCase() + e.slice(1);


    return e;
}

export const normalizeWeight = (e) => {
    if (typeof e == 'number') {
        if (e >= 1000){
            e = e / 1000;
            if(e < 100) {
                e = e.toFixed(1)
            }
            else {
                e = e.toFixed(0);
            }
            
            return e + " t"
        } 
        else if (e <= 1) {
            e = e * 1000;
            e = e.toFixed(1);
            return e + " gr"
        }
        else {
            e = e.toFixed(1);
            return e + " kg"
        }
    }

    return e;
}

export const normalizeDate  = (e) => {
    return format(new Date(e), "d/M/yyyy")

}