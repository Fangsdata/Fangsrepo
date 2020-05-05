import React from 'react';
import { Map, Marker, TileLayer } from 'react-leaflet'
import 'leaflet/dist/leaflet.css';
import L from "leaflet";


class VesselMap extends React.Component {
    state = {
        zoom: 7
  }

  render() {
    const position = [this.props.lat, this.props.lng];
    const customMarker = L.icon({ iconUrl: require('./place-24px.svg'), })

    return (
      <Map center={position} zoom={this.state.zoom}>
        <TileLayer
          attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          url='https://{s}.tile.osm.org/{z}/{x}/{y}.png'
        />
        <Marker position={position}>
        </Marker>
      </Map>
    );
  }
}

export default VesselMap;

delete L.Icon.Default.prototype._getIconUrl;

L.Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png')
});
