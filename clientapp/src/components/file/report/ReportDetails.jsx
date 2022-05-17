import React, { useState, useEffect } from "react"
import { GET_VIRUS_TOTAL_REPORT } from '../../../constants/apiUrl'

export const ReportDetails = ({ reportId }) => {
    reportId = '21BDE1CD-28AA-44EB-9D3D-263CD2400877';
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!reportId) return;
        populateData();
    }, [reportId])

    async function populateData() {
        await fetch(`${GET_VIRUS_TOTAL_REPORT}/${reportId}`)
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    return (
        <>
            <div>ReportDetails</div>
            {console.log(data)}
        </>
    );
}

