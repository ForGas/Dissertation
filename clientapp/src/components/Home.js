import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { test: [], loading: true };
    }

    componentDidMount() {
        this.populateTestData();
    }

    static renderTest(forecasts) {
        return (
            <div>{forecasts}</div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : App.renderTest(this.state.test);

        return (
            <div>
                <h1>Home page</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateTestData() {
        await fetch('api/soarFile/test')
            .then((response) => response.text())
            .then((data) => {
                console.log(data);
                this.setState({ forecasts: data, loading: false });
            });
    }
}