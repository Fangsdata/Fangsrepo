

const getOffloads = (filter = {
        boatLength: '',
        boatFishingGear: '',
        month: '',
        year: '',
        count: '',
        landingTown: '',
        landingState: ''
    }) => {
        return [
            {
                boatImage: "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg",
                boatName : "Snorri skúta",
                totalWeight : 700000,
                largestLanding : 20000,
                trips : 20,
                boatRadioSignalId : '1',
            },
            {
                boatImage: "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg",
                boatName : "Lorem bátur",
                totalWeight : 700000,
                largestLanding : 20000,
                trips : 20,
                boatRadioSignalId : '2',
            },
            {
                boatImage: "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg",
                boatName : "Ibsum skip",
                totalWeight : 700000,
                largestLanding : 20000,
                trips : 20,
                boatRadioSignalId : '3',

            },
            {
                boatImage: "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg",
                boatName : "önnur skúta",
                totalWeight : 700000,
                largestLanding : 20000,
                trips : 20,
                boatRadioSignalId : '4',

            },
            {
                boatImage: "http://www.blogsnow.com/wp-content/uploads/2017/01/Boat.jpg",
                boatName : "Snorri skúta",
                totalWeight : 700000,
                largestLanding : 20000,
                trips : 20,
                boatRadioSignalId : '5',

            },

        ]
    };


export {
    getOffloads
};
