import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Layout } from './UI/Layout';
import { HomePage } from './pages/home/HomePage';
import { ReportDeailsPage } from './pages/reportDetails/ReportDeailsPage';
import { ReportDetails } from './components/file/report/ReportDetails';
import routes from './constants/routes';

export default function App() {
    return (
        <>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route index element={<HomePage />} />
                    <Route path="/files/report/:incidentId" element={<ReportDetails />} />
                    <Route path="/reports" element={<ReportDeailsPage />} />
                    {routes.map(({ path, element, exact }) => (
                        <Route key={path} path={path} element={element} exact={exact} />
                    ))}
                </Route>
            </Routes>
        </>
    );
}

