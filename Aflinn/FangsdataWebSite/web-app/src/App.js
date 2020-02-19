import React from 'react';
import NavigationBar from './compoments/NavigationBar';
import TopOffloads from './compoments/TopOffLoads';
import Container from './compoments/Container';

function App() {
  return (
    <div className="App">
      <NavigationBar />
      <Container>
        <TopOffloads />
      </Container>
    </div>
  );
}

export default App;
