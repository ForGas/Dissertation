import React, { Component } from 'react';
import { Routes, Route } from 'react-router-dom';
import { Home } from './components/Home';


export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Routes>
                <Route exact path='/' element={<Home />} />
                {/* <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} /> */}
            </Routes>
        );
    }
}