import React from 'react';

const OffloadDetails = ({offloadId}) => 
{

    async componentDidMount() {
        fetch(`http://fangsdata-api.herokuapp.com/api/Boats/${offloadId}`)
            .then((res) => res.json())
            .then((res) => {
                this.setState({boat: res})
                console.log(res)
            });
    }

    return (
        <div>
            <p>hello {offloadId}</p>
        </div>
    )
}
export default OffloadDetails;