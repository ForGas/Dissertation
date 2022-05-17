import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Layout } from './pages/Layout';
import { HomePage } from './pages/HomePage';
import routes from './constants/routes';

export default function App() {
    return (
        <>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route index element={<HomePage />} />
                    {routes.map(({ path, element, exact }) => (
                        <Route key={path} path={path} element={element} exact={exact} />
                    ))}
                </Route>
            </Routes>
        </>
    );
}

