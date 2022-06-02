import React, { useState, useEffect, useMemo } from "react"
import { GET_VIRUS_TOTAL_REPORT } from '../../../constants/apiUrl'
import { ResponsiveContainer, Pie, PieChart, Cell, Tooltip, Legend, Label } from "recharts";
import { Container } from 'react-bootstrap';
import './reportDetailPie.css';

export const ReportDetailPie = ({ reportId }) => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [pieData, setPieData] = useState([]);
    const [error, setError] = useState();

    useEffect(() => {
        if (!reportId) return;
        console.log("gg")
        populateData();
    }, [reportId])

    useEffect(() => {
        if (loading) return;
        createPieData();
    }, [loading])

    async function populateData() {
        await fetch(`${GET_VIRUS_TOTAL_REPORT}/${reportId}`)
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    function createPieData() {
        const newPieData = data.map((item) => {
            console.log(item.detected)
            return {
                name: `${item.name} ${item.result ? `(${item.result})` : ""}`,
                value: 1.72,
                color: item.detected === true ? "#a0102a" : "#0e0d92"
            }
        })
        setPieData(newPieData);
    }

    return (
        <>
            <section className="report-detail-pie_wrapper">
                <Container>
                    <div className="report-pie-container">
                        <h1>Report Details</h1>
                        <div className="d-flex justify-content-center align-items-center">
                            <ResponsiveContainer aspect={1} width={870} position="center">
                                <PieChart width={870} height={400}>
                                    <Pie
                                        data={pieData}
                                        dataKey={"value"}
                                        fill="#8884d8"
                                        color="#000000"
                                    >
                                        <Label value="VIRUS SCAN RESULT" position="center" />
                                        {pieData.map((item, index) => (
                                            <Cell key={`cell-${index}`} fill={item.color} cx="50%" cy="50%" />
                                        ))}
                                    </Pie>
                                    <Tooltip />
                                    <Legend />
                                </PieChart>
                            </ResponsiveContainer>
                        </div>
                    </div>
                </Container>
            </section>
        </>
    )
}