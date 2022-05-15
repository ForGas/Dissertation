import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { Home } from './components/pages/Home';
import { Files } from './components/pages/Files';
import { Layout } from './components/pages/Layout';
import { NotFoundPage } from './components/pages/NotFoundPage';
import { FileForm } from './components/files/FileForm';
import routes from './constants/routes';

export default function App() {
    return (
        <>
            <Routes>
                <Route path='/' element={<Layout />}>
                    <Route index element={<Home />} />
                    <Route path='/files' element={<Files />} />
                    <Route path='/files/form' element={<FileForm />} />
                    {/* <Route path="*" element={<NotFoundPage />} /> */}

                    {routes.map(({ path, component, exact }) => (
                        <Route key={path} exact={exact} path={path} component={component} />
                    ))}
                </Route>
            </Routes>
        </>
    );
}

