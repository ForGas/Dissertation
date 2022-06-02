import React, { useState, useEffect } from 'react'
import { Container } from 'react-bootstrap';
import { useNavigate } from "react-router-dom";
import './reportDeailsPage.css';

const ReportDeailsPage = () => {
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    const [content, setContent] = useState([]);
    let navigate = useNavigate();

    useEffect(() => {
        getReportDeailsData();
    }, [])

    useEffect(() => {
        createData();
    }, [loading])

    function navigateReport(id){
        navigate(`/files/report/${id}`);
    }

    function createData() {
        console.log(data);
        if (data === undefined) return;
        const result = data.map((item, index) => {
            return <li 
                key={index}
                onClick={() => navigateReport(item.fileIncidentId)}
                className="list-group-item"
                > id - {item.id} / md5 -{item.md5} / sha1 - {item.sha1} / sha256 -{item.sha256} / created -{item.created} 
            </li>
         })
        
         setContent(result);
    }

    async function getReportDeailsData() {
        await fetch(`api/SoarFile/GetReports`)
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    return (
        <>
            <section className='home-create-method_wrapper'>
            <Container>
                    <h1>ОТЧЕТЫ</h1>
                    <ul className="list-group list-group-flush">
                        {content}
                    </ul>
                </Container>
            </section>
        </>
    )
}

export { ReportDeailsPage };

