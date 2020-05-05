import React from 'react';
import { Link } from 'react-router-dom';
import { normalizeWeight,normalizeDate, normalizeCase } from '../../services/TextTools';

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
                this.setState({landings: res2});
            });
    }

  render() {
      const {landings} = this.state;
    return (
        <table className="landing-table">
        <tr>
            <th className="landing-table-header" colSpan="5">Most recent landings</th>
        </tr>
        <tr >
            <td></td>
            <td>Date</td>
            <td>Town</td>
            <td>State</td>
            <td>Total weitght</td>
        </tr>
        { landings.length !== 0
         ? landings.map((landing, i)=>(
            <tr>
                <td><Link to={"/offloads/"+ landing.id}> {i+1}. </Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeDate(landing.landingDate)}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeCase(landing.town)}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{landing.state}</Link></td>
                <td><Link to={"/offloads/"+ landing.id}>{normalizeWeight(landing.totalWeight)}</Link></td>
            </tr>))
          :<th colSpan="5"><div className="loader"></div></th>}

    </table>
    );
  }
}

export default LandingsTable;