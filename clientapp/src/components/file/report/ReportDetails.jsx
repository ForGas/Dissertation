import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { Container, Card, ListGroup, Button } from 'react-bootstrap';
import { GET_FILE_INCIDENT, FILL_UP_REPORT } from '../../../constants/apiUrl'
import { ReportDetailPie } from '../detailPie/ReportDetailPie'
import './reportDetails.css'

export const ReportDetails = () => {
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    const [reportId, setReportId] = useState("");
    const { incidentId } = useParams();
    const readyStatus = "IsReady";
    const queueStatus = "InQueue";
    const [reportStatus, setReportStatus] = useState("");
    const [isButtonClick, setIsButtonClick] = useState(false);

    useEffect(() => {
        if (!incidentId) return;
        getIncidentData();
    }, [incidentId])

    useEffect(() => {
        if (loading) return;
        setReportId(data.virusTotalReportId);
    }, [loading])

    useEffect(() => {
        console.log(data)
        if (data.report === null) {
            fillUpVirusTotalReport();
        }
        else {
            console.log(reportStatus)
            console.log(data)
            console.log(data.report)
            if(data.report !== undefined) {
                setReportStatus(readyStatus);
            }
        }
    }, [reportId])

    function updateReportData() {
        setIsButtonClick(true);
        if (reportStatus !== readyStatus) {
            fillUpVirusTotalReport();
        }

        getIncidentData();
        setIsButtonClick(false);
    }

    async function getIncidentData() {
        await fetch(`${GET_FILE_INCIDENT}${incidentId}`)
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    async function fillUpVirusTotalReport() {
        console.log(reportStatus)
        await fetch(`${FILL_UP_REPORT}${reportId}`, {
            method: 'PATCH',
            url: '/',
        })
            .then((response) => response.json())
            .then(setReportStatus)
            .catch(setError);
        console.log(reportStatus)
    }

    let pieContent = reportStatus === readyStatus
        ? <ReportDetailPie reportId={reportId} />
        : <></>

    return (
        <>
            <section className="report-detail_wrapper">
                <Container>
                     <div>
                        <p className='text'>
                            ???????????? {reportStatus === readyStatus 
                                    ? "??????????" 
                                    : reportStatus === queueStatus 
                                        ? "?? ??????????????"
                                        : "????????????????"
                                }
                            </p>
                    </div>
                    <div className='report-container'>
                    <Card>
                        <Card.Header className='text'>??????????</Card.Header>
                        <ListGroup variant="flush">
                            <ListGroup.Item>??????: {data.code}</ListGroup.Item>
                            <ListGroup.Item>???????????????? ?????????? ?? ??????????????: {data.fileName}</ListGroup.Item>
                            <ListGroup.Item>??????????????????????????: {data.id}</ListGroup.Item>
                            <ListGroup.Item>Ip address: {data.ipAddress}</ListGroup.Item>
                            <ListGroup.Item>????????????: {data.domain}</ListGroup.Item>
                            <ListGroup.Item>???????????????? ???????????????????? ????????????????????: {data.isSystemScanClean ? "??????????????" : "?????????? ????????????????"}</ListGroup.Item>
                            <ListGroup.Item>Resource: {data.resource}</ListGroup.Item>
                            <ListGroup.Item>SHA1: {data.sha1}</ListGroup.Item>
                            <ListGroup.Item>SHA256: {data.sha256}</ListGroup.Item>
                            <ListGroup.Item>MD5: {data.md5}</ListGroup.Item>
                            <ListGroup.Item>?????????????? ????????????: {data.status}</ListGroup.Item>
                            <ListGroup.Item>??????????????????: {data.priority}</ListGroup.Item>
                            <ListGroup.Item>Resource VT: {data.resource}</ListGroup.Item>
                            <ListGroup.Item>Scan Id VT: {data.scanId}</ListGroup.Item>
                            <ListGroup.Item>???????????? ??????????: {data.report === null ? "??????????????????????" : "??????????????"}</ListGroup.Item>
                        </ListGroup>
                    </Card>
                    </div>               
                    <div className='d-flex justify-content-end mt-1'>
                        <Button
                            onClick={!isButtonClick ? updateReportData : null}
                            size="lg"
                            variant="outline-primary"
                            disabled={isButtonClick}
                        >
                            {isButtonClick ? '???????????????????????' : '???????????????? ??????????'}
                        </Button>
                    </div>
                    
                </Container>
            </section>
            {pieContent}
        </>
    )
}