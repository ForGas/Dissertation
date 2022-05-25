import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { Container, Card, ListGroup } from 'react-bootstrap';
import { GET_FILE_INCIDENT, FILL_UP_REPORT } from '../../../constants/apiUrl'
import { ReportDetailPie } from '../detailPie/ReportDetailPie'

export const ReportDetails = () => {
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    const [reportId, setReportId] = useState("");
    const { incidentId } = useParams();

    useEffect(() => {
        if (!incidentId) return;
        getIncidentData();
    }, [incidentId])

    useEffect(() => {
        if (loading) return;
        setReportId(data.virusTotalReportId);

        // if (data.report === null) {
        //     fillUpVirusTotalReport();
        // }
    }, [loading])

    async function getIncidentData() {
        await fetch(`${GET_FILE_INCIDENT}${incidentId}`)
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    async function fillUpVirusTotalReport() {
        await fetch(`${FILL_UP_REPORT}/${reportId}`, {
            method: 'PATCH',
            url: '/',
        })
            .then((response) => response.json())
            .catch(setError);
    }

    return (
        <>
            <section className="report-detail_wrapper">
                <Container>
                    <Card>
                        <Card.Header>Детали отчета</Card.Header>
                        <ListGroup variant="flush">
                            <ListGroup.Item>Код: {data.code}</ListGroup.Item>
                            <ListGroup.Item>Названия файла в системе: {data.fileName}</ListGroup.Item>
                            <ListGroup.Item>Идентификатор: {data.id}</ListGroup.Item>
                            <ListGroup.Item>Ip address: {data.ipAddress}</ListGroup.Item>
                            <ListGroup.Item>Клиент: {data.domain}</ListGroup.Item>
                            <ListGroup.Item>Проверка системными средствами: {data.isSystemScanClean ? "успешно" : "нужна проверка"}</ListGroup.Item>
                            <ListGroup.Item>Код: {data.resource}</ListGroup.Item>
                            <ListGroup.Item>SHA1: {data.sha1}</ListGroup.Item>
                            <ListGroup.Item>SHA256: {data.sha256}</ListGroup.Item>
                            <ListGroup.Item>MD5: {data.md5}</ListGroup.Item>
                            <ListGroup.Item>Текущий статус: {data.status}</ListGroup.Item>
                            <ListGroup.Item>Приоритет: {data.priority}</ListGroup.Item>
                            <ListGroup.Item>Resource VT: {data.resource}</ListGroup.Item>
                            <ListGroup.Item>Scan Id VT: {data.scanId}</ListGroup.Item>
                            <ListGroup.Item>Полный отчет: {data.report === null ? "отсутствует" : "имеется"}</ListGroup.Item>
                        </ListGroup>
                    </Card>
                    <div>{console.log(data)}</div>
                </Container>
            </section>
        </>
    )
}