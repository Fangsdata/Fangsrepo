import {OFFLOADAPI} from '../Constants';

async function getDataFromApi( filter ) {
    
    let url = OFFLOADAPI + '/offloads?';
    let params = ''; 
    Object.getOwnPropertyNames(filter).forEach(
        (prop)=>{ 
            if(typeof(filter[prop] === 'object')){
                if(filter[prop].length !== 0){
                    params += prop + '=';
                    filter[prop].forEach( p => {
                        params += "" + p + ",";
                    });
                    params = params.substring(0, params.length - 1);
                }
                params += '&'; 
            }
        }
    )
    params = params.substring(0, params.length - 1);

    url = url + params;
    const resp = await fetch(url);
    const json = await resp.json();
    return json;
}

const getOffloads = async (filter = {}) => {
        console.log(filter);
        let data =  await getDataFromApi(filter);
        if(data.status !== 400){
            data.forEach(item => {
                if (!('boatImage' in item)){
                    item.boatImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/38/RMS_Titanic_4.jpg/2560px-RMS_Titanic_4.jpg" 
                } 
            });
            return data;
        }
        else{
            return []
        }
    };
const getBoats = async (radioSignal = "") => {
    const resp = await fetch(OFFLOADAPI + "/boats/" + radioSignal);
    const json = await resp.json();
    return json;
};

const getOffloadsTest = async (filter = {}) => {
    const resp = await fetch(OFFLOADAPI + "/offloads?fishingGear=Garn&Count=5");
    const json = await resp.json();
    return json;
};

export {
    getOffloads,
    getBoats,
    getOffloadsTest
};
