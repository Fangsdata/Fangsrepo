import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import NavigationBar from './compoments/NavigationBar';
import TopOffloads from './compoments/TopOffLoads';
import Container from './compoments/Container';
// import Boat from './compoments/Boat';
import BoatDetails from './compoments/BoatDetails';
import Contact from './compoments/Contact';
import About from './compoments/About';


function App() {
  return (
    <div className="App">
      <NavigationBar />
      <Container>
        <Switch>
          <Route exact path="/" component={ TopOffloads } />
          <Route exact path="/home" render={ () => <Redirect to="/"/>} />
          <Route exact path="/contact" component={ Contact } />
          <Route exact path="/about" component={ About } />
          <Route exact path="/boats/:boatname" render={ e => <BoatDetails boatname={e.match.params.boatname}/> } />

        </Switch>
        {/* <TopOffloads /> */}
      </Container>
    </div>
  );
}

export default App;
