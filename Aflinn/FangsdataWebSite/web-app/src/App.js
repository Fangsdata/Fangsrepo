import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import NavigationBar from './compoments/NavigationBar';
import TopOffloads from './compoments/TopOffLoads';
import Container from './compoments/Container';
import Boat from './compoments/Boat';

function App() {
  return (
    <div className="App">
      <NavigationBar />
      <Container>
        <Switch>
          <Route exact path="/" component={ TopOffloads } />
          <Route exact path="/home" render={ () => <Redirect to="/"/>} />
          <Route exact path="/boats" component={ Boat } />
        </Switch>
        {/* <TopOffloads /> */}
      </Container>
    </div>
  );
}

export default App;
