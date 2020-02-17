import CONST from '../Constants';

async function getDataFromApi( filter ){
    const url = new URL( CONST.offloadApi + "/offload");
    var params = filter;
    url.search = new URLSearchParams(params).toString();
    const resp = await fetch(url);
    const json = await resp.json();
    return json;
}

const getOffloads = async (filter = {
        boatLength: [''],
        boatFishingGear: [''],
        month: [''],
        year: [''],
        count: [''],
        landingTown: [''],
        landingState: ['']
    }) => {
        let data =  await getDataFromApi({filter});
        data.forEach(item => {
            if (!('boatImage' in item)){
                item.boatImage = "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg" 
            } 
        });
        return data;
    };


export {
    getOffloads
};
