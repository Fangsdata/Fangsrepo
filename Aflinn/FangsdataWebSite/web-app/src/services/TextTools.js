import { format } from 'date-fns';
import { id, el } from 'date-fns/locale';


export const normalizeCase = (str)=>{
    if(str === null){
        return "";
    }
    return str.replace(/\w\S*/g, function(txt){
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });
}


export const normalizeWeight = (e) => {
    if (typeof e == 'number') {
        if (e >= 1000){
            e = e / 1000;
            if(e < 100){
                e = e.toFixed(1)
            }
            else{
                e = e.toFixed(0);
            }
            
            return e + " t"
        } 
        else if (e <= 1){
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

export const normalizeMonth = (e) => {

    switch(e) {
        case 0: return ""
        case 1: return "januar"
        case 2: return "februar"
        case 3: return "mars"
        case 4: return "april"
        case 5: return "mai"
        case 6: return "juni"
        case 7: return "juli"
        case 8: return "august"
        case 9: return "september"
        case 10: return "oktober"
        case 11: return "november"
        case 12: return "desember"
        default: return ""
      }

}