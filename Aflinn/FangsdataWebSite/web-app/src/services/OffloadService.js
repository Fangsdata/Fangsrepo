import CONST from '../Constants';

async function getDataFromApi( filter ){
    const url = new URL( CONST.offloadApi + "/offloads"),
        params = filter
        Object.keys(params).forEach(key => url.searchParams.append(key, params[key])) ;
    const resp = await fetch(url);
    const json = await resp.json();
    return json;
}

const getOffloads = async () => {
        let data =  await getDataFromApi({count : 10});
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
