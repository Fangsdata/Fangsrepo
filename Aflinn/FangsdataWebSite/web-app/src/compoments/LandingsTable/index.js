import React from 'react';

class LandingsTable extends React.Component {
    state = {
        landings: [{
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0
        },
        {
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0
        },
        {
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0
        },
        {
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0
        },
        {
            town: "",
            state: "",
            landingDate: "",
            totalWeight: 0
        }]
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
                console.log(res2)
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
        <tr>
            <td>1. </td>
            <td>{this.state.landings[0].landingDate}</td>
            <td>{this.state.landings[0].town}</td>
            <td>{this.state.landings[0].state}</td>
            <td>{this.state.landings[0].totalWeight} kg.</td>
        </tr>
        <tr>
            <td>2. </td>
            <td>{this.state.landings[1].landingDate}</td>
            <td>{this.state.landings[1].town}</td>
            <td>{this.state.landings[1].state}</td>
            <td>{this.state.landings[1].totalWeight} kg.</td>
        </tr>
        <tr>
            <td>3. </td>
            <td>{this.state.landings[2].landingDate}</td>
            <td>{this.state.landings[2].town}</td>
            <td>{this.state.landings[2].state}</td>
            <td>{this.state.landings[2].totalWeight} kg.</td>
        </tr>
        <tr>
            <td>4. </td>
            <td>{this.state.landings[3].landingDate}</td>
            <td>{this.state.landings[3].town}</td>
            <td>{this.state.landings[3].state}</td>
            <td>{this.state.landings[3].totalWeight} kg.</td>
        </tr>
        <tr>
            <td>5. </td>
            <td>{this.state.landings[4].landingDate}</td>
            <td>{this.state.landings[4].town}</td>
            <td>{this.state.landings[4].state}</td>
            <td>{this.state.landings[4].totalWeight} kg.</td>
        </tr>

    </table>
    );
  }
}

export default LandingsTable;