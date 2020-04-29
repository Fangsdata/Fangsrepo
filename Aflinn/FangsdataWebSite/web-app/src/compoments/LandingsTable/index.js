import React from 'react';
import { Link } from 'react-router-dom';

class LandingsTable extends React.Component {
    state = {
        landings: [/*{
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0,
            id: ""
        }*/]
    };

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        const boatname = this.props.boatname.boatname;
        fetch(`https://fangsdata-api.herokuapp.com/api/offloads/` + boatname + "/5")
            .then((res2) => res2.json())
            .then((res2) => {
                this.setState({landings: res2})
            });
    }

  render() {
    return (
        <table className="landing-table">
        <tr>
            <th colSpan="5">Most recent landings</th>
        </tr>
        <tr>
            <td></td>
            <td>Date</td>
            <td>Town</td>
            <td>State</td>
            <td>Total weitght</td>
        </tr>
        {true
         ? this.state.landings.map((landing, i)=>(
            <tr>
                <td><Link to={"/offloads/"+ landing.id}> {i+1}. </Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.landingDate}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.town}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.state}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.totalWeight} kg.</Link></td>
            </tr>))
          :<></>}

    </table>
    );
  }
}

export default LandingsTable;