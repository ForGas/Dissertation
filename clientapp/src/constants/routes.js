import React from "react";
import { FilesPage } from '../pages/files/FilesPage'
import { NotFoundPage } from '../pages/notFound/NotFoundPage'
import { SystemVirusScanForm } from '../components/file/form/SystemVirusScanForm'
import { ReportDetails } from '../components/file/report/ReportDetails'

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
        exact: true,
    },
    {
        path: "/files/report/:incidentId",
        element: <ReportDetails />,
        exact: true,
    },
    {
        path: "*",
        element: <NotFoundPage />,
        exact: false,
    },
];

export default routes;