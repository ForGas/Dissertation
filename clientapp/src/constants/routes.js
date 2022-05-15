import React from "react";
import { HomePage } from '../components/pages/Home';

const FilesPage = React.lazy(() => import("../components/pages/Files"));
const FileFormPage = React.lazy(() => import("../components/files/FileForm"));

const routes = [
    {
        path: "/",
        component: HomePage,
        exact: false,
    },
    {
        path: "/files",
        component: FilesPage,
        exact: true,
    },
    {
        path: "/files/form",
        component: FileFormPage,
        exact: true,
    },
];

export default routes;