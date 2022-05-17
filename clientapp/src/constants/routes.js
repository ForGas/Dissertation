import React from "react";
import { FilesPage } from '../pages/FilesPage'
import { SystemVirusScanForm } from '../components/file/form/SystemVirusScanForm'
import { ReportDetails } from '../components/file/report/ReportDetails'
import { NotFoundPage } from '../pages/NotFoundPage'

// const FilesPage = React.lazy(() => import("../components/pages/FilesPage"));
// const SystemVirusScanForm = React.lazy(() => import("../components/file/form/SystemVirusScanForm"));
// const ReportDetails = React.lazy(() => import("../components/file/report/ReportDetails"));

const routes = [
    {
        path: "/files",
        element: <FilesPage />,
        exact: false,
    },
    {
        path: "/files/add",
        element: <SystemVirusScanForm />,
        exact: false,
    },
    {
        path: "/files/report",
        element: <ReportDetails />,
        exact: false,
    },
    {
        path: "*",
        element: <NotFoundPage />,
        exact: false,
    },
];

export default routes;