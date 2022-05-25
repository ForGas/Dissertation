import React, { useEffect, useState, useMemo } from 'react';
import { Table } from '../../UI/Table';
import { Container, Spinner, Row } from 'react-bootstrap';
import './files.css'
import { GET_FILE_ALL } from '../../constants/apiUrl'

export const FilesPage = () => {
    const [data, setData] = useState([]);
    const [error, setError] = useState();
    const [loading, setLoading] = useState(true);
    const [tableData, setTableData] = useState([]);

    useEffect(() => {
        populateData();
    }, [])

    useEffect(() => {
        createTableData();
    }, [loading])

    async function populateData() {
        await fetch(GET_FILE_ALL, {
            method: 'GET',
            url: '/',
        })
            .then((response) => response.json())
            .then(setData)
            .then(() => setLoading(false))
            .catch(setError);
    }

    function createTableData() {
        if (data.items === undefined) return;

        const tableData = data.items.map((item, index) => {
            return { ...item, subRows: undefined }
        })
        setTableData(tableData);
    }

    const columns = useMemo(
        () => [
            {
                Header: '#',
                columns: [
                    {
                        Header: 'Id',
                        accessor: 'id',
                    },
                    {
                        Header: 'Code',
                        accessor: 'code',
                    },
                ],
            },
            {
                Header: 'Scan Result',
                columns: [
                    {
                        Header: 'Status',
                        accessor: 'status',
                    },
                    {
                        Header: 'System Scan',
                        accessor: 'isSystemScanClean',
                    },
                ],
            },
            {
                Header: 'Info',
                columns: [
                    {
                        Header: 'Sha256',
                        accessor: 'sha256',
                    },
                    {
                        Header: 'Priority',
                        accessor: 'priority',
                    },
                    {
                        Header: 'Resource',
                        accessor: 'resource',
                    },
                ],
            },
        ],
        []
    )

    let contents = tableData.length < 0
        ? <Row className="justify-content-center">
            <Spinner animation="border" variant="primary" />
        </Row>
        : <Table columns={columns} data={tableData} />;

    return (
        <>
            <section className='files_wrapper'>
                <Container>
                    <h2 className='files-title_text'>File incident Table</h2>

                    <div>{contents}</div>
                </Container>
            </section>
        </>
    )
}
